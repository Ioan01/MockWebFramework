using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking.HttpRequest.Body
{
    internal class FileBody : HttpBody
    {
        public FileBody(Memory<byte> contentBytes) : base(contentBytes)
        {
        }

        

        public override string? GetParameter(string name)
        {
            throw new NotImplementedException();
        }
    }
}
