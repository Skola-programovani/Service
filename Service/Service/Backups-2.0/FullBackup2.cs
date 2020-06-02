using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Service
{
    public class FullBackup2
    {
        static FTP ftp = new FTP();
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
 

        //public void Copy(string sourceDirectory, string targetDirectory)
        //{

        //    var diSource = new DirectoryInfo(sourceDirectory);
        //    var diTargetOrigin = new DirectoryInfo(targetDirectory);
        //    var diTarget = new DirectoryInfo(targetDirectory + "@/full");

        //    diTargetOrigin.CreateSubdirectory("full");
        //    CopyAll(diSource, diTarget);
            
        //}
        //public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        //{
        //    Directory.CreateDirectory(target.FullName);
        //    using (StreamWriter sw = new StreamWriter("SnapText.txt"))
        //    {
        //        foreach (FileInfo fi in source.GetFiles())
        //        {
        //            Console.WriteLine(@"Copying {0}{1}", target.FullName, fi.Name);
        //            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        //            File.AppendAllText(@"C:\Temp\SnapText.txt",
        //                  fi.FullName + "," + fi.Attributes + "," + fi.CreationTime.ToString() + "," + fi.GetHashCode() + Environment.NewLine);

        //        }
        //        sw.Close();

        //        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        //        {
        //            DirectoryInfo nextTargetSubDir =
        //                target.CreateSubdirectory(diSourceSubDir.Name);
        //            File.AppendAllText(@"C:\Temp\SnapText.txt",
        //                  diSourceSubDir.FullName + "," + diSourceSubDir.Attributes + "," + diSourceSubDir.CreationTime.ToString() + Environment.NewLine);
        //            CopyAll(diSourceSubDir, nextTargetSubDir);
        //        }
        //        sw.Close();
        //    }
        //}

        public void Copy(string sourceDirectory)
        {

            var diSource = new DirectoryInfo(sourceDirectory);
            ftp.CreateFolder(sourceDirectory);
            CopyAll(diSource);

        }
        public static void CopyAll(DirectoryInfo source)
        {
            ftp.CreateFolder(source.FullName);
            using (StreamWriter sw = new StreamWriter("SnapText.txt"))
            {
                foreach (FileInfo fi in source.GetFiles())
                {
                    ftp.UploadFile(fi.FullName);
                    File.AppendAllText(@"C:\Temp\SnapText.txt",
                          fi.FullName + "," + fi.Attributes + "," + fi.CreationTime.ToString() + "," + fi.GetHashCode() + Environment.NewLine);

                }

                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    ftp.CreateFolder(diSourceSubDir.FullName);
                    File.AppendAllText(@"C:\Temp\SnapText.txt",
                          diSourceSubDir.FullName + "," + diSourceSubDir.Attributes + "," + diSourceSubDir.CreationTime.ToString() + Environment.NewLine);
                    CopyAll(diSourceSubDir);
                }
                sw.Close();
            }
        }

    }
}
