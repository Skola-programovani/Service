using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Common.Logging;



namespace Service
{
    class HelloWorldJob : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HelloWorldJob));

        /// <summary> 
        /// Empty constructor for job initilization
        /// <para>
        /// Quartz requires a public empty constructor so that the
        /// scheduler can instantiate the class whenever it needs.
        /// </para>
        /// </summary>
        public HelloWorldJob()
        {

        }

        public void Execute(IJobExecutionContext context)
        {
            
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
