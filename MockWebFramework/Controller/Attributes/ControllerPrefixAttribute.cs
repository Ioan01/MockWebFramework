namespace MockWebFramework.Controller.Attributes
{
    internal class ControllerPrefixAttribute : Attribute
    {
        public string Prefix { get; }

        public ControllerPrefixAttribute(string prefix)
        {
            Prefix = prefix;
        }
    }
}
