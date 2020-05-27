using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace Service
{
    public class Overseer
    {
        DiffBackup2 diff = new DiffBackup2();
        FullBackup2 full = new FullBackup2();
        IncrBackup2 incr = new IncrBackup2();
        Writer myWriter = new Writer();
        public void Run()
        {
            if (File.Exists(@"C:\Users\pc\Desktop\FullRepet.txt"))
            {
                foreach(string datetime in myWriter.ReadField(@"C:\Users\pc\Desktop\FullRepet.txt"))
                {
                    if(datetime == Convert.ToString(DateTime.Now))
                    {
                        if (Convert.ToInt32(File.ReadAllText(@"C:\Users\pc\Desktop\MaxFull.txt")) - 1 == 0)
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
            }
            if (File.Exists(@"C:\Users\pc\Desktop\DiffRepet.txt"))
            {
                foreach (string datetime in myWriter.ReadField(@"C:\Users\pc\Desktop\DiffRepet.txt"))
                {
                    if (datetime == Convert.ToString(DateTime.Now))
                    {
                        if (Convert.ToInt32(File.ReadAllText(@"C:\Users\pc\Desktop\MaxDiff.txt")) - 1 == 0)
                        {
                            myWriter.DecreaseInText("MaxDiff");
                            diff.Copy(@"C:\1", @"C:\2");
                            Console.WriteLine("done");
                        }
                        else
                        {
                            Console.WriteLine("přesažen limit DiffBackupu");
                        }
                    }
                }
            }
            if (File.Exists(@"C:\Users\pc\Desktop\IncrRepet.txt"))
            {
                foreach (string datetime in myWriter.ReadField(@"C:\Users\pc\Desktop\IncrRepet.txt"))
                {
                    if (datetime == Convert.ToString(DateTime.Now))
                    {
                        if (Convert.ToInt32(File.ReadAllText(@"C:\Users\pc\Desktop\MaxIncr.txt")) - 1 == 0)
                        {
                            myWriter.DecreaseInText("MaxIncr");
                            incr.Copy(@"C:\1", @"C:\2");
                            Console.WriteLine("done");
                        }
                        else
                        {
                            Console.WriteLine("přesažen limit IncrBackupu");
                        }
                    }
                }
            }
        }
    }
}
