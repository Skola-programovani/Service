﻿using System;
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
                writer.SaveID("KlientID",Convert.ToString(KlientByPath(path).Id));
                Console.WriteLine($"Created at {url}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        public static async Task RunTemplateAsync()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.BaseAddress = new Uri("https://localhost:5001/");

            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);
            path = "path";

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);

            Template myTemplate = new Template();
            try
            {
                string id = File.ReadAllText(@"C:\Temp\KlientID.txt", Encoding.UTF8);
                myTemplate = await GetTemplateAsync(id);
                writer.SaveID("TemplateID", Convert.ToString(myTemplate.id));
                if(myTemplate.backup == 1)
                    writer.SaveID("MaxFull", Convert.ToString(myTemplate.retention));
                if (myTemplate.backup == 2)
                    writer.SaveID("MaxDiff", Convert.ToString(myTemplate.retention));
                if (myTemplate.backup == 3)
                    writer.SaveID("MaxIncr", Convert.ToString(myTemplate.retention));
                writer.WriteField(myTemplate.backup, myTemplate.repetition);
                Console.WriteLine("Template:" + myTemplate.name);
                Console.WriteLine("id:" + Convert.ToString(myTemplate.id));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("problém s http");
                Console.WriteLine(e.InnerException.Message);
            }
            catch (System.NullReferenceException d)
            {
                Console.WriteLine("nullová reference");
                Console.WriteLine(d.InnerException.Message);
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
