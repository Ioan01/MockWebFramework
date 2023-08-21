using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http.Body
{
    internal class XmlBody : HttpBody
    {
        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.Xml);
        public XmlBody(Memory<byte> contentBytes) : base(contentBytes)
        {
        }



        public override string? GetParameter(string name)
        {
            throw new NotImplementedException();
        }

        public object As(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
