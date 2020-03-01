using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace cw1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var result = await client.GetAsync("https://www.pja.edu.pl");

            if (result.IsSuccessStatusCode) //prawidlowy czyli 2xx
            {
                string html = await result.Content.ReadAsStringAsync();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
