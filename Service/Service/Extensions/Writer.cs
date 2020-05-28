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
            using (StreamWriter sw = File.CreateText(@"C:\Temp\" + name + ".txt"))
            {

            }
            using (StreamWriter outputFile = new StreamWriter(@"C:\Temp\" + name + ".txt"))
            {
                    outputFile.WriteLine(text);
            }
            Console.WriteLine("id copied to: " + Path.Combine(@"C:\Temp\", name + ".txt"));
        }
        public void DecreaseInText(string name)
        {
            string text = null;
            using (StreamReader outputFile = new StreamReader(Path.Combine(@"C:\Temp\", name + ".txt")))
            {
                text = outputFile.ReadLine();
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(@"C:\Temp\", name + ".txt")))
            {
                int newId = Convert.ToInt32(text) - 1;
                outputFile.WriteLine(newId);
            }

        }
        public string Read(string name)
        {
            string text = null;
            using (StreamReader outputFile = new StreamReader(Path.Combine(@"C:\Temp\", name + ".txt")))
            {
                text = outputFile.ReadLine();
            }
            return text;
        }
        public List<string[]> ReadSnap(string path)
        {
            List<string[]> output = new List<string[]>();

            string[] lines = File.ReadAllLines(@"C:\Temp\MyFile.txt");

            foreach (string line in lines)
            {
                string[] sline = line.Split(Convert.ToChar(","));
                output.Add(sline);
            }

            return output;
        }
        public void WriteField(int typ,string[] text)
        {
            string name = null;
            if (typ == 1)
                name = @"C:\Temp\FullRepet.txt";
            else if(typ == 2)
                name = @"C:\Temp\DiffRepet.txt";
            else if(typ == 3)
                name = @"C:\Temp\IncrRepet.txt";
            if (File.Exists(name))
            {
                File.Delete(name);
            }
            File.Create(name);
            File.WriteAllLines(name, text);
        }
        public string[] ReadField(string path)
        {
            string[] output = File.ReadAllLines(path);
            return output;
        }
    }
}
