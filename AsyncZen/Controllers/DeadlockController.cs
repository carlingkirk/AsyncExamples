using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncZen.Controllers
{
    public class DeadlockController : Controller
    {
        // GET: Deadlock
        public ActionResult Index()
        {
            var task = GetStringAsync();
            ViewBag.Message = task.Result;
            return View();
        }

        public async Task<string> GetStringAsync()
        {
            await Task.Delay(100);
            return await Task.FromResult("Hello");
        }
    }
}