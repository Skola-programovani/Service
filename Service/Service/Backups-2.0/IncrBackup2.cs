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
        static SnapCompare myCompare = new SnapCompare();
        public void Copy(string sourceDirectory, string targetDirectory)
        {
            var target = new DirectoryInfo(targetDirectory + @"\incr");
            var source = new DirectoryInfo(sourceDirectory);

            foreach (FileInfo fi in source.GetFiles())
            {
                if (!myCompare.IsSnappedFile(fi.FullName, fi.CreationTime.ToString(), Convert.ToString(fi.GetHashCode())))
                {
                    var replacementPath = fi.FullName.Replace(targetDirectory, sourceDirectory);
                    fi.CopyTo(replacementPath);
                }
            }

            foreach (DirectoryInfo di in source.GetDirectories())
            {
                if (!myCompare.IsSnappedFile(di.FullName, di.CreationTime.ToString(), Convert.ToString(di.GetHashCode())))
                {
                    target.CreateSubdirectory(di.Name);
                }
                Copy(di.FullName, target.FullName);
            }
        }
        void UpdateSnapshot(DirectoryInfo source)
        {
            File.WriteAllText(@"C:\Snap\SnapText.txt", String.Empty);
            using (StreamWriter sw = new StreamWriter("SnapText.txt"))
            {
                foreach (FileInfo fi in source.GetFiles())
                {

                    File.AppendAllText(@"C:\Snap\SnapText.txt",
                          fi.FullName + "," + fi.Attributes + "," + fi.CreationTime.ToString() + "," + fi.GetHashCode() + Environment.NewLine);

                }
                sw.Close();

                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {

                    File.AppendAllText(@"C:\Snap\SnapText.txt",
                          diSourceSubDir.FullName + "," + diSourceSubDir.Attributes + "," + diSourceSubDir.CreationTime.ToString() + Environment.NewLine);
                    UpdateSnapshot(diSourceSubDir);
                }
                sw.Close();
            }
        }

    }
}
