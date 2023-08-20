﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Networking.HttpRequest;

namespace MockWebFramework.HttpExceptions
{
    internal class MethodNotAllowedException : HttpException
    {
        public MethodNotAllowedException(string? message = null,IEnumerable<Header>? headers = null, Exception? innerException = null) : base(405,message,headers, innerException)
        {
        }
    }
}
