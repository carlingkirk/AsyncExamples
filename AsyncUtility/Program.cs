using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task<string>>();
            for(int i = 0; i < 100; i++)
            {
                tasks.Add(GetNumber(i));
            }

            foreach(var task in tasks)
            {
                var number = await task;
            }
            Console.ReadLine();
        }

        public static async Task<string> GetNumber(int i)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"http://localhost:51579/Api/Number/{i}");
                var number = await response.Content.ReadAsStringAsync();
                Console.WriteLine(number);
                return number;
            }
        }
    }
}
