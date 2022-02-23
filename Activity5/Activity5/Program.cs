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
                    Console.WriteLine("Enter a pokemon Name. Enter nothing to stop");

                    var name = Console.ReadLine();

                    if(string.IsNullOrEmpty(name))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/" + name.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var pokemon = JsonConvert.DeserializeObject<Pokemon>(resultRead);

                    Console.WriteLine("Pokemon Name: " + pokemon.Name);
                    Console.WriteLine("Pokemon Id: " + pokemon.Id);
                }
                catch(Exception)
                {
                    Console.WriteLine("ERROR");
                }
            }
        }
    }

    class Pokemon
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        

    }
}