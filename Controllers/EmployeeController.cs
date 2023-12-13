using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class EmployeeController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult EmployeeList(int page = 1)
    {
        return View(_context.Employees.Where(x => x.Deleted == false).ToList().ToPagedList(page, 5));
    }

    public ActionResult EmployeeCardList(int page = 1)
    {
        return View(_context.Employees.Where(x => x.Deleted == false).ToList().ToPagedList(page, 3));
    }

    [HttpGet]
    public ActionResult EmployeeAdd()
    {
        List<SelectListItem> departments = (from department in _context.Departments.ToList()
                                            select new SelectListItem
                                            {
                                                Text = department.DepartmentName,
                                                Value = department.DepartmentId.ToString()
                                            }).ToList();
        ViewBag.departments = departments;

        return View();
    }
    [HttpPost]
    public ActionResult EmployeeAdd(Employee employee)
    {
        if (Request.Files.Count > 0)
        {
            string fileName = Path.GetFileName(Request.Files[0]?.FileName);
            string extension = Path.GetExtension(Request.Files[0]?.FileName);
            string path = $"~/Image/{fileName}{extension}";
            Request.Files[0]?.SaveAs(Server.MapPath(path));
            employee.EmployeeImage = $"/Image/{fileName}{extension}";
        }
        _context.Employees.Add(employee);
        _context.SaveChanges();
        return RedirectToAction("EmployeeList");
    }

    public ActionResult EmployeeRemove(int id)
    {
        var employee = _context.Employees.Find(id);
        employee.Deleted = true;
        _context.SaveChanges();
        return RedirectToAction("EmployeeList");
    }

    [HttpGet]
    public ActionResult EmployeeUpdate(int id)
    {
        var employee = _context.Employees.Find(id);
        List<SelectListItem> departments = (from department in _context.Departments.ToList()
                                            select new SelectListItem
                                            {
                                                Text = department.DepartmentName,
                                                Value = department.DepartmentId.ToString()
                                            }).ToList();
        ViewBag.departments = departments;
        return View(employee);
    }
    [HttpPost]
    public ActionResult EmployeeUpdate(Employee employee)
    {
        var employee2 = _context.Employees.Find(employee.EmployeeId);
        if (Request.Files.Count > 0)
        {
            string fileName = Path.GetFileName(Request.Files[0]?.FileName);
            string extension = Path.GetExtension(Request.Files[0]?.FileName);
            string path = $"~/Image/{fileName}{extension}";
            Request.Files[0]?.SaveAs(Server.MapPath(path));
            employee2.EmployeeImage = $"/Image/{fileName}{extension}";
        }
        employee2.EmployeeFirstName = employee.EmployeeFirstName;
        employee2.EmployeeLastName = employee.EmployeeLastName;
        //employee2.EmployeeImage = employee.EmployeeImage;
        employee2.DepartmentId = employee.DepartmentId;
        _context.SaveChanges();
        return RedirectToAction("EmployeeList");
    }
}