using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Http.HttpExceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockWebFramework.Http.Body
{
    internal class JsonBody : ObjectBody
    {
        private JObject body = new JObject();

        private string? deserializedBody;

        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.Json);

        public JsonBody(Memory<byte> contentBytes) : base(contentBytes)
        {

            var oldBody = JObject.Parse(Encoding.UTF8.GetString(this.contentBytes.Span));

            foreach (var (str,token) in oldBody)
            {
                

                body.Add(str.ToLower(),token);
            }
        }

        public JsonBody(object obj) : base(null)
        {
            if (typeof(IEnumerable).IsAssignableFrom(obj.GetType()))
                contentBytes = new Memory<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, Formatting.Indented)));
            else
            {
                body = JObject.FromObject(obj);
                contentBytes = new Memory<byte>(Encoding.UTF8.GetBytes(body.ToString()));
            }
            

        }

        public override string? GetParameter(string name)
        {
            return body[name]?.Value<string>();
        }

        public override object GetArray(string name, Type type)
        { 
            var arr = body[name] as JArray;

            return arr.ToObject(type);
        }


        public object As(Type type)
        {
            return body.ToObject(type) ?? throw new BadRequestException();
        }

        
    }
}
