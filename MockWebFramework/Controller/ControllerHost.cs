using System.Reflection;
using MockWebFramework.Controller.Attributes;
using MockWebFramework.Exceptions;
using MockWebFramework.Logging;
using MockWebFramework.Networking;

namespace MockWebFramework.Controller
{
    internal class ControllerHost
    {
        private Dictionary<string, ControllerRegistration> _controllers = new();


        public void HandleRequest(RequestReceivedEvent e)
        {
            var controller = _controllers[e.Request.Route[0]];

            if (controller != null)
            {
                object returnValue = controller.Handle(e.Request);
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public void RegisterControllers(string @namespace = "Controllers")
        {
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(t=>t.Namespace != null && t.Namespace.EndsWith("Controllers"));
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
                .First(attr => attr is ControllerPrefixAttribute) as ControllerPrefixAttribute;

            if (prefixAttribute != null)
            {
                controllerName = prefixAttribute.Prefix;
            }
            else if (controllerType.Name.EndsWith("controller", true, null))
            {
                controllerName = 
                    controllerType.Name.Substring(0, controllerType.Name.Length - "controller".Length);
            }
            else { controllerName = controllerType.Name; }

            if (_controllers.ContainsKey(controllerName))
                throw new Exception($"Controller {controllerName} is already defined");



            _controllers.Add(controllerName, new ControllerRegistration(controllerType,controllerName));

        }
    }
}
