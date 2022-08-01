using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schema.Core.Services;
using System.Web.Http;
using System.Threading.Tasks;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;

namespace Schema.Web.Controllers
{
    [RoutePrefix("poverification")]
    public class POVerificationController : BaseApiController
    {
        IPOVerificationService _poVerificationService;
        ILoggingService _loggingService;
        public POVerificationController(IPOVerificationService POVerificationService, ILoggingService loggingService)
        {
            _poVerificationService = POVerificationService;
            _loggingService = loggingService;
        }
        //added by Sandip on 1st Sept 2019 for RFC0021956 -- This api returns the complete list of PO Verification IDs that are assigned to the logged in Project Officer
        [CustomAuthorize]
        [Route("getpovjobinfoforpoid")]
        public async Task<IHttpActionResult> GetPOJobInfoAsync()
        {
            string ProjectOfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = ProjectOfficerID.Split('\\');
            ProjectOfficerID = splitString[splitString.Length - 1].Trim();

            var result = await _poVerificationService.GetPOJobInfoAsync(ProjectOfficerID);
            return Ok(result);
        }       
        //added by Sandip on  8th Sept 2019 for RFC0021956 -- When PO approves/rejects As-Built data in Schema application, the same will be updated in PostgreSQL database.
        [CustomAuthorize]
        [Route("updatepovjobinfo")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOJobInfoAsync(object JsonObj)
        {
            /*string ProjectOfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = ProjectOfficerID.Split('\\');
            ProjectOfficerID = splitString[splitString.Length - 1].Trim();*/

            var httpContext = HttpContext.Current;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
            string ProjectOfficerID = user.DisplayName;

            var result = await _poVerificationService.UpdatePOJobInfoAsync(JsonObj, ProjectOfficerID);
            return Ok(result);
        }
        //added by Sandip on  8th Nov 2019 for RFC0021956 -- Allow PO to upload multiple files using Schema application.
        [CustomAuthorize]
        [Route("uploadpovimageorpdf")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOVImageOrPDFFileAsync(object JsonObj)
        {
            var result = await _poVerificationService.UpdatePOVImageOrPDFFileAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on  19th Sept 2019 for RFC0021956 -- This api will be called from GEMS, based on jobstatus it will return true or false.
        [CustomAuthorize]
        [Route("getpovgemsenablepo")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPOVGemsEnablePOButtonAsync(Int64 SessionID)
        {            
            var result = await _poVerificationService.GetPOVGemsEnablePOButtonAsync(SessionID);
            return Ok(result);
        }
        //added by Sandip on  20th Sept 2019 for RFC0021956 -- This api will be called from GEMS, based on jobstatus it will return true or false.
        [CustomAuthorize]
        [Route("getpovgemsenablereconcile")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPOVGemsEnableReconcileButtonAsync(Int64 SessionID)
        {
            var result = await _poVerificationService.GetPOVGemsEnableReconcileButtonAsync(SessionID);
            return Ok(result);
        }
        //added by Sandip on  9th Oct 2019 for RFC0021956 -- This api will be called from GEMS, it will return the current job status of each session.
        [CustomAuthorize]
        [Route("getpovgemsjobstatus")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPOVGemsJobStatusAsync(Int64 SessionID)
        {
            var result = await _poVerificationService.GetPOVGemsJobStatusAsync(SessionID);
            return Ok(result);
        }
        //added by Sandip on  25th Sept 2019 for RFC0021956 -- This api will be called from GEMS, it will update various stages of landbase status. 
        [CustomAuthorize]
        [Route("updatepovgemslandbasestatus")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOVGemsLandbaseStatusAsync(object JsonObj)
        {
            var result = await _poVerificationService.UpdatePOVGemsLandbaseStatusAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on  30th Sept 2019 for RFC0021956 -- This api will be called from GEMS, when MEA wants to reassign the Session back to PO for his/her verification.
        [CustomAuthorize]
        [Route("updatepovgemsmeareassign")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOVGemsMEAReAssignAsync(object JsonObj)
        {
            var result = await _poVerificationService.UpdatePOVGemsMEAReAssignAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on  24th Sept 2019 for RFC0021956 -- This api will be called from GEMS, it will update the jobstatus when the data is finally posted in GEMS.
        [CustomAuthorize]
        [Route("updatepovgemspoststatus")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOVGemsPostStatusAsync(object JsonObj)
        {
            var result = await _poVerificationService.UpdatePOVGemsPostStatusAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on  27th Sept 2019 for RFC0021956 -- This api will be called from GEMS to update the PDFName against session name.
        [CustomAuthorize]
        [Route("updatepovgemspdfname")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOVGemsPDFNameAsync(object JsonObj)
        {
            var result = await _poVerificationService.UpdatePOVGemsPDFNameAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on  5th Nov 2019 for RFC0021956 -- This api will be called from GEMS to update the MEA Editor details for each session ids.
        [CustomAuthorize]
        [Route("updatepovgemsmeaid")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOVGemsMEAIDAsync(object JsonObj)
        {
            var result = await _poVerificationService.UpdatePOVGemsMEAIDAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on  20th July 2021 for RFC0034115 -- This api will be called from GEMS, it will update totalfeatures, totallbfeatures, totallberror and qaqcpct.       
        [CustomAuthorize]
        [Route("updatepovgemsqaqcpct")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOVGemsQAQCPctAsync(object JsonObj)
        {
            var result = await _poVerificationService.UpdatePOVGemsQAQCPctAsync(JsonObj);
            return Ok(result);
        }
    }
}