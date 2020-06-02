using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class IncrBackup2
    {
        FTP ftp = new FTP();
        static SnapCompare myCompare = new SnapCompare();
        public void Copy(string sourceDirectory)
        {
            var source = new DirectoryInfo(sourceDirectory);


            foreach (FileInfo fi in source.GetFiles())
            {
                if (!myCompare.IsSnappedFile(fi.FullName, fi.CreationTime.ToString(), Convert.ToString(fi.GetHashCode())))
                {
                    ftp.UploadFile(fi.FullName);
                }
            }

            foreach (DirectoryInfo di in source.GetDirectories())
            {
                if (!myCompare.IsSnappedFile(di.FullName, di.CreationTime.ToString(), Convert.ToString(di.GetHashCode())))
                {
                    ftp.CreateFolder(di.FullName);
                }
                Copy(di.FullName);
            }

        }

        public void UpdateSnapshot(DirectoryInfo source)
        {

            File.WriteAllText(@"C:\Temp\SnapText.txt", String.Empty);
            using (StreamWriter sw = new StreamWriter("SnapText.txt"))
            {
                foreach (FileInfo fi in source.GetFiles())
                {

                    File.AppendAllText(@"C:\Temp\SnapText.txt",
                          fi.FullName + "," + fi.Attributes + "," + fi.CreationTime.ToString() + "," + fi.GetHashCode() + Environment.NewLine);

                }
                sw.Close();

                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {

                    File.AppendAllText(@"C:\Temp\SnapText.txt",
                          diSourceSubDir.FullName + "," + diSourceSubDir.Attributes + "," + diSourceSubDir.CreationTime.ToString() + Environment.NewLine);
                    UpdateSnapshot(diSourceSubDir);
                }
                sw.Close();
            }
        }

    }
}
