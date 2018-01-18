using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncZen.Controllers
{
    public class CancellationController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var cts = new CancellationTokenSource();
            var task = LongRunningWorkAsync(cts.Token);

            try
            {
                await task;
            }
            catch(OperationCanceledException)
            {
                ViewBag.Message = "Cancelled";
                return View();
            }
            ViewBag.Message = task.Result.ToString();
            return View();
        }

        private async Task<int> LongRunningWorkAsync(CancellationToken token)
        {
            int lines = 0;
            using (var reader = new StreamReader(Server.MapPath("~/Content/bigfile.txt")))
            {
                while (!reader.EndOfStream)
                {
                    token.ThrowIfCancellationRequested();
                    if (lines % 100 == 0)
                        await Task.Delay(10);
                    var line = await reader.ReadLineAsync();
                    lines++; 
                }
            }

            return await Task.FromResult(lines);
        }
    }
}