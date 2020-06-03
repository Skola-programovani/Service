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

            Overseer o = new Overseer();

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer((e) =>
            {
                Console.WriteLine("Overseer provádí kontrolu");
                o.RunAsync();

            }, null, startTimeSpan, periodTimeSpan);


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
                    if (info.Key == ConsoleKey.NumPad2)
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
