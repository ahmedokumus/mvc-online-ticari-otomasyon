using MvcOnlineTicariOtomasyon.Models;
using System.Linq;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Breadcrumb;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
[Authorize(Roles = "A")]
public class CustomerController : Controller
{
    private readonly Context _context = new();

    [BreadCrumb(Clear = true)]
    public ActionResult CustomerList(int page = 1)
    {
        return View(_context.Customers.Where(x => x.Deleted == false).ToList().ToPagedList(page, 5));
    }

    [HttpGet]
    public ActionResult CustomerAdd()
    {
        return View();
    }
    [HttpPost]
    public ActionResult CustomerAdd(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return RedirectToAction("CustomerList");
    }

    public ActionResult CustomerRemove(int id)
    {
        var customer = _context.Customers.Find(id);
        customer.Deleted = true;
        _context.SaveChanges();
        return RedirectToAction("CustomerList");
    }


    [HttpGet]
    public ActionResult CustomerUpdate(int id)
    {
        var customer = _context.Customers.Find(id);
        return View(customer);
    }
    [HttpPost]
    public ActionResult CustomerUpdate(Customer customer)
    {
        var customer2 = _context.Customers.Find(customer.CustomerId);
        if (!ModelState.IsValid)
        {
            return View(customer2);
        }
        customer2.CustomerFirstName = customer.CustomerFirstName;
        customer2.CustomerLastName = customer.CustomerLastName;
        customer2.CustomerCity = customer.CustomerCity;
        customer2.Email = customer.Email;
        _context.SaveChanges();
        return RedirectToAction("CustomerList");
    }


    public ActionResult CustomerPurchaseHistory(int id)
    {
        var value = _context.Customers.Where(x => x.CustomerId == id)
            .Select(y => y.CustomerFirstName + " " + y.CustomerLastName).FirstOrDefault();
        ViewBag.value = value;
        return View(_context.SalesMovements.Where(x => x.CustomerId == id).ToList());
    }
}