using System;
using System.Collections;
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
                    try
                    {
                        var client = new HttpClient();

                        var result = await client.GetAsync(args[0]);
                        client.Dispose();

                        if (result.IsSuccessStatusCode) //prawidlowy czyli 2xx
                        {
                            string html = await result.Content.ReadAsStringAsync();
                            var regex = new Regex("[a-z0-9]+@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);
                            int licznik = 0;

                            MatchCollection matches = regex.Matches(html);

                            Hashtable uniMatches = new Hashtable();

                            foreach (var m in matches)
                            {
                                licznik++;
                                string uniMatch = m.ToString();
                                if (!uniMatches.Contains(uniMatch))
                                    uniMatches.Add(uniMatch, string.Empty);
                            }
                            
                            foreach (DictionaryEntry element in uniMatches)
                            {
                                Console.WriteLine(element.Key);
                            }
                                
                            if (licznik == 0)
                            {
                                Console.WriteLine("Nie znaleziono adresow email.");
                            }
                        }  
                    }
                    catch
                    {
                        Console.WriteLine("Wystapil blad podczas ladowania strony");
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
