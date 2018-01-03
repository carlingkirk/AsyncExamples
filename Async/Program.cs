using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var task = GetZenResponseAsync();

            ExpensiveCPUBoundWork();

            var response = await task;

            var message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public static Task SimpleTask()
        {
            return Task.Delay(1000);
        }

        private static async Task<HttpResponseMessage> GetZenResponseAsync()
        {
            await Task.Delay(10000);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");

                return await client.GetAsync("https://api.github.com/zen");
            }
        }

        private static void ExpensiveCPUBoundWork()
        {
            var list = Enumerable.Range(0, 10000000).ToList();
            var list2 = Enumerable.Range(10000000, 0).ToList();
            foreach (var item in list)
            {
                if (list2.Contains(item))
                {
                    list2.Add(item);
                }
            }
        }
    }
}
