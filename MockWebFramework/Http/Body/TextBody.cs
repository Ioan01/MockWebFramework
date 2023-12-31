﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http.Body
{
    internal class TextBody : HttpBody
    {
        private string? _contentString = null;

        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.TextPlain);
        public string Content
        {
            get
            {
                if (_contentString == null)
                    _contentString = Encoding.UTF8.GetString(contentBytes.Span);
                return _contentString;
            }
        }


        public TextBody(Memory<byte> contentBytes) : base(contentBytes)
        {

        }

   
    }
}
