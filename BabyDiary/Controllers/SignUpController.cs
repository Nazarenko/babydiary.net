using BabyDiary.Business.Interfaces;
using BabyDiary.Models.DTOs;
using System.Web.Mvc;

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
        public ActionResult Index(SignUpDto user)
        {
            if (ModelState.IsValid)
            {
                _userProvider.SignUp(user);
                return View("SignUpConfirmation");
            }
            else
                return View();
        }

        public ActionResult IsEmailAvailble(string email)
        {
            return Json(_userProvider.IsEmailAvailable(email), JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsLoginAvailble(string login)
        {
            return Json(_userProvider.IsLoginAvailable(login), JsonRequestBehavior.AllowGet);
        }
    }

}