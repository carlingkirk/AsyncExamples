using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncZen.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Zen()
        {
            var task = GetZenResponseAsync();

            ExpensiveCPUBoundWork();

            var response = await task;

            ViewBag.Message = await response.Content.ReadAsStringAsync();
            
            return View();
        }

        private async Task<HttpResponseMessage> GetZenResponseAsync()
        {
            await Task.Delay(10000);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");

                return await client.GetAsync("https://api.github.com/zen");
            }
        }

        private void ExpensiveCPUBoundWork()
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

        public async Task<ActionResult> Contexts()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");

                System.Web.HttpContext.Current.Cache["test"] = 123;

                var response = await client.GetAsync("https://api.github.com/zen").ConfigureAwait(true);

                var item = System.Web.HttpContext.Current.Cache["test"];

                ViewBag.Message = await response.Content.ReadAsStringAsync();
            }
            return View();
        }
    }
}