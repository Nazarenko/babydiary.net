using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;
using BabyDiary.Business;
using BabyDiary.Business.Interfaces;

namespace BabyDiary.Controllers
{
    public class BaseController : Controller
    {
//        protected ICurrentUser CurrentUser { get; set; }
//
//        public BaseController(ICurrentUser currentUser)
//        {
//            CurrentUser = currentUser;
//        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
//            var claimsIdentity = User.Identity as ClaimsIdentity;
//            if (claimsIdentity != null)
//            {
//                ViewBag.Username = claimsIdentity.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier);
//            }

            // Grab the user's login information from Identity
                        if (User is ClaimsPrincipal)
                        {
                            var user = User as ClaimsPrincipal;
            //                var claims = user.Claims;
                            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
                            if (claim != null)
                                ViewData.Add("Username", claim.Value);
                            //var id = GetClaim(claims, ClaimTypes.NameIdentifier);
            
                        }

        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}