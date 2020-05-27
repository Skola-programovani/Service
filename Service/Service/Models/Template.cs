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
        public string name { get; set; }
        public int backup { get; set; }
        public string format { get; set; }
        public int retention { get; set; }
        public int sources { get; set; }
        public int destinations { get; set; }
        public string[] repetition { get; set; }

    }
}
