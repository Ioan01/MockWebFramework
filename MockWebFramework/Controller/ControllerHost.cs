using System.Reflection;
using System.Text;
using MockWebFramework.Controller.Attributes;
using MockWebFramework.Controller.Attributes.Endpoint;
using MockWebFramework.Http.Body;
using MockWebFramework.Http.HttpExceptions;
using MockWebFramework.Http.Response;
using MockWebFramework.Logging;
using MockWebFramework.Networking;
using MockWebFramework.Service;


namespace MockWebFramework.Controller
{
    internal class ControllerHost
    {
        private readonly ServiceContainer _services;
        private Dictionary<string, ControllerRegistration> _controllers = new();

        public ControllerHost(ServiceContainer services)
        {
            _services = services;
        }


        public void HandleRequest(RequestReceivedEvent e)
        {
            if (!_controllers.ContainsKey(e.Request.RouteList[0]))
                throw new NotFoundException();
            var controller = _controllers[e.Request.RouteList[0]];

            object? returnValue = controller.Handle(e.Request);

            if (returnValue is null)
            {
                e.Response = new OK();
                return;
            }

            if (returnValue is string || returnValue.GetType().IsPrimitive)
            {
                e.Response = new OK(new TextBody(Encoding.UTF8.GetBytes(returnValue.ToString())));
                return;
            }

            

            e.Response = new OK(new JsonBody(returnValue));



            
            
            
        }

        public void RegisterControllers(string @namespace = "Controllers")
        {
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(t=>t.Namespace != null &&  !t.Namespace.StartsWith("System") && t.Namespace.Contains(@namespace));
            foreach (var controller in controllers)
            {
                if (!controller.GetMethods()
                    .Any(methodInfo=>methodInfo.GetCustomAttributes()
                        .Any(attribute=>
                            attribute is HttpRouteAttribute)))
                    Ilogger.Instance.LogWarning($"Class {controller.FullName} has no http routes assigned.");
                    
                RegisterController(controller);
            }
        }

        private void RegisterController(Type controllerType)
        {
            string controllerName;

            var prefixAttribute = controllerType.GetCustomAttributes()
                .FirstOrDefault(attr => attr is ControllerPrefixAttribute) as ControllerPrefixAttribute;

            if (prefixAttribute != null)
            {
                controllerName = prefixAttribute.Prefix;
            }
            else if (controllerType.Name.EndsWith("controller", true, null))
            {
                controllerName = '/' +
                    controllerType.Name.Substring(0, controllerType.Name.Length - "controller".Length);
            }
            else { controllerName = '/' + controllerType.Name; }

            if (controllerName == "/")
                controllerName = String.Empty;
            else controllerName = controllerName.ToLower();

            if (_controllers.ContainsKey(controllerName))
                throw new Exception($"Controller {controllerName} is already defined");



            _controllers.Add(controllerName.Substring(1), new ControllerRegistration(controllerType,controllerName,_services));

        }
    }
}
