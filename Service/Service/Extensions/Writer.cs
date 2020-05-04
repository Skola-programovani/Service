using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Writer
    {
        public void SaveID(string text)
        {

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(@"C:/", "KlientID.txt")))
            {
                    outputFile.WriteLine(text);
            }
        }
    }
}
