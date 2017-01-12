using System.Web.Mvc;

namespace SOP.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Parametrizacao()
        {
            return View("~/Views/Shared/Parametrizacao.cshtml");
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}