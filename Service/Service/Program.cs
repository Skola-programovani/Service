using System;
using System.IO;

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

                if (info.Key == ConsoleKey.NumPad1)
                    Connect.RunCreateAsync().GetAwaiter().GetResult();
                try
                {
                    if (info.Key == ConsoleKey.NumPad2)
                    {
                        if (Convert.ToInt32(File.ReadAllText(@"c:\MaxFull.txt")) - 1 == 0)
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
                        if (Convert.ToInt32(File.ReadAllText(@"c:\MaxSegments.txt")) - 1 == 0)
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
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    if (info.Key == ConsoleKey.NumPad4)
                    {
                        if (Convert.ToInt32(File.ReadAllText(@"c:\MaxSegments.txt")) - 1 == 0)
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
