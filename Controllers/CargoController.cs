using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;
using PagedList;
using QRCoder;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class CargoController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult CargoList(string search, int page = 1)
    {
        var cargoDetails = from cargoDetail in _context.CargoDetails select cargoDetail;
        if (!string.IsNullOrEmpty(search))
        {
            cargoDetails = cargoDetails.Where(y => y.CargoDetailTrackingCode.Contains(search));
        }

        return View(cargoDetails.ToList().ToPagedList(page, 5));
    }

    [HttpGet]
    public ActionResult CargoAdd()
    {
        List<SelectListItem> employees = (from employee in _context.Employees.ToList()
                                          select new SelectListItem
                                          {
                                              Text = $"{employee.EmployeeFirstName} {employee.EmployeeLastName}",
                                              Value = $"{employee.EmployeeFirstName} {employee.EmployeeLastName}"
                                          }).ToList();
        ViewBag.employees = employees;

        List<SelectListItem> customers = (from customer in _context.Customers.ToList()
                                          select new SelectListItem
                                          {
                                              Text = $"{customer.CustomerFirstName} {customer.CustomerLastName}",
                                              Value = $"{customer.CustomerFirstName} {customer.CustomerLastName}"
                                          }).ToList();
        ViewBag.customers = customers;

        Random random = new Random();
        char[] items = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N' };
        int i1, i2, i3;
        i1 = random.Next(0, items.Length);
        i2 = random.Next(0, items.Length);
        i3 = random.Next(0, items.Length);
        int n1, n2, n3;
        n1 = random.Next(100, 1000);
        n2 = random.Next(10, 99);
        n3 = random.Next(10, 99);
        string trackingCode = $"{n1}{items[i1]}{n2}{items[i2]}{n3}{items[i3]}";
        ViewBag.code = trackingCode;

        //using (MemoryStream memoryStream = new MemoryStream())
        //{
        //    QRCodeGenerator generator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = generator.CreateQrCode(trackingCode, QRCodeGenerator.ECCLevel.Q);
        //    using (Bitmap image = qrCode.GetGraphic(10))
        //    {
        //        image.Save(memoryStream, ImageFormat.Png);
        //        ViewBag.qrCode = "data:image/png/;base64," + Convert.ToBase64String(memoryStream.ToArray());
        //    }
        //}

        return View();
    }
    [HttpPost]
    public ActionResult CargoAdd(CargoDetail cargoDetail)
    {
        cargoDetail.Date = DateTime.Now;
        _context.CargoDetails.Add(cargoDetail);
        _context.SaveChanges();
        return RedirectToAction("CargoList");
    }


    public ActionResult CargoDetail(string trackingCode)
    {
        var result = _context.CargoTrackings.Where(x => x.CargoTrackingCargoTracking == trackingCode).ToList();
        ViewBag.value = trackingCode;
        return View(result);
    }
}