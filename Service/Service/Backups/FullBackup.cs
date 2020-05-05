using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Service
{
    public class FullBackup
    {
        public void NewFull()
        {
            /*string fileName = @"C:/FullBack";
            string directory = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            */

            string foldername = @"C:/Backups";
            string targetDirectory = System.IO.Path.Combine(foldername, "FullBackup");
            System.IO.Directory.CreateDirectory(targetDirectory);
            string fileName = "FirstBackup";
            targetDirectory = System.IO.Path.Combine(targetDirectory, fileName);

            Console.WriteLine("Path to my file: {0}\n", targetDirectory);

        }

        public void Copy(string sourceDirectory, string targetDirectory)
        {

            var diSource = new DirectoryInfo(sourceDirectory);
            var diTargetOrigin = new DirectoryInfo(targetDirectory);
            var diTarget = new DirectoryInfo(targetDirectory + "@/full");

            diTargetOrigin.CreateSubdirectory("full");

            CopyAll(diSource, diTarget);
        }
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}