using System.Web.Mvc;

namespace BabyDiary.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

    }
}