using System;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace Service
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DiffBackup diff = new DiffBackup();
            FullBackup full = new FullBackup();
            IncrBackup incr = new IncrBackup();
            Writer myWriter = new Writer();


            while (true)
            {

                ConsoleKeyInfo info = Console.ReadKey();
                try
                {
                    if (info.Key == ConsoleKey.NumPad1)
                        Connect.RunCreateAsync().GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                try
                {
                    if (info.Key == ConsoleKey.NumPad5)
                        Connect.RunTemplateAsync().GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }



            }
        }
    }
}
