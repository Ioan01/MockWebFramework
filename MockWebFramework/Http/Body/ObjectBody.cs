using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http.Body
{
    internal abstract class ObjectBody : HttpBody
    {
        public abstract string? GetParameter(string name);

        public abstract object GetArray(string name,Type tpe);


        protected ObjectBody(Memory<byte> contentBytes) : base(contentBytes)
        {
        }
    }
}
