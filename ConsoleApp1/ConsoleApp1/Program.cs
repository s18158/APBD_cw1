using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("4.1");
            }
            else
            {
                foreach (var a in args)
                {
                    Console.WriteLine(a);
                }

                var emails = await GetEmails(args[0]);
                //var emails = await GetEmails("https://www.pja.edu.pl/");

                foreach (var email in emails)
                {
                    Console.WriteLine(email);
                }
            }

        }

        static async Task<IList<string>> GetEmails(string url)
        {
            var httpClient = new HttpClient();
            //var response = await httpClient.GetAsync(args[0]);
            var listOfEmails = new List<string>();

            var response = await httpClient.GetAsync(url);

            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            RegexOptions.IgnoreCase);

            MatchCollection emailMatches = emailRegex.Matches(response.Content.ReadAsStringAsync().Result);

            foreach (var emailMatch in emailMatches)
            {
                listOfEmails.Add(emailMatch.ToString());
            }

            return listOfEmails;
        }
    }

}