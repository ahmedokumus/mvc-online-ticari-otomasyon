using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult PageError()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
    }
}