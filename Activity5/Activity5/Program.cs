using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Activity5
{



    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while(true)
            {
                try
                {
                    Console.WriteLine("Enter anything to generate a random user! Enter nothing to stop.");

                    var name = Console.ReadLine();

                    if(string.IsNullOrEmpty(name))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://randomuser.me/api?gender=");
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var books = JsonConvert.DeserializeObject<Result>(resultRead);

                    foreach(var i in books.Results)
                    {
                        Console.WriteLine("New user information ------");
                        Console.WriteLine("Gender: " + i.Gender);
                        Console.WriteLine("Email: " + i.Email);
                        Console.WriteLine("Cell: " + i.Cell);
                        Console.WriteLine("\n\n");
                    }
                    
                }
                catch(Exception)
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }
    }

    class Result
    {
        [JsonProperty("results")]
        public List<Person> Results { get; set; }
    }


    class Person
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("cell")]
        public string Cell { get; set; }

    }
}