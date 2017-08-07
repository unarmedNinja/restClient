using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace WebAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting WebAPIClient ...");
/* 
            var repositories = ProcessRepositories().Result;
            foreach (var repo in repositories){
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitHubHomeUrl);
                Console.WriteLine(repo.Homepage);
                Console.WriteLine(repo.Watchers);
                Console.WriteLine(repo.LastPush);
                Console.WriteLine();
            }
*/
            string url = "https://api.github.com/orgs/dotnet/repos";
            List<webHeader> headers = new List<webHeader>();
            webHeader h = new webHeader();
            h.name = "User-Agent";
            h.value = ".NET Foundation Repository Reporter";
            headers.Add(h);

            List<string> acceptHeaders = new List<string>();
            acceptHeaders.Add("application/vnd.github.v3+json");


           Api api = new Api();
           var content =  api.getData(url, acceptHeaders, headers).Result;
           Console.WriteLine(content);

        }
    
        private static async Task<List<Repository>> ProcessRepositories()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
          //  Console.Write(msg);

            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));
            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<Repository>;
            return repositories;                
        }
    }
}
