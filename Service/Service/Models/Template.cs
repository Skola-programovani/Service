using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class Template
    {
        public int id { get; set; }
        public int idTemplateLink { get; set; }
        public string name { get; set; }
        public int backup { get; set; }
        public int maxFull { get; set; }
        public int maxSegments { get; set; }
        public string repetition { get; set; }
        public string savedFiles { get; set; }
        public string format { get; set; }
    }
}
