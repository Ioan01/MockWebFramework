using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MockWebFramework.Controller.Attributes;
using MockWebFramework.Logging;
using MockWebFramework.Networking.HttpRequest;

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


        private MethodInfo method;

        private List<(ParameterInfo, ParameterSource)> parameters = new();

        public Endpoint(MethodInfo method, HttpRouteAttribute httpRouteAttribute)
        {

            Method = httpRouteAttribute.Method;

            this.method = method;



            // ensure get endpoints do not use body
            if (httpRouteAttribute.Method == WebRequestMethods.Http.Get)
                if (this.method.GetParameters()
                    .Any(param =>
                    {
                        var attr = param.GetCustomAttributes().FirstOrDefault(attr => attr is ParameterFromAttribute);
                        if (attr != null)
                            return attr is FromBodyAttribute or FromFormAttribute;
                        return false;
                    }))
                    Ilogger.Instance.LogError($"{method.Name} from {method.DeclaringType} of type GET can only have parameters from route and queries ");

            var endpointRoute = httpRouteAttribute.Route != null ? httpRouteAttribute.Route : method.Name.ToLower();


            StringBuilder pathPattern = new StringBuilder(endpointRoute.Length);


            foreach (var parameterInfo in this.method.GetParameters())
            {
                var fromAttribute = parameterInfo.GetCustomAttributes()
                    .FirstOrDefault(attr => attr is ParameterFromAttribute) as ParameterFromAttribute;
                if (fromAttribute == null)
                {
                    Ilogger.Instance.LogFatal($"Parameter {parameterInfo.Name} from {method} does not have From attribute");
                }
                else
                {
                    parameters.Add((parameterInfo, fromAttribute.ParameterSource));
                    if (fromAttribute.ParameterSource == ParameterSource.FromRoute)
                    {
                        // if parameter is from route, the endpoint must contain it in its' path
                        if (!endpointRoute.Contains(parameterInfo.Name,StringComparison.OrdinalIgnoreCase))
                            Ilogger.Instance.LogFatal($"{method.Name} from {method.DeclaringType.Name} does not have parameter {parameterInfo.Name} in its' http route");
                        if (pathPattern.Length > 0)
                            pathPattern.Append('|');
                        pathPattern.Append($"({parameterInfo.Name.ToLower()})");
                    }
                        
                }
            }

            // create pattern to allow the capture of route params if route params are used
            if (pathPattern.Length > 0)
                PathMatcher = new Regex(
                    Regex.Replace(endpointRoute, pathPattern.ToString(),
                        "([a-zA-Z0-9.-_~!$&'\\(\\)\\-\\*\\+,;=:@]+)"),
                    RegexOptions.Compiled);
            // otherwise just create a normal path matcher
            else PathMatcher = new Regex(endpointRoute, RegexOptions.Compiled);



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
                    case ParameterSource.FromBody:
                        @params[index++] = httpRequest.Body.GetParameter(parameterInfo.Name);
                        break;
                    case ParameterSource.FromRoute:
                        @params[index++] = matchCollection[0].Groups[routeIndex++].Value;
                        break;
                    case ParameterSource.FromQuery:
                        @params[index++] = httpRequest.Query.Parameters[parameterInfo.Name];
                        break;
                }
            }

            return method.Invoke(controller, @params);
        }

        
    }
}
