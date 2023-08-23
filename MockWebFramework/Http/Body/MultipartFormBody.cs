using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http.Body
{
    internal class MultipartFormBody : ObjectBody
    {

        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.MultipartForm);
        public MultipartFormBody(Memory<byte> contentBytes) : base(contentBytes)
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
    }
}
