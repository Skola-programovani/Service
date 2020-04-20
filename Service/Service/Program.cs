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
    class Program
    {
        public class Klient
        {
            public int id { get; set; }

            public string name { get; set; }

            public bool confirmed { get; set; }

            public string email { get; set; }

            public string MAC { get; set; }

            public string IP { get; set; }

            public string Description { get; set; }

            public string lastSeen { get; set; }
        }

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static HttpClient client = new HttpClient();

        static void ShowKlient(Klient klient)
        {
            Console.WriteLine(klient.name);
        }

        static async Task<Uri> CreateAsync(Klient klient)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Klient", klient);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
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
                $"api/Klient/{klient.id}", klient);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            klient = await response.Content.ReadAsAsync<Klient>();
            return klient;
        }

        static async Task<HttpStatusCode> DeleteAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Klient/{id}");
            return response.StatusCode;
        }


        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Klient klient = new Klient
                {
                    id = 5,
                    name = "klient1",
                    confirmed = true,
                    email = "email",
                    MAC = "mac",
                    IP = "ip",
                    Description  = "test",
                    lastSeen = "---"
                    
                };

                var url = await CreateAsync(klient);
                Console.WriteLine($"Created at {url}");

                // Get the product
                klient = await GetAsync(url.PathAndQuery);
                ShowKlient(klient);

           
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
