using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http.Body
{
    internal class ErrorBody : HttpBody
    {
        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.TextPlain);
        public ErrorBody(string errorMessage) : base(errorMessage != null && ServerConfiguration.Config.DebugMode ? Encoding.UTF8.GetBytes(errorMessage) : Memory<byte>.Empty)
        {
        }

        public override string? GetParameter(string name)
        {
            throw new NotImplementedException();
        }
    }
}
