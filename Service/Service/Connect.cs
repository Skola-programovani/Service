using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Service
{
    public class Connect
    {
        static HttpClient client = new HttpClient();

        static void ShowKlient(Klient klient)
        {
            Console.WriteLine(klient.name);
        }

        static async Task<Uri> CreateAsync(Klient klient)
        {
       
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/klient", klient);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        static async Task<Klient> GetAsync(string path)
        {
            Klient klient = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                klient = await response.Content.ReadAsAsync<Klient>();
            }
            return klient;
        }
        static async Task<Klient> UpdateAsync(Klient klient)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{klient.id}", klient);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            klient = await response.Content.ReadAsAsync<Klient>();
            return klient;
        }


        public static async Task RunAsync(string command)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.BaseAddress = new Uri("https://localhost:5001/");

            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);

            Klient klient = new Klient
            {
                name = Environment.MachineName,
                confirmed = false,
                MAC = GetMacAddress(),
                IP = GetLocalIPAddress(),
                Description = "testing",
                lastSeen = "never"

            };

            if (command == "POST")
            {
                try
                {
                    var url = await CreateAsync(klient);
                    Console.WriteLine($"Created at {url}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }
            else if(command == "GET")
            {
                try
                {
                    klient = await UpdateAsync(klient);
                    ShowKlient(klient);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }

        }
        static string GetMacAddress()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces, thereby ignoring any
                // loopback devices etc.
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
