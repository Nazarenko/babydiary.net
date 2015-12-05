using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using BabyDiary.Business.Interfaces;
using BabyDiary.Models.DTOs;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Resources;

namespace BabyDiary.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserProvider _userProvider;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        // GET: SignUp
        [Authorize]
        public ActionResult SignUp()
        {
            //            HttpApplication httpApps = HttpContext.ApplicationInstance;
            //            //Get List of modules in module collections
            //            HttpModuleCollection httpModuleCollections = httpApps.Modules;
            //            Response.Write("Total Number Active HttpModule : " + httpModuleCollections.Count.ToString() + "</br>");
            //            Response.Write("<b>List of Active Modules</b>" + "</br>");
            //            foreach (string activeModule in httpModuleCollections.AllKeys)
            //            {
            //                Response.Write(activeModule + "</br>");
            //            }
            return View();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // Grab the user's login information from Identity
            if (User is ClaimsPrincipal)
            {
                var user = User as ClaimsPrincipal;
                var claims = user.Claims.ToList();

                //var name = GetClaim(claims, ClaimTypes.Name);
                //var id = GetClaim(claims, ClaimTypes.NameIdentifier);

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpDto user)
        {
            if (!_userProvider.IsEmailAvailable(user.Email))
            {
                ModelState.AddModelError("Email", ValidationMessages.ResourceManager.GetString("SignUpEmailRemote"));
            }
            if (!_userProvider.IsLoginAvailable(user.Login))
            {
                ModelState.AddModelError("Login", ValidationMessages.ResourceManager.GetString("SignUpLoginRemote"));
            }
            if (ModelState.IsValid)
            {
                _userProvider.CreateNewUser(user);
                return View("SignUpConfirmation");
            }
            else
                return View();
        }

        // GET: SignIn
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInDto model, string returnUrl)
        {
            AuthenticationManager.SignOut();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _userProvider.GetUserSignIn(model);
            switch (result.State)
            {
                case UserState.Success:
                    SignIn(result, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                case UserState.Locked:
                    return View("Lockout");
                case UserState.NotActivated:
                    return View("NotActivated");
                case UserState.NotFound:
                default:
                    ModelState.AddModelError("Error", ValidationMessages.ResourceManager.GetString("InvalidUsernameOrPassword"));
                    return View(model);
            }
        }

        private void SignIn(SignInInfoDto userInfo, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Login),
                new Claim(ClaimTypes.Name, userInfo.Name)
            };

            // create required claims
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = rememberMe,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public ActionResult ActivateUser(string hash)
        {
            _userProvider.ActivateUser(hash);
            return View("ActivateConfirmation");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsEmailAvailble(string email)
        {
            return Json(_userProvider.IsEmailAvailable(email), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsLoginAvailble(string login)
        {
            return Json(_userProvider.IsLoginAvailable(login), JsonRequestBehavior.AllowGet);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}