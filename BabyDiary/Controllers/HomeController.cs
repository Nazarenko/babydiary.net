using System.Web.Mvc;
using BabyDiary.Business.Interfaces;

namespace BabyDiary.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}