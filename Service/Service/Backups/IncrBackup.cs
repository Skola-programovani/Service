using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Service
{
    public class IncrBackup
    {
        public void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory + @"\full");
            var diTarget = new DirectoryInfo(targetDirectory);

            DirectoryInfo target = new DirectoryInfo(targetDirectory);
            int dirCount = target.GetDirectories().Length;

            string newFolderName = "incr" + Convert.ToString(dirCount - 1);

            diTarget.CreateSubdirectory(newFolderName);
            CompareDir compare = new CompareDir();
            compare.DirHandler(sourceDirectory + @"\" + FindLatest(targetDirectory), targetDirectory + @"\" + newFolderName);

            foreach (DirectoryInfo diSourceSubDir in diSource.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                compare.DirHandler(nextTargetSubDir.FullName, nextTargetSubDir.FullName.Replace(sourceDirectory, targetDirectory));
            }
        }
        string FindLatest(string path)
        {
            string final = null;
            string[] tempString = Directory.GetDirectories(path);;
            List<int> ints = new List<int>();
            foreach(string item in tempString)
            {
                ints.Add(Convert.ToInt32(item.Remove(0, 4)));               
            }
            final = "diff" + ints.Max();
            return final;
        }
    }
}
