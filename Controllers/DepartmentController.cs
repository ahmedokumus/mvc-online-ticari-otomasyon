using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class DepartmentController : Controller
{
    private readonly Context _context = new();

    [BreadCrumb(Clear = true)]
    public ActionResult DepartmentList(int page = 1)
    {
        return View(_context.Departments.Where(x => x.Deleted == false).ToList().ToPagedList(page, 5));
    }
        
    [HttpGet]
    public ActionResult DepartmentAdd()
    {
        return View();
    }
    [HttpPost]
    public ActionResult DepartmentAdd(Department department)
    {
        if (department.DepartmentName != null)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }
        return RedirectToAction("DepartmentList");
    }

    public ActionResult DepartmentRemove(int id)
    {
        var department = _context.Departments.Find(id);
        department.Deleted = true;
        _context.SaveChanges();
        return RedirectToAction("DepartmentList");
    }
        
    [HttpGet]
    public ActionResult DepartmentUpdate(int id)
    {
        var department = _context.Departments.Find(id);
        return View(department);
    }
    [HttpPost]
    public ActionResult DepartmentUpdate(Department department)
    {
        var department2 = _context.Departments.Find(department.DepartmentId);
        if (department.DepartmentName != null)
        {
            department2.DepartmentName = department.DepartmentName;
            _context.SaveChanges();
            return RedirectToAction("DepartmentList");
        }
        return View();
    }
        
    public ActionResult DepartmentDetail(int id)
    {
        var result = _context.Employees.Where(x => x.DepartmentId == id).ToList();
        var value = _context.Departments.Where(x => x.DepartmentId == id).Select(y => y.DepartmentName)
            .FirstOrDefault();
        ViewBag.value = value;
        return View(result);
    }
        
    public ActionResult DepartmentEmployeeSales(int id)
    {
        var value = _context.Employees.Where(x => x.EmployeeId == id).Select(y => y.EmployeeFirstName + " " + y.EmployeeLastName)
            .FirstOrDefault();
        ViewBag.value = value;
        return View(_context.SalesMovements.Where(x => x.EmployeeId == id).ToList());
    }
}