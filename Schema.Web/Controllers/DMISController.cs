using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("dmis")]
    public class DMISController : BaseApiController
    {
        IDMISService _dmisService;
        ILoggingService _loggingService;
        public DMISController(IDMISService DMISService, ILoggingService loggingService)
        {
            _dmisService = DMISService;
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("createdmis")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateDMISPointAsync(object JsonObj)
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            //WriteErrorLog(Username);
            var result = await _dmisService.CreateDMISPointAsync(JsonObj, Username);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getdmiseditors")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDMISEditorsAsync()
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            var result = await _dmisService.GetDMISEditorsAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getdmisapprovalofficers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetApprovalOfficersAsync()
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            var result = await _dmisService.GetApprovalOfficersAsync(Username);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getdmis")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDMISPointsAsync()
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            //WriteErrorLog(Username);
            var result = await _dmisService.GetDMISPointsAsync(Username);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getdmiswithinextent")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDMISpointsWithinExtentAsync(string Geometry)
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            //WriteErrorLog(Username);
            var result = await _dmisService.GetDMISpointsWithinExtentAsync(Geometry, Username);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getdmisdetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDMISPointDetailsAsync(string OID)
        {
            var result = await _dmisService.GetDMISPointDetailsAsync(OID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("senddmistoapprover")]
        [HttpPost]
        public async Task<IHttpActionResult> SendToApproverAsync(object JsonObj)
        {
            var result = await _dmisService.SendToApproverAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("dmisapprovalprocess")]
        [HttpPost]
        public async Task<IHttpActionResult> ApprovalProcessAsync(object JsonObj)
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();

            //Get complete name of the approver 
            var httpContext = HttpContext.Current;
            IPrincipal principal = httpContext.User as IPrincipal;
            var identity = principal;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
            string ApproverName = user.DisplayName;
            //
            var result = await _dmisService.ApprovalProcessAsync(JsonObj, Username, ApproverName);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("updatedmis")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateDMISPointAsync(object JsonObj)
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            //WriteErrorLog(Username);
            var result = await _dmisService.UpdateDMISPointAsync(JsonObj, Username);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("deletedmis")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteDMISPointAsync(object JsonObj)
        {
            var result = await _dmisService.DeleteDMISPointAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("snapdmistogasline")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPointSnaptoGaslineAsync(double X, double Y, string Layer)
        {
            var result = await _dmisService.GetPointSnaptoGaslineAsync(X, Y, Layer);
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