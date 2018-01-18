using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncZen.Controllers
{
    public class HomeController : Controller
    {
        #region Zen
        public async Task<ActionResult> Zen()
        {
            var task = GetZenResponseAsync();

            var response = await task;

            ViewBag.Message = await response.Content.ReadAsStringAsync();
            
            return View();
        }

        private async Task<HttpResponseMessage> GetZenResponseAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");

                return await client.GetAsync("https://api.github.com/zen");
            }
        }
        #endregion

        #region Big File
        public async Task<ActionResult> BigFile()
        {
            var task = LoadFileAsync();
            ViewBag.Message = await task;
            return View();
        }

        private async Task<string> LoadFileAsync()
        {
            var builder = new StringBuilder();
            using (var reader = new StreamReader(Server.MapPath("~/Content/bigfile.txt")))
            {
                while (!reader.EndOfStream)
                {
                    builder.Append(await reader.ReadLineAsync());
                    builder.Append("<br>");
                }
            }

            return builder.ToString();
        }
        #endregion

        #region Contexts
        public async Task<ActionResult> Contexts()
        {
            System.Web.HttpContext.Current.Cache["UserId"] = 123;

            var response = await GetZenResponseAsync().ConfigureAwait(true);

            var item = System.Web.HttpContext.Current.Cache["UserId"];

            ViewBag.Message = await response.Content.ReadAsStringAsync();
            return View();
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion
    }
}