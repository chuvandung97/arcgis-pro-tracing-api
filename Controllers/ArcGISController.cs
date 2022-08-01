using Microsoft.IdentityModel.WindowsTokenService;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("api/arcgis")]
    public class ArcGISController : ApiController
    {
        ILoggingService _loggingService;
        public ArcGISController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [Route("proxy")]
        [HttpGet]
        public async Task<HttpResponseMessage> ProcessRequest()
        {
            byte[] data = null;
            string Url = Request.RequestUri.Query.Substring(1);
            if (!Url.Contains("?"))
            {
                string newUrl = string.Empty;
                var queryparms = Url.Split('&');
                for (int i = 0; i < queryparms.Length; i++)
                {
                    if (i == 0)
                        newUrl = queryparms[i];
                    else if (i == 1)
                        newUrl += "?" + queryparms[i];
                    else
                        newUrl += "&" + queryparms[i];
                }
                Url = newUrl;
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                ClaimsPrincipal principal = RequestContext.Principal as ClaimsPrincipal;

                if (principal.Claims != null && principal.Claims.Count(claim => string.Compare(claim.Type, ClaimTypes.Upn, true) == 0) > 0)
                {
                    Claim claim = principal.Claims.Where(c => c.Type == ClaimTypes.Upn).FirstOrDefault();
                    string upn = claim.Value;   

                    WindowsIdentity windowsIdentity = S4UClient.UpnLogon(upn);

                    await WindowsIdentity.RunImpersonated(windowsIdentity.AccessToken, async () =>
                    {
                        WebClient client = new WebClient();
                        //client.UseDefaultCredentials = true;
                        data = await client.DownloadDataTaskAsync(Url);
                        result.Content = new ByteArrayContent(data);
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    });
                }
                else
                    result = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                result = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return result;
        }
    }
}
