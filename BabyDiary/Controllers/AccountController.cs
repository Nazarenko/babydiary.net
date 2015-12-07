using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BabyDiary.Business.Interfaces;
using BabyDiary.Models.DTOs;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Resources;

namespace BabyDiary.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserProvider _userProvider;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        // GET: SignUp
        public ActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(SignUpDto user)
        {
            if (!await _userProvider.IsEmailAvailableAsync(user.Email))
            {
                ModelState.AddModelError("Email", ValidationMessages.ResourceManager.GetString("SignUpEmailRemote"));
            }
            if (!await _userProvider.IsLoginAvailableAsync(user.Login))
            {
                ModelState.AddModelError("Login", ValidationMessages.ResourceManager.GetString("SignUpLoginRemote"));
            }
            if (ModelState.IsValid)
            {
                await _userProvider.CreateNewUserAsync(user);
                // TODO send email
                return View("SignUpConfirmation");
            }
            else
                return View();
        }

        // GET: SignIn
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(SignInDto model, string returnUrl)
        {
            AuthenticationManager.SignOut();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userProvider.SignInAsync(model);
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
                    ModelState.AddModelError("", ValidationMessages.ResourceManager.GetString("InvalidUsernameOrPassword"));
                    return View(model);
            }
        }

        private void SignIn(SignInInfoDto userInfo, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Login)
            };

            if (userInfo.Name != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, userInfo.Name));
            }
            // create required claims
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = rememberMe,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> ActivateUser(string token)
        {
            if (token == null)
                return View("Errors/Error404");
            if (await _userProvider.ActivateUserAsync(token))
                return View("ActivateConfirmation");
            else
                return View("Errors/Error404");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IsEmailAvailble(string email)
        {
            return Json(await _userProvider.IsEmailAvailableAsync(email), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IsLoginAvailble(string login)
        {
            return Json(await _userProvider.IsLoginAvailableAsync(login), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var code = await _userProvider.GeneratePasswordResetTokenAsync(model.Email);
            if (code != null)
            {
                // TODO send Email
            }
            return View("ForgotPasswordConfirmation");

//            var user = await UserManager.FindByNameAsync(model.Email);
//            if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
//            {
//                // Don't reveal that the user does not exist or is not confirmed
//                return View("ForgotPasswordConfirmation");
//            }
//
//            var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
//            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
//            await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
//            ViewBag.Link = callbackUrl;
//            return View("ForgotPasswordConfirmation");

        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Errors/Error404") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (await _userProvider.ResetPasswordAsync(model))
            {
                return View("ResetPasswordConfirmation");
            }
            else
            {
                return View("Errors/Error");
            }
        }

    }
}