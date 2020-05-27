using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Service
{
    public class Report
    {
        //static Writer myWriter = new Writer();
        /*
         V tom RunRepAsync jsem nevěděl jak budou pojmenovaný ty columns v té databázi tak jsem to hodil jako příklad.
        */

        static HttpClient client = new HttpClient();

        static async Task<Uri> CreateRepAsync(Klient klient)
        {

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/klient", klient);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        static async Task<Klient> GetKlientAsync(string id)
        {
            Klient klient = null;
            HttpResponseMessage response = await client.GetAsync($"api/Klient/user/{id}");
            if (response.IsSuccessStatusCode)
            {
                klient = await response.Content.ReadAsAsync<Klient>();
            }
            return klient;
        }

        static async Task<Klient> PutKlientAsync(Klient klient)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/klient/{klient.id}", klient);
            response.EnsureSuccessStatusCode();

            klient = await response.Content.ReadAsAsync<Klient>();
            return klient;
        }

        static async Task RunRepAsync()
        {
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Klient klient = new Klient();
                klient.name = ("nevim co tu dělám");
                await PutKlientAsync(klient);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }




    }
}
