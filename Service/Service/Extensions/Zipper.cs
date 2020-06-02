using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;


namespace Service
{
    public class Zipper
    {
        public void ZipIt(string sourcePath, string zipPath)
        {
            ZipFile.CreateFromDirectory(sourcePath, zipPath);
        }
    }
}
