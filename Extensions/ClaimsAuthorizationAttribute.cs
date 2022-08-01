using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Schema.Web.Extensions
{
    public class ClaimsAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly string _defaultClaimType = "permission";
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthorized = true;

            ClaimsPrincipal principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (string.IsNullOrEmpty(ClaimType))
            {
                ClaimType = _defaultClaimType;
            }

            if (principal == null || !principal.Identity.IsAuthenticated)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized);
                isAuthorized = false;
            }
            else if (!string.IsNullOrEmpty(ClaimValue))
            {
                if (principal.Claims != null && principal.Claims.Count(claim => string.Compare(claim.Type, ClaimType, true) == 0) > 0)
                {
                    bool foundMatch = false;

                    foreach (var claim in principal.Claims.Where(claim => string.Compare(claim.Type, ClaimType, true) == 0))
                    {
                        Regex regex = new Regex(WildcardToRegex(claim.Value), RegexOptions.IgnoreCase);
                        if (regex.IsMatch(ClaimValue))
                        {
                            foundMatch = true;
                            break;
                        }
                    }

                    if (!foundMatch)
                    {
                        isAuthorized = false;
                    }
                }
                else
                {
                    // No permission claims? Not authorized
                    isAuthorized = false;
                }
            }


            //If user is not authorized, log the authorization attempt
            if (!isAuthorized)
            {
                //LogFailedAuthorization(principal);
            }

            return isAuthorized;
        }

        private static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern)
                              .Replace(@"\*", ".*")
                              .Replace(@"\?", ".")
                       + "$";
        }
    }
}