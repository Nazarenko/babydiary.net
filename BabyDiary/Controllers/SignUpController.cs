using BabyDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyDiary.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SignUp user)
        {
            if (ModelState.IsValid)
                return View("SignUpConfirmation");
            else
                return View();
        }

        public ActionResult IsEmailAvailble(string email)
        {
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsLoginAvailble(string login)
        {
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }

}