using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            if (args.Length > 0) //czy podany argument - adres url
            {

                if (Uri.IsWellFormedUriString(args[0], UriKind.Absolute)) //czy podany argument/adres jest poprawny
                {
                    var client = new HttpClient();

                    var result = await client.GetAsync(args[0]);

                    if (result.IsSuccessStatusCode) //prawidlowy czyli 2xx
                    {
                        string html = await result.Content.ReadAsStringAsync();
                        var regex = new Regex("[a-z0-9]+@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);


                        MatchCollection matches = regex.Matches(html);
                        foreach (var m in matches)
                        {
                            Console.WriteLine(m);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Podano zly adres!!");
                }

            }
            else
            {
                throw new ArgumentNullException("Nie podano adresu URL!!");
            }



            Console.WriteLine("Koniec!");
        }
    }
}
