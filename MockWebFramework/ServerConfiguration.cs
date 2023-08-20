using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework
{
    internal class ServerConfiguration
    {
        public bool DebugMode { get; set; } = true;


        public ServerConfiguration()
        {
            
        }


        public static ServerConfiguration Config { get; }= new ServerConfiguration();


    }
}
