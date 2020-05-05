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
        public void SaveID(string name,string text)
        {

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(@"C:/", name + ".txt")))
            {
                    outputFile.WriteLine(text);
            }
        }
        public void DecreaseInText(string name)
        {
            string text = null;
            using (StreamReader outputFile = new StreamReader(Path.Combine(@"C:/", name + ".txt")))
            {
                text = outputFile.ReadLine();
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(@"C:/", name + ".txt")))
            {
                int newId = Convert.ToInt32(text) - 1;
                outputFile.WriteLine(newId);
            }

        }
    }
}
