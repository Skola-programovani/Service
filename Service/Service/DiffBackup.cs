using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Service
{
    public class DiffBackup
    {
        public void DirHandler(string pathOld, string pathNew)
        {
            DirectoryInfo dirOld = new DirectoryInfo(pathOld);
            DirectoryInfo dirNew = new DirectoryInfo(pathNew);

            IEnumerable<FileInfo> listOld = dirOld.GetFiles("*.*", SearchOption.AllDirectories);
            IEnumerable<FileInfo> listNew = dirNew.GetFiles("*.*", SearchOption.AllDirectories);

            FileCompare myFileCompare = new FileCompare();

            bool Same = listOld.SequenceEqual(listNew, myFileCompare);

            var queryList1Only = (from file in listNew
                                  select file).Except(listOld, myFileCompare);

            foreach (var v in queryList1Only)
            {
                var replacementPath = v.FullName.Replace(pathNew, pathOld);
                v.CopyTo(replacementPath);
                Console.WriteLine("copied: " + v.Name);
                
            }
            Console.ReadLine();
        }
        class FileCompare : IEqualityComparer<FileInfo>
        {
            public FileCompare() { }

            public bool Equals(FileInfo f1, FileInfo f2)
            {
                return (f1.Name == f2.Name &&
                        f1.Length == f2.Length);
            }

            public int GetHashCode(System.IO.FileInfo fi)
            {
                string s = $"{fi.Name}{fi.Length}";
                return s.GetHashCode();
            }
        }

    }
}
