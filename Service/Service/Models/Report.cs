using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Report
    {
        public int idReport { get; set; }
        public int idTemplateLink { get; set; }
        public int idTemplate { get; set; }
        public int idKlient { get; set; }
        public DateTime JobStart { get; set; }
        public DateTime JobEnd { get; set; }
        public string FileBackedup { get; set; }
        public string Result { get; set; }
        public string ErrorInfo { get; set; }
    }
}
