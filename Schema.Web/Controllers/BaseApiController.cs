using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Schema.Web.Controllers
{
   //[Authorize]
    //[EnableCors("*", "*", "*")]
    public class BaseApiController : ApiController
    {
        public BaseApiController()
        {
        }
    }
}
