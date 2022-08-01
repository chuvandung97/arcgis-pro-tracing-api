using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;

namespace Schema.Web.Controllers
{
    [RoutePrefix("gasinternalpipedrawings")]
    public class GasInternalPipeDrawingsController : BaseApiController
    {
        // GET: GasInternalPipeDrawings

        IGasInternalPipeDrawingsService _gasInternalPipeDrawingsService;
        ILoggingService _loggingService;
        public GasInternalPipeDrawingsController(IGasInternalPipeDrawingsService gasInternalPipeDrawingsService, ILoggingService loggingService)
        {
            _gasInternalPipeDrawingsService = gasInternalPipeDrawingsService;
            _loggingService = loggingService;
        }

        // added by Tulasi on 15/09/2021 for RFC0035510 --  Validate Postal code(Postal code valid or not, if existing postal code bring the details)
        [CustomAuthorize]
        [Route("getvalidatepostalcode")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGasInternalPipeDrawingsAsync(string PostalCode)
        {
            var result = await _gasInternalPipeDrawingsService.GetGasInternalPipeDrawingsAsync(PostalCode);
            return Ok(result);
        }
        // added by Tulasi on 15/09/2021 for RFC0035510 --   get the complete history of postal codes
        [CustomAuthorize]
        [Route("getgasinternalpipedrawingshistory")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGasInternalPipeDrawingsHistoryAsync()
        {
            var result = await _gasInternalPipeDrawingsService.GetGasInternalPipeDrawingsHistoryAsync();
            return Ok(result);
        }
        // added by Tulasi on 15/09/2021 for RFC0035510 --   get the details based on postal code
        [CustomAuthorize]
        [Route("getgasinternalpipedrawingsbypostalcode")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGasInternalPipeDrawingsByPostalCodeAsync(string PostalCode)
        {
            var result = await _gasInternalPipeDrawingsService.GetGasInternalPipeDrawingsByPostalCodeAsync(PostalCode);
            return Ok(result);
        }
        // added by Tulasi on 15/09/2021 for RFC0035510 -- Upload PDF Documents(Insert)
        [CustomAuthorize]
        [Route("uploadpdf")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePDFFileAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _gasInternalPipeDrawingsService.UpdatePDFFileAsync(JsonObj, UserID);
            return Ok(result);
        }
        // added by Tulasi on 15/09/2021 for RFC0035510 --  Delete PDF documents(Delete)
        [CustomAuthorize]
        [Route("deletepdf")]
        [HttpPost]
        public async Task<IHttpActionResult> DeletePDFFileAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _gasInternalPipeDrawingsService.DeletePDFFileAsync(JsonObj, UserID);
            return Ok(result);
        }
        // added by Tulasi on 15/09/2021 for RFC0035510 --  Post the postal code details(Insert)
        [CustomAuthorize]
        [Route("insertgasinternalpipedrawingsinfo")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateGasInternalPipeDrawingsAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _gasInternalPipeDrawingsService.CreateGasInternalPipeDrawingsAsync(JsonObj, UserID);
            return Ok(result);
        }

        // added by Tulasi on 15/09/2021 for RFC0035510 --  Update drawing info
        [CustomAuthorize]
        [Route("updategasinternalpipedrawingsinfo")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateGasInternalPipeDrawingsAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _gasInternalPipeDrawingsService.UpdateGasInternalPipeDrawingsAsync(JsonObj, UserID);
            return Ok(result);
        }
        // added by Tulasi on 15/09/2021 for RFC0035510 -- Delete the Postal code details(Delete)
        [CustomAuthorize]
        [Route("deletegasinternalpipedrawingsinfo")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteGasInternalPipeDrawingsAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _gasInternalPipeDrawingsService.DeleteGasInternalPipeDrawingsAsync(JsonObj, UserID);
            return Ok(result);
        }
    }
}