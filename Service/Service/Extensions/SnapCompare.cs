using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SnapCompare
    {
        static Writer myWriter = new Writer();
        List<string[]> comparelist = myWriter.ReadSnap(@"C:\Temp\SnapText.txt");

        public bool IsSnappedFile(string path, string created, string hash)
        {
            bool result = false;
            foreach(string[] record in this.comparelist)
            {
                if(record[0] == path)
                {
                    if(record[1] == created)
                    {
                        if(record[2] == hash)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
        public bool IsSnappedDir(string path, string created)
        {
            bool result = false;
            foreach (string[] record in this.comparelist)
            {
                if (record[0] == path)
                {
                    if (record[1] == created)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
