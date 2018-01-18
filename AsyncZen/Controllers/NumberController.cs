using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncZen.Controllers
{
    public class NumberController : ApiController
    {
        [Route("api/number/{i}")]
        public async Task<int> Get(int i)
        {
            await Task.Delay(DateTime.Now.Millisecond);
            return i;
        }
    }
}
