using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DiffBackup2
    {
        static SnapCompare myCompare = new SnapCompare();
        public void Copy(string sourceDirectory, string targetDirectory)
        {
            var target = new DirectoryInfo(targetDirectory + @"\diff");
            var source = new DirectoryInfo(sourceDirectory + @"\full");

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
                Copy(di.FullName, di.FullName.Replace(sourceDirectory, targetDirectory));
            }
        }
    }
}
