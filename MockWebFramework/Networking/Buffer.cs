using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking
{
    internal class Buffer
    {
        public Memory<byte> ArraySegment { get;  }

        public bool Free { get; set; } = true;

        public Buffer(int maxPacketSize)
        {
            ArraySegment = new Memory<byte>(new byte[maxPacketSize]);
        }



    }
}
