using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;

namespace Schema.Web.Controllers
{
    [RoutePrefix("loginfo")]
    public class CustomAuthorizeController : BaseApiController
    {
        ICustomAuthorizeService _customAuthorizeService;
        ILoggingService _loggingService;
        // GET: CustomAuthorize
        public CustomAuthorizeController(ICustomAuthorizeService CustomAuthorizeService, ILoggingService loggingService)
        {
            _customAuthorizeService = CustomAuthorizeService;
            _loggingService = loggingService;
        }

        //[CustomAuthorize]
        [Route("insertmapservicerequest")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertMapServiceRequestInDB(object JsonObj)
        {           
            var result = await _customAuthorizeService.InsertMapServiceRequestInDB(JsonObj);
            return Ok(result);
        }
        //[CustomAuthorize]
        [Route("insertmapserviceerror")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertMapServiceErrorInDB(object JsonObj)
        {
            var result = await _customAuthorizeService.InsertMapServiceErrorInDB(JsonObj);
            return Ok(result);
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}