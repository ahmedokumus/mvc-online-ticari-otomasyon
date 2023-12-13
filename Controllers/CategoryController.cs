using System.Linq;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers;

//<add name = "Context" connectionString="Server=tcp:ahmed-hakan-sqlserver.database.windows.net,1433;Initial Catalog=DbTicariOtomasyon;Persist Security Info=False;User ID=CloudSA9a68e6ab;Password=Aho38839804216;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" providerName="System.Data.SqlClient" />
//<add name="Context" connectionString="data source=(localdb)\MSSQLLocalDB;initial catalog=DbTicariOtomasyon;integrated security=True;" providerName="System.Data.SqlClient"/>

//[Authorize]
[BreadCrumb(Manual = false)]
[Authorize(Roles = "A")]
public class CategoryController : Controller
{
    private readonly Context _context = new();

    [BreadCrumb(Clear = true)]
    public ActionResult CategoryList(int page = 1)
    {
        return View(_context.Categories.Where(x => x.Deleted == false).ToList().ToPagedList(page, 5));
    }

    [HttpGet]
    public ActionResult CategoryAdd()
    {
        return View();
    }
    [HttpPost]
    public ActionResult CategoryAdd(Category category)
    {
        if (category.CategoryName != null)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        return RedirectToAction("CategoryList");
    }

    public ActionResult CategoryRemove(int id)
    {
        var category = _context.Categories.Find(id);
        category.Deleted = true;
        _context.SaveChanges();
        return RedirectToAction("CategoryList");
    }

    [HttpGet]
    public ActionResult CategoryUpdate(int id)
    {
        var category = _context.Categories.Find(id);
        return View(category);
    }
    [HttpPost]
    public ActionResult CategoryUpdate(Category category)
    {
        var category2 = _context.Categories.Find(category.CategoryId);
        if (category.CategoryName != null)
        {
            category2.CategoryName = category.CategoryName;
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        return RedirectToAction("CategoryList");
    }

    public ActionResult Trial()
    {
        ClassForCascading1 cascading1 = new ClassForCascading1();
        cascading1.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
        cascading1.Products = new SelectList(_context.Products, "ProductId", "ProductName");

        return View(cascading1);
    }
    public JsonResult BringProduct(int p)
    {
        var productList = (from product in _context.Products
                           join category in _context.Categories
                               on product.Category.CategoryId equals category.CategoryId
                           where product.Category.CategoryId == p
                           select new
                           {
                               Text = product.ProductName,
                               Value = product.ProductId.ToString()
                           }).ToList();
        return Json(productList, JsonRequestBehavior.AllowGet);
    }
}
