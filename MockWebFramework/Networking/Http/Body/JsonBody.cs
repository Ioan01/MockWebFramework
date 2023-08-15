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


        public JsonBody(Memory<byte> contentBytes) : base(contentBytes)
        {

            body = JObject.Parse(Encoding.UTF8.GetString(this.contentBytes.Span));
        }
    }
}
