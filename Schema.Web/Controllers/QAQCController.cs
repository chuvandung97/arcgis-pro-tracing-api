using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("qaqc")]
    public class QAQCController : BaseApiController
    {
        // GET: QAQC
        IQAQCService _qaqcService;
        ILoggingService _loggingService;
        public QAQCController(IQAQCService QAQCService, ILoggingService loggingService)
        {
            _qaqcService = QAQCService;
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("getallusers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllUsersAsync()
        {
            var result = await _qaqcService.GetAllUsersAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getallerrorcategories")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllErrorCategoriesAsync()
        {
            var result = await _qaqcService.GetAllErrorCategoriesAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getlast3montherrors")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLast3MonthErrorsAsync()
        {
            var result = await _qaqcService.GetLast3MonthErrorsAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("gettop10userqaqcerrors")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTop10UserQAQCErrorsAsync(int Year, string Month = null)
        {
            var result = await _qaqcService.GetTop10UserQAQCErrorsAsync(Year, Month);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getallerrorslist")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllErrorsListAsync(int Year, string Month = null, string ErrCatg = null, string Username = null)
        {
            var result = await _qaqcService.GetAllErrorsListAsync(Year, Month, ErrCatg, Username);
            return Ok(result);
        }
    }
}