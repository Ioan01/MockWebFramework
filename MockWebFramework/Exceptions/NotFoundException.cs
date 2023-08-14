using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Exceptions
{
    internal class NotFoundException : HttpException
    {
        public NotFoundException() : base(404,"Not found")
        {

        }
    }
}
