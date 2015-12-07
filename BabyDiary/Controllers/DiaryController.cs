using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyDiary.Controllers
{
    [Authorize]
    public class DiaryController : BaseController
    {
        // GET: Diary
        public ActionResult Index()
        {
            return View();
        }
    }
}