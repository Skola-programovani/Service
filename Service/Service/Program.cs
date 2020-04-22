using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Connect.RunAsync().GetAwaiter().GetResult();
        }
    }
}
