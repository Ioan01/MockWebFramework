using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockWebFramework.Networking.HttpRequest.Body
{
    internal class JsonBody : HttpBody
    {
        private JObject body;

        private string? deserializedBody;

        public JsonBody(Memory<byte> contentBytes) : base(contentBytes)
        {

            body = JObject.Parse(Encoding.UTF8.GetString(this.contentBytes.Span));
        }

        public JsonBody(object obj) : base(null)
        {
            body = JObject.FromObject(obj);
            contentBytes = new Memory<byte>(Encoding.UTF8.GetBytes(body.ToString()));

        }

        public override string? GetParameter(string name)
        {
            return body[name]?.Value<string>(); 
        }
    }
}
