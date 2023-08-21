using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.HttpExceptions;
using MockWebFramework.Networking.Http.Body;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockWebFramework.Networking.HttpRequest.Body
{
    internal class JsonBody : HttpBody
    {
        private JObject body;

        private string? deserializedBody;

        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.Json);

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



        public object As(Type type)
        {
            return body.ToObject(type) ?? throw new BadRequestException();
        }
    }
}
