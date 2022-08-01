using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Schema.Web.Providers
{
    public class ADAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //IConfigService _configService;
        //ILoggingService _loggingService;
        //public ADAuthorizationServerProvider(IConfigService configService, ILoggingService loggingService)
        //{
        //    _configService = configService;
        //    _loggingService = loggingService;
        //}

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => { context.Validated(); });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                try
                {
                    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "SINGAPOREPOWER"))
                    {
                        //validate the credentials
                        bool isValid = pc.ValidateCredentials(context.UserName, context.Password);

                        if (!isValid)
                        {
                            context.SetError("invalid_grant", "The user name or password is incorrect.");
                            return;
                        }

                        UserPrincipal user = UserPrincipal.FindByIdentity(pc, context.UserName);

                        if (user != null)
                        {
                            PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();


                            //iterate over all groups
                            foreach (Principal p in groups)
                            {

                                //make sure to add only group principals
                                if (p is GroupPrincipal)
                                {
                                    identity.AddClaim(new Claim(ClaimTypes.Role, ((GroupPrincipal)p).Name));
                                }
                            }
                            identity.AddClaim(new Claim(ClaimTypes.Upn, user.UserPrincipalName));
                            identity.AddClaim(new Claim(ClaimTypes.Name, user.SamAccountName));
                            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.DisplayName));

                            //Get Roles based on AD Group
                            //if (user.SamAccountName == "tsap352u")
                            //    identity.AddClaim(new Claim(ClaimTypes.UserData, "SINGAPOREPOWER\\Electricity Distribution Viewer"));
                            //else if (user.SamAccountName == "ttuv691m")
                            //    identity.AddClaim(new Claim(ClaimTypes.UserData, "SINGAPOREPOWER\\Electricity Transmission Viewer"));
                            //else if (user.SamAccountName == "tsrk726w")
                            //    identity.AddClaim(new Claim(ClaimTypes.UserData, "SINGAPOREPOWER\\Landbase Viewer,SINGAPOREPOWER\\DMIS Writer,SINGAPOREPOWER\\Gas Viewer"));

                            //commented by sandip on 9th Oct 2018
                            /*if (user.SamAccountName == "TBHU820R")
                                identity.AddClaim(new Claim(ClaimTypes.UserData, "DMIS_View"));
                            else if (user.SamAccountName == "TTUV691M")
                                identity.AddClaim(new Claim(ClaimTypes.UserData, "DMIS_View,DMIS_Create,DMIS_Move,DMIS_Update,DMIS_Delete,DMIS_SendForApproal,DMIS_Approve"));
                            else if (user.SamAccountName == "TARS179N")
                                identity.AddClaim(new Claim(ClaimTypes.UserData, "NULL"));*/
                        }
                        var id = new ClaimsIdentity(identity.Claims,
                            Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);

                        var ctx = context.OwinContext;
                        var authenticationManager = ctx.Authentication;
                        authenticationManager.SignIn(id);

                        context.Validated(identity);
                    }
                }
                catch (Exception)
                {
                    // _loggingService.Error(ex);
                }
            });
        }


    }
}