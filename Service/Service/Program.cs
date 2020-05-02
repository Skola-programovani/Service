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
            CompareDir diff = new CompareDir();
 
            while (true)
            {

                ConsoleKeyInfo info = Console.ReadKey();

                if (info.Key == ConsoleKey.NumPad1)
                    Connect.RunCreateAsync().GetAwaiter().GetResult();
                if (info.Key == ConsoleKey.NumPad2)
                    diff.DirHandler(@"C:\1", @"C:\2");
                    Console.WriteLine("done");


            }
        }
    }
}
