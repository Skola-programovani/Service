using System;

namespace Service
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DiffBackup diff = new DiffBackup();
            FullBackup full = new FullBackup();
 
            while (true)
            {

                ConsoleKeyInfo info = Console.ReadKey();

                if (info.Key == ConsoleKey.NumPad1)
                    Connect.RunCreateAsync().GetAwaiter().GetResult();
                try
                {
                    if (info.Key == ConsoleKey.NumPad2)
                        full.Copy(@"C:\1", @"C:\2");
                    Console.WriteLine("done");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    if (info.Key == ConsoleKey.NumPad3)
                        diff.Copy(@"C:\1", @"C:\2");
                    Console.WriteLine("done");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    if (info.Key == ConsoleKey.NumPad4)
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
