using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    class ScheduledJob : IScheduledJob
    {
        public void Run(string CronSchedule)
        {
            // Get an instance of the Quartz.Net scheduler
            var schd = GetScheduler();

            // Start the scheduler if its in standby
            if (!schd.IsStarted)
                schd.Start();

            // Define the Job to be scheduled
            var job = JobBuilder.Create<BackupJob>()
                .WithIdentity("Backup", "IT")
                .RequestRecovery()
                .Build();

            // Associate a trigger with the Job
            var trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("Backup", "IT")
                .WithCronSchedule(CronSchedule) 
                .StartAt(DateTime.UtcNow)
                .WithPriority(1)
                .Build();



            var schedule = schd.ScheduleJob(job, trigger);

        }

        // Get an instance of the Quartz.Net scheduler
        private static IScheduler GetScheduler()
        {
            try
            {
                var properties = new NameValueCollection();
                properties["quartz.scheduler.instanceName"] = "ServerScheduler";

                // set remoting expoter
                properties["quartz.scheduler.proxy"] = "true";
                properties["quartz.scheduler.proxy.address"] = string.Format("tcp://{0}:{1}/{2}", "localhost", "555",
                                                                             "QuartzScheduler");

                // Get a reference to the scheduler
                var sf = new StdSchedulerFactory(properties);

                return sf.GetScheduler();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Scheduler not available: '{0}'", ex.Message);
                throw;
            }
        }
    }
}
