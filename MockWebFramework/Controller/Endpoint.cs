using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MockWebFramework.Controller.Attributes;
using MockWebFramework.Controller.Attributes.Endpoint;
using MockWebFramework.Controller.Attributes.From;
using MockWebFramework.Http;
using MockWebFramework.Http.Body;
using MockWebFramework.Http.HttpExceptions;
using MockWebFramework.Logging;

namespace MockWebFramework.Controller
{
    enum ParameterSource
    {
        FromQuery,
        FromBody,
        FromForm,
        FromRoute
    }

    

    internal class Endpoint
    {
        public string Method { get; }

        public Regex PathMatcher { get;  }


        private MethodInfo methodInfo;

        private List<(ParameterInfo, ParameterSource)> parameters = new();


        private bool IsNullable(ParameterInfo parameterInfo)
        {
            return parameterInfo.ParameterType.IsGenericType && parameterInfo.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        private bool IsNullabePrimitiveOrString(ParameterInfo p)
        {
            if (IsNullable(p))
            {
                var genericArgument = p.ParameterType.GetGenericArguments()[0];
                if (genericArgument.IsPrimitive || genericArgument == typeof(string))
                    return true;
            }

            return false;
        }

        private bool IsPrimitiveOrString(ParameterInfo p) => p.ParameterType.IsPrimitive || p.ParameterType == typeof(string);

        private IEnumerable<ParameterInfo> FilterNullablePrimitives(IEnumerable<ParameterInfo> parameterInfos)
        {
            return parameterInfos.Where(p => !IsNullabePrimitiveOrString(p));
        }

        // ensure get endpoints do not use [fromBody] or [fromForm]
        private void EnsureNoGetBody()
        {
            if (Method == WebRequestMethods.Http.Get)
                if (methodInfo.GetParameters()
                    .Any(param =>
                    {
                        var attr = param.GetCustomAttributes().FirstOrDefault(attr => attr is ParameterFromAttribute);
                        return attr is FromBodyAttribute or FromFormAttribute;
                    }))
                    Ilogger.Instance.LogError($"{methodInfo.Name} from {methodInfo.DeclaringType} of type GET can only have parameters from route and queries ");
        }

        // ensrue that the endpoint's parameters sources are declared with [fromBody], [fromRoute] etc
        private void EnsureNoUnannotatedParams()
        {
            if (methodInfo.GetParameters().Any(param =>
                {
                    var fromAttribute = param.GetCustomAttributes()
                        .FirstOrDefault(attr => attr is ParameterFromAttribute);
                    if (fromAttribute == null)
                    {
                        Ilogger.Instance.LogFatal(
                            $"Parameter {param.Name} from {methodInfo} does not have From attribute");
                        return true;
                    }

                    return false;
                })) 
                Ilogger.Instance.LogFatal($"{methodInfo.Name} from {methodInfo.DeclaringType.Name} needs all parameters to have a [from...] Attribute");
        }

        // query and route parameters can only have primitive or string types
        private void VerifyRouteAndQueryParams()
        {

            
            var routeAndQueryParams = methodInfo.GetParameters().Where(p =>
                p.GetCustomAttributes().Any(attr => attr is FromQueryAttribute || attr is FromRouteAttribute));

            // remove parameters with nullable primitive or string types
            routeAndQueryParams = FilterNullablePrimitives(routeAndQueryParams);


            if (routeAndQueryParams.Any(p => !p.ParameterType.IsPrimitive && p.ParameterType != typeof(string))) 
                Ilogger.Instance.LogFatal($"{methodInfo.Name} from {methodInfo.DeclaringType.Name} has route or query parameters of non-string reference types");
        }

        // fromBody and fromForm can only extract either one or multiple primitive types or one non-string reference
        // type, but not both
        private void VerifyBodyAndFormParams()
        {
            var classParameterInfos = methodInfo.GetParameters()
                .Where(p=>p.GetCustomAttributes().Any(attr=>attr is FromFormAttribute || attr is FromBodyAttribute))
                .Where(p => !p.ParameterType.IsPrimitive && p.ParameterType != typeof(string)).ToArray();

            // remove parameters with nullable primitive or string types

            classParameterInfos = FilterNullablePrimitives(classParameterInfos).ToArray();

            // cannot have multiple non-string reference types parameters
            if (classParameterInfos.Length > 1)
                Ilogger.Instance.LogFatal($"{methodInfo.Name} from {methodInfo.DeclaringType.Name} cannot have two non-string reference types as parameters");

            var primitiveParameter = methodInfo.GetParameters()
                .Where(p => p.GetCustomAttributes().Any(attr => attr is FromFormAttribute || attr is FromBodyAttribute))
                .FirstOrDefault(p => p.ParameterType.IsPrimitive || p.ParameterType == typeof(string));

            if (classParameterInfos.Length != 0 && primitiveParameter != null)
                Ilogger.Instance.LogFatal($"{methodInfo.Name} from {methodInfo.DeclaringType.Name} cannot have both non-string reference types and primitive types extracted from the body");

        }


        public Endpoint(MethodInfo methodInfo, HttpRouteAttribute httpRouteAttribute)
        {

            Method = httpRouteAttribute.Method;

            this.methodInfo = methodInfo;

            EnsureNoGetBody();
            VerifyRouteAndQueryParams();
            EnsureNoUnannotatedParams();
            VerifyBodyAndFormParams();


            // get name from route attribute or function name if route is null
            var endpointRoute = httpRouteAttribute.Route != null ?  httpRouteAttribute.Route.ToLower() : "/" + methodInfo.Name.ToLower();


            StringBuilder pathPattern = new StringBuilder(endpointRoute.Length);


            foreach (var parameterInfo in this.methodInfo.GetParameters())
            {
                var fromAttribute = parameterInfo.GetCustomAttributes()
                    .FirstOrDefault(attr => attr is ParameterFromAttribute) as ParameterFromAttribute;
                

                    parameters.Add((parameterInfo, fromAttribute.ParameterSource));
                    if (fromAttribute.ParameterSource == ParameterSource.FromRoute)
                    {
                        // if parameter is from route, the endpoint must contain it in its' path
                        // /books/get/bookid for example, bookid being a [fromRoute] parameter
                        if (!endpointRoute.Contains(parameterInfo.Name,StringComparison.OrdinalIgnoreCase))
                            Ilogger.Instance.LogFatal($"{methodInfo.Name} from {methodInfo.DeclaringType.Name} does not have parameter {parameterInfo.Name} in its' http route");
                        
                        // path pattern contains all the route params
                        // the string will replace all route params in the route with capture groups
                        // in the final path regex
                        if (pathPattern.Length > 0)
                            pathPattern.Append('|');
                        pathPattern.Append($"({parameterInfo.Name.ToLower()})");
                    }
                    
            }

            // create pattern to allow the capture of route params if route params are used
            // /books/get/(captureGroup)/(captureGroup2)/.... to capture the route param(s)
            if (pathPattern.Length > 0)
                PathMatcher = new Regex("^"+
                    Regex.Replace(endpointRoute, pathPattern.ToString(),
                        "([a-zA-Z0-9.-_~!$&'\\(\\)\\-\\*\\+,;=:@%]+)"),
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // otherwise just create a normal path matcher
            else PathMatcher = new Regex("^" + endpointRoute, RegexOptions.Compiled);



        }

        private object ConvertToParameterType(ParameterInfo parameterInfo, string value)
        {

            var type = parameterInfo.ParameterType;
            if (IsNullable(parameterInfo))
                type = parameterInfo.ParameterType.GetGenericArguments()[0];



            if (type == typeof(string))
                return value;

            try
            {
                if (type == typeof(Int32))
                    return Int32.Parse(value);

                if (type == typeof(float))
                    return float.Parse(value);

                if (type == typeof(double))
                    return Double.Parse(value);

                if (type == typeof(bool))
                    return Boolean.Parse(value);

                throw new InternalServerErrorException();

            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message,null,ex);
            }
            
        }

        private object ConvertToParameterType(ParameterInfo parameterInfo, HttpBody body)
        {
            if (IsNullabePrimitiveOrString(parameterInfo) || IsPrimitiveOrString(parameterInfo))
            {
                return ConvertToParameterType(parameterInfo, body.GetParameter(parameterInfo.Name.ToLower()));
            }

            if (body is not JsonBody && body is not XmlBody && body is not FormBody)
                throw new BadRequestException();

            if (body is JsonBody jsonBody)
                return jsonBody.As(parameterInfo.ParameterType);

            if (body is FormBody formBody)
                return formBody.As(parameterInfo.ParameterType);
            
            return (body as XmlBody)!.As(parameterInfo.ParameterType);

        }

        

        public object? Invoke(object controller, HttpRequest httpRequest, MatchCollection? matchCollection)
        {
            object[] @params = new object[parameters.Count];
            int index = 0;
            int routeIndex = 1;

            foreach (var (parameterInfo,source) in parameters)
            {
                switch (source)
                {
                    case ParameterSource.FromForm:
                        if (httpRequest.Body is not FormBody)
                            throw new UnsupportedMediaTypeException();
                        @params[index++] = ConvertToParameterType(parameterInfo, httpRequest.Body);
                        break;
                    case ParameterSource.FromBody:
                        if (httpRequest.Body is not JsonBody or XmlBody)
                            throw new UnsupportedMediaTypeException();
                            @params[index++] = ConvertToParameterType(parameterInfo, httpRequest.Body);
                        break;
                    case ParameterSource.FromRoute:
                        var routeVal = Uri.UnescapeDataString(matchCollection[0].Groups[routeIndex++].Value);
                        @params[index++] = ConvertToParameterType(parameterInfo,routeVal);
                        break;
                    case ParameterSource.FromQuery:
                        if (!IsNullable(parameterInfo))
                            @params[index++] = ConvertToParameterType(parameterInfo, httpRequest.Query.Parameters[parameterInfo.Name]);
                        else if (httpRequest.Query != null && httpRequest.Query.Parameters.ContainsKey(parameterInfo.Name.ToLower()))
                        {
                            @params[index++] = ConvertToParameterType(parameterInfo,
                                httpRequest.Query.Parameters[parameterInfo.Name]);

                        }
                        else
                        {
                            @params[index++] = null;}
                        break;
                }
            }

            return methodInfo.Invoke(controller, @params);
        }

        
    }
}
