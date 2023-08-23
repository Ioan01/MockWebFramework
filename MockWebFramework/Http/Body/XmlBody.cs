using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MockWebFramework.Http.Body
{
    internal class XmlBody : ObjectBody
    {
        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.Xml);
        public XmlBody(Memory<byte> contentBytes) : base(contentBytes)
        {
        }



        public override string? GetParameter(string name)
        {
            throw new NotImplementedException();
        }

        public override object[] GetArray(string name, Type tpe)
        {
            throw new NotImplementedException();
        }


        public object As(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
