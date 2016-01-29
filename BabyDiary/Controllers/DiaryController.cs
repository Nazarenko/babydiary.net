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
        private readonly IChildProvider _childProvider;

        public DiaryController(IChildProvider childProvider)
        {
            _childProvider = childProvider;
        }

        // GET: Diary
        public ActionResult Index()
        {
            return View();
        }

    }
}