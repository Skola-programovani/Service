using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FTP
    {
        string host = "ftp://localhost/";
        string UserId = "FTP-Host";
        string Password = "123456";
        string currentBackup = null;

        public void UpdateCurrentBackup()
        {
            currentBackup = "backup" + Convert.ToString(DateTime.Now);
        }
        public void CreateFolder(string path)
        {     
            try
            {
                WebRequest request = WebRequest.Create(host + currentBackup + path);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(UserId, Password);
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UploadFile(string path)
        {
            string From = path;
            string To = host + currentBackup + path;

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(UserId, Password);
                client.UploadFile(To, WebRequestMethods.Ftp.UploadFile, From);
            }
        }

    }
}
