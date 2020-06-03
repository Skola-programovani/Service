using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Report
    {
        public int id { get; set; }
        public string message { get; set; }
        public int idKlient { get; set; }
        public int idTemplate { get; set; }
        public string status { get; set; }
        public DateTime sentTime { get; set; }
        public int idTemplateLink { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string fileBackedUp { get; set; }
    }
}
