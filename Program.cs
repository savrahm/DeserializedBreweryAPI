using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace DeserializedBrewery
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            //Random Beer
            var randomBeerResponse = client.GetStringAsync("http://api.brewerydb.com/v2/beer/random/?key=334037ca4bf3bf3948a873fba742951c").Result;
            JToken randomBeer = JToken.Parse(randomBeerResponse);
            var randomBeerName = randomBeer.SelectToken("data.name").ToString();
            var randomBeerId = randomBeer.SelectToken("data.id").ToString();
            var randomBeerAbv = randomBeer.SelectToken("data.abv").ToString();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Random beer:");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"Beer name: {randomBeerName}, ID: {randomBeerId}, ABV: {randomBeerAbv}");


            //BeerList
            var beerResponse = client.GetStringAsync("http://api.brewerydb.com/v2/beers/?key=334037ca4bf3bf3948a873fba742951c").Result;
            var beers = JsonConvert.DeserializeObject<BeerList>(beerResponse);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("All beers:");
            Console.ResetColor();
            Console.WriteLine();
            foreach (var thing in beers.data)
            {
                Console.WriteLine($"Beer name: {thing.name}, ID: {thing.id}, ABV: {thing.abv}");
            }
        }
    }
}
