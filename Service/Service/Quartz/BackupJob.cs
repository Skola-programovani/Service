using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Common.Logging;
using System.IO;

namespace Service
{
    class BackupJob : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BackupJob));
        DiffBackup diff = new DiffBackup();
        FullBackup full = new FullBackup();
        IncrBackup incr = new IncrBackup();
        Writer myWriter = new Writer();

        /// <summary> 
        /// Empty constructor for job initilization
        /// <para>
        /// Quartz requires a public empty constructor so that the
        /// scheduler can instantiate the class whenever it needs.
        /// </para>
        /// </summary>
        public BackupJob()
        {

        }

        public void Execute(IJobExecutionContext context)
        {
            if(Directory.Exists("@C:/full"))
            {
                if (Convert.ToInt32(File.ReadAllText(@"c:\MaxSegments.txt")) > 0)
                {
                    myWriter.DecreaseInText("MaxSegments");
                    diff.Copy(@"C:\1", @"C:\2");
                    Console.WriteLine("done");
                }
                else
                {
                    Console.WriteLine("přesažen limit MaxSegments");
                }
            }
            else
            {
                if (Convert.ToInt32(File.ReadAllText(@"c:\MaxFull.txt")) > 0)
                {
                    myWriter.DecreaseInText("MaxFull");
                    full.Copy(@"C:\1", @"C:\2");
                    Console.WriteLine("done");
                }
                else
                {
                    Console.WriteLine("přesažen limit MaxFull");
                }
            }
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
