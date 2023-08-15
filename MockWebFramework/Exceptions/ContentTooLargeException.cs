using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Exceptions
{
    internal class ContentTooLargeException : HttpException
    {
        public ContentTooLargeException(string? message = null) : base(413, "Content Too Large",message)
        {
        }
    }
}
