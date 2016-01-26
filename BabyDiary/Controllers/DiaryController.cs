using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BabyDiary.Business.Interfaces;
using BabyDiary.Models.DTOs;

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