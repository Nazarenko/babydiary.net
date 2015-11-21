using BabyDiary.Business.Interfaces;
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
        private readonly IUserProvider _userProvider;

        public SignUpController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SignUpDTO user)
        {
            if (ModelState.IsValid)
                return View("SignUpConfirmation");
            else
                return View();
        }

        public ActionResult IsEmailAvailble(string Email)
        {
            return Json(_userProvider.IsEmailAvailable(Email), JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsLoginAvailble(string Login)
        {
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }

}