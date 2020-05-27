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


            //try
            //{
            //    Connect.RunCreateAsync().GetAwaiter().GetResult();
            //    Connect.RunTemplateAsync().GetAwaiter().GetResult();

            //    while (true)
            //    {
            //        var sj = new ScheduledJob();
            //        sj.Run(myWriter.Read("Repetition"));

            //        Console.WriteLine(@"{0}Check Quartz.net\Trace\application.log.txt for Job updates{0}",
            //                            Environment.NewLine);

            //        Console.WriteLine("{0}Press Ctrl^C to close the window. The job will continue " +
            //                            "to run via Quartz.Net windows service, " +
            //                            "see job activity in the Quartz.Net Trace file...{0}",
            //                            Environment.NewLine);

            //        Thread.Sleep(10000 * 100000);
            //    }
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine(e.InnerException.Message);
            //}
            //Console.ReadLine();
            //**********************************************************************************************************************************************************************************************

            while (true)
            {

                ConsoleKeyInfo info = Console.ReadKey();

                if (info.Key == ConsoleKey.NumPad1)
                    Connect.RunCreateAsync().GetAwaiter().GetResult();
                try
                {
                    if (info.Key == ConsoleKey.NumPad2)
                    {
                        if (Convert.ToInt32(File.ReadAllText(@"C:\MaxFull.txt")) - 1 == 0)
                        {
                            myWriter.DecreaseInText("MaxFull");
                            full.Copy(@"C:\1", @"C:\2");
                            Console.WriteLine("done");
                        }
                        else
                        {
                            Console.WriteLine("přesažen limit FullBackupu");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    if (info.Key == ConsoleKey.NumPad3)
                    {
                        if (Convert.ToInt32(File.ReadAllText(@"C:\MaxSegments.txt")) - 1 == 0)
                        {
                            myWriter.DecreaseInText("MaxSegments");
                            diff.Copy(@"C:\1", @"C:\2");
                            Console.WriteLine("done");
                        }
                        else
                        {
                            Console.WriteLine("přesažen limit FullSegments");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    if (info.Key == ConsoleKey.NumPad4)
                    {
                        if (Convert.ToInt32(File.ReadAllText(@"C:\MaxSegments.txt")) - 1 == 0)
                        {
                            myWriter.DecreaseInText("MaxSegments");
                            incr.Copy(@"C:\1", @"C:\2");
                            Console.WriteLine("done");
                        }
                        else
                        {
                            Console.WriteLine("přesažen limit FullSegments");
                        }
                    }
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
