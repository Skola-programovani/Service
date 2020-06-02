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
        static FTP ftp = new FTP();
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
    }
}
