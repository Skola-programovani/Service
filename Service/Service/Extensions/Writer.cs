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
            using (StreamWriter sw = File.CreateText(@"C:\Users\pc\Desktop\" + name + ".txt"))
            {

            }
            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\pc\Desktop\" + name + ".txt"))
            {
                    outputFile.WriteLine(text);
            }
            Console.WriteLine("id copied to: " + Path.Combine(@"C:\Users\pc\Desktop\", name + ".txt"));
        }
        public void DecreaseInText(string name)
        {
            string text = null;
            using (StreamReader outputFile = new StreamReader(Path.Combine(@"C:\Users\pc\Desktop\", name + ".txt")))
            {
                text = outputFile.ReadLine();
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(@"C:\Users\pc\Desktop\", name + ".txt")))
            {
                int newId = Convert.ToInt32(text) - 1;
                outputFile.WriteLine(newId);
            }

        }
        public string Read(string name)
        {
            string text = null;
            using (StreamReader outputFile = new StreamReader(Path.Combine(@"C:\Users\pc\Desktop\", name + ".txt")))
            {
                text = outputFile.ReadLine();
            }
            return text;
        }
    }
}
