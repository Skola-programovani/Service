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
        bool switcher = true;
        public void Run()
        {
            if (switcher)
            {
                if (File.Exists(@"C:\Temp\FullRepet.txt"))
                {
                    Console.WriteLine("Provádí se Full Backup");
                    foreach (string datetime in myWriter.ReadField(@"C:\Temp\FullRepet.txt"))
                    {
                        if (datetime == Convert.ToString(DateTime.Now))
                        {
                            if (Convert.ToInt32(File.ReadAllText(@"C:\Temp\MaxFull.txt")) == 0)
                            {
                                myWriter.DecreaseInText("MaxFull");
                                full.Copy(@"C:\1");
                                Console.WriteLine("done");
                                switcher = false;
                            }
                            else
                            {
                                Console.WriteLine("přesažen limit FullBackupu");
                            }
                        }
                    }
                }
            }
            else
            {
                if (File.Exists(@"C:\Temp\DiffRepet.txt"))
                {
                    Console.WriteLine("Provádí se Diff Backup");
                    foreach (string datetime in myWriter.ReadField(@"C:\Temp\DiffRepet.txt"))
                    {
                        if (datetime == Convert.ToString(DateTime.Now))
                        {
                            if (Convert.ToInt32(File.ReadAllText(@"C:\Temp\MaxSegments.txt")) != 0)
                            {
                                myWriter.DecreaseInText("MaxSegments");
                                diff.Copy(@"C:\1");
                                Console.WriteLine("done");
                                
                            }
                            else
                            {
                                Console.WriteLine("přesažen limit Segmentů");
                                switcher = true;
                                myWriter.Refill("MaxSegments");
                            }
                        }
                    }
                }
                if (File.Exists(@"C:\Temp\IncrRepet.txt"))
                {
                    Console.WriteLine("Provádí se Incremental Backup");
                    foreach (string datetime in myWriter.ReadField(@"C:\Temp\IncrRepet.txt"))
                    {
                        if (datetime == Convert.ToString(DateTime.Now))
                        {
                            if (Convert.ToInt32(File.ReadAllText(@"C:\Temp\MaxSegments.txt")) != 0)
                            {
                                myWriter.DecreaseInText("MaxSegments");
                                incr.Copy(@"C:\1");
                                incr.UpdateSnapshot(@"C:\1");
                                Console.WriteLine("done");

                            }
                            else
                            {
                                Console.WriteLine("přesažen limit Segmentů");
                                switcher = true;
                                myWriter.Refill("MaxSegments");
                            }
                        }
                    }
                }
            }
            
        }
    }
}
