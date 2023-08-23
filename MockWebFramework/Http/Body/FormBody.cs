using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Http.HttpExceptions;
using Newtonsoft.Json;

namespace MockWebFramework.Http.Body
{
    internal class FormBody : ObjectBody
    {
        private Dictionary<string,string> _formBody = new Dictionary<string,string>();

        public FormBody(Memory<byte> contentBytes) : base(contentBytes)
        {
            // skip over \r\n
            ReadOnlySpan<byte> dataSpan = contentBytes.Span.Slice(2);




            while (!dataSpan.IsEmpty)
            {
                int equalsIndex = dataSpan.IndexOf((byte)'=');
                if (equalsIndex == -1)
                {
                    break; // No more key-value pairs found
                }

                ReadOnlySpan<byte> keyBytes = dataSpan.Slice(0, equalsIndex);
                dataSpan = dataSpan.Slice(equalsIndex + 1);

                int ampersandIndex = dataSpan.IndexOf((byte)'&');
                ReadOnlySpan<byte> valueBytes;
                if (ampersandIndex == -1)
                {
                    valueBytes = dataSpan;
                    dataSpan = ReadOnlySpan<byte>.Empty;
                }
                else
                {
                    valueBytes = dataSpan.Slice(0, ampersandIndex);
                    dataSpan = dataSpan.Slice(ampersandIndex + 1);
                }

                string key = Uri.UnescapeDataString(Encoding.UTF8.GetString(keyBytes));
                string value = Uri.UnescapeDataString(Encoding.UTF8.GetString(valueBytes));
                _formBody.Add(key,value);


            }
        }


        public override Header? ContentTypeHeader => ContentTypes.GetContentTypeHeader(ContentTypes.FormData);

        public override string? GetParameter(string name)
        {
            if (!_formBody.ContainsKey(name))
                throw new BadRequestException();

            return _formBody[name];
        }

        public override object[] GetArray(string name, Type tpe)
        {
            throw new NotImplementedException();
        }

        public object As(Type type)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_formBody),type) ?? null;
        }
    }
}
