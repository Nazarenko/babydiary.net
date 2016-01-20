using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using BabyDiary.Business.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;

namespace BabyDiary
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(options: new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/SignIn"),
                // TODO SSL on production
                //                CookieSecure = CookieSecureOption.Always
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = async context =>
                    {
                        var reject = false;
//                        if (context.Request.Method == "POST")
//                        {
                            IUserProvider userProvider = DependencyResolver.Current.GetService(typeof (IUserProvider)) as IUserProvider;
                            // TODO exception if null
                            if (userProvider != null)
                                reject = !await userProvider.LoadUserBySidAsync(
                                            context.Identity.FindFirstValue(ClaimTypes.Sid));
//                        }
                        if (reject)
                        {
                            context.OwinContext.Authentication.SignOut(context.Options.AuthenticationType);
                        }
                    }
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
