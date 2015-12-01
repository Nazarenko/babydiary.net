using BabyDiary.Business.Interfaces;
using BabyDiary.Models.DTOs;
using System.Web.Mvc;
using Resources;

namespace BabyDiary.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IUserProvider _userProvider;

        public SignUpController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        // GET: SignUp
        public ActionResult Index()
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

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(SignUpDto user)
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

        public ActionResult ActivateUser(string hash)
        {
            _userProvider.ActivateUser(hash);
            return View("ActivateConfirmation");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult IsEmailAvailble(string email)
        {
            return Json(_userProvider.IsEmailAvailable(email), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult IsLoginAvailble(string login)
        {
            return Json(_userProvider.IsLoginAvailable(login), JsonRequestBehavior.AllowGet);
        }
    }

}