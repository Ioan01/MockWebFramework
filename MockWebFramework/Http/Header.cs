using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http
{
    public class Header
    {
        public string Name { get; }

        public string?[] Values { get; }

        public byte[] Bytes
        {
            get
            {
                int index = 0;
                var byteArray = new byte[Name.Length + 2 + Values.Length
                                         + Values.Sum(s => s.Length) + 1];

                foreach (var chr in Name)
                {
                    byteArray[index++] = (byte)chr;
                }

                byteArray[index++] = (byte)':';
                byteArray[index++] = (byte)' ';

                foreach (var str in Values)
                {
                    foreach (var chr in str)
                    {
                        byteArray[index++] = (byte)chr;
                    }

                    byteArray[index++] = (byte)',';

                }
                byteArray[index - 1] = 0xd;
                byteArray[index] = 0xa;
                return byteArray;
            }
        }

        public Header(string headerName, string headerValue)
        {
            Name = headerName;

            Values = headerValue.Split(',');
        }


        public static List<Header> DefaultHeaders = new List<Header>()
        {
            new Header("Server","Macabee")
        };

        public static Header EmptyBodyHeader = new Header("Content-Length", "0");


    }
}
