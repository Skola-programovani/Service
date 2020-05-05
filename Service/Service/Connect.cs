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
using Service.Models;
using System.IO;

namespace Service
{
    public class Connect
    {
        static HttpClient client = new HttpClient();
        static Writer writer = new Writer();
        public static string path = null;

        static async Task<Uri> CreateAsync(Klient klient)
        {
       
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/klient", klient);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public static async Task RunCreateAsync()
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
                MAC = LocalProperties.GetMacAddress(),
                IP = LocalProperties.GetLocalIPAddress(),
                Description = "testing",
                lastSeen = "never"

            };
            try
            {
                var url = await CreateAsync(klient);
                path = url.PathAndQuery;
                writer.SaveID("Klient",Convert.ToString(KlientByPath(path).Id));
                Console.WriteLine($"Created at {url}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            Console.ReadLine();
        }
        public static async Task RunTemplateAsync()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.BaseAddress = new Uri("https://localhost:5001/");

            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);

            Template myTemplate = new Template();
            try
            {
                string id = File.ReadAllText(@"c:\KlientID.txt", Encoding.UTF8);
                myTemplate = await GetTemplateAsync(id);
                writer.SaveID("Template", Convert.ToString(myTemplate.id));
                Console.WriteLine("Zapsano id Templatu:" + Convert.ToString(myTemplate.id));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            Console.ReadLine();
        }
        public static async Task<Klient> KlientByPath(string path)
        {
            Klient klient = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                klient = await response.Content.ReadAsAsync<Klient>();
            }
            return klient;
        }
        static async Task<Template> GetTemplateAsync(string id)
        {
            Template template = null;
            HttpResponseMessage response = await client.GetAsync($"api/Templatelink/user/{id}");
            if (response.IsSuccessStatusCode)
            {
                template = await response.Content.ReadAsAsync<Template>();
            }
            return template;
        }
    }
}
