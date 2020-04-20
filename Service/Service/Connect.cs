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
    public class Command
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }
    class Connect
    {
        static HttpClient client = new HttpClient();

        static void ShowCommand(Command command)
        {
            Console.WriteLine(command.Message);
        }

        static async Task<Uri> CreateAsync(Command command)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/commands", command);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Command> GetAsync(string path)
        {
            Command command = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                command = await response.Content.ReadAsAsync<Command>();
            }
            return command;
        }

        static async Task<Command> UpdateAsync(Command command)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/commands/{command.Id}", command);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            command = await response.Content.ReadAsAsync<Command>();
            return command;
        }

        static async Task<HttpStatusCode> DeleteAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/commands/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            Console.ReadLine();
        }
    }
}
