using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;
using System.Linq;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class GalleryController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult Index()
    {
        return View(_context.Products.ToList());
    }
}