using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Klient
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool confirmed { get; set; }
        public string MAC { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
        public string lastSeen { get; set; }
    }
}
