using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public static async Task RunAsync()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.BaseAddress = new Uri("http://localhost:5000/");

            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);
               


            try
            {
                Klient klient = new Klient
                {
                    id = 1,
                    name = "klient1",
                    confirmed = true,
                    email = "email",
                    MAC = "mac",
                    IP = "ip",
                    Description = "test",
                    lastSeen = "---"

                };

                var url = await CreateAsync(klient);
                Console.WriteLine($"Created at {url}");


                klient = await GetAsync(url.PathAndQuery);
                ShowKlient(klient);


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            Console.ReadLine();

        }
    }
}
