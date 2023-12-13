using System.Linq;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class HomeController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult Index()
    {
        ViewBag.Customers = _context.Customers.Count();
        ViewBag.Products = _context.Products.Count();
        ViewBag.Categories = _context.Categories.Count();
        ViewBag.CityCount = (from customer in _context.Customers select customer.CustomerCity).Distinct().Count();
        return View();
    }

    public PartialViewResult PartialToDoList()
    {
        return PartialView(_context.ToDoLists.ToList());
    }

    public PartialViewResult PartialDirectChat()
    {
        return PartialView();
    }

    public PartialViewResult PartialCustomTabs()
    {
        return PartialView();
    }

    public PartialViewResult PartialCalendar()
    {
        return PartialView();
    }

    public PartialViewResult PartialMapCard()
    {
        return PartialView();
    }

    public PartialViewResult PartialSolidSalesGraph()
    {
        return PartialView();
    }
}