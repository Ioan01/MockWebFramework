using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http.Body
{
    internal class FormBody : HttpBody
    {
        public FormBody(Memory<byte> contentBytes) : base(contentBytes)
        {
        }


        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.FormData);

        public override string? GetParameter(string name)
        {
            throw new NotImplementedException();
        }
    }
}
