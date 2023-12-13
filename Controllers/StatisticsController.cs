using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class StatisticsController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult CardStatistics()
    {
        ViewBag.totalCustomer = _context.Customers.Count();

        ViewBag.totalProduct = _context.Products.Count();

        ViewBag.totalEmployee = _context.Employees.Count();

        ViewBag.totalCategory = _context.Categories.Count();

        ViewBag.totalStock = _context.Products.Sum(x => x.Stock);

        ViewBag.totalBrand = (from product in _context.Products select product.ProductBrand).Distinct().Count();

        ViewBag.criticalLevel = _context.Products.Count(x => x.Stock <= 5);

        ViewBag.highestPricedProduct =
            (from product in _context.Products orderby product.SalePrice descending select product.ProductName)
            .FirstOrDefault();

        ViewBag.value1 = _context.Products.GroupBy(x => x.ProductBrand).OrderByDescending(y => y.Count())
            .Select(z => z.Key).FirstOrDefault();

        ViewBag.freezerCount = _context.Products.Count(x => x.ProductName == "Buzdolabı");

        ViewBag.laptopCount = _context.Products.Count(x => x.ProductName == "Laptop");

        ViewBag.lowestPricedProduct = (from product in _context.Products orderby product.SalePrice select product.ProductName)
            .FirstOrDefault();

        ViewBag.bestSellingProduct = _context.Products.Where(p => p.ProductId == _context.SalesMovements
                .GroupBy(x => x.ProductId)
                .OrderByDescending(y => y.Count())
                .Select(z => z.Key).FirstOrDefault())
            .Select(k => k.ProductName).FirstOrDefault();

        ViewBag.totalPrice = _context.SalesMovements.Sum(x => x.TotalPrice);

        DateTime today = DateTime.Today;

        ViewBag.todaySales = _context.SalesMovements.Count(x => x.Date == today);

        ViewBag.todayTotalPrice = _context.SalesMovements.Where(x => x.Date == today).Sum(y => (decimal?)y.TotalPrice);

        return View();
    }

    public ActionResult SimpleStatistics()
    {
        return View();
    }

    public PartialViewResult PartialCategoryTable()
    {
        return PartialView();
    }

    public PartialViewResult PartialCustomerCityTable()
    {
        var result = from customer in _context.Customers
                     group customer by customer.CustomerCity
            into temp
                     select new ClassGroup
                     {
                         City = temp.Key,
                         Number = temp.Count()
                     };
        return PartialView(result.ToList());
    }

    public PartialViewResult PartialDepartmentEmployeeTable()
    {
        var result = from employee in _context.Employees
                     group employee by employee.Department.DepartmentName
            into temp
                     select new ClassGroup2
                     {
                         DepartmentName = temp.Key,
                         Number = temp.Count()
                     };
        return PartialView(result.ToList());
    }

    public PartialViewResult PartialProductBrandTable()
    {
        var result = from product in _context.Products
                     group product by product.ProductBrand
            into temp
                     select new ClassGroup3
                     {
                         Brand = temp.Key,
                         Number = temp.Count()
                     };
        return PartialView(result.ToList());
    }

    public PartialViewResult PartialCustomerTable()
    {
        return PartialView(_context.Customers.Where(x => x.Deleted == false).ToList());
    }

    public PartialViewResult PartialProductTable()
    {
        return PartialView(_context.Products.Where(x => x.Deleted == false).ToList());
    }

    public ActionResult Graphic1()
    {
        return View();
    }

    public ActionResult Graphic2()
    {
        var graphic = new Chart(width: 600, height: 600);
        graphic.AddTitle(text: "Kategori ve Ürün Stok Sayısı");
        graphic.AddLegend(title: "Stok");
        graphic.AddSeries(
            name: "Veriler",
            //chartType: "Column",
            xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar", "Küçük Ev Aletleri" },
            yValues: new[] { 85, 66, 98, 75 }
        );
        graphic.Write();

        return File(graphic.ToWebImage().GetBytes(), "image/jpg");
    }

    public ActionResult Graphic3()
    {
        ArrayList xvalue = new ArrayList();
        ArrayList yvalue = new ArrayList();
        var results = _context.Products.ToList();
        results.ToList().ForEach(x => xvalue.Add(x.ProductName));
        results.ToList().ForEach(y => yvalue.Add(y.Stock));

        var graphic = new Chart(width: 1200, height: 800)
            .AddTitle("Stoklar")
            .AddSeries(
                chartType: "Pie",
                name: "Stok",
                xValue: xvalue,
                yValues: yvalue
            );

        return File(graphic.ToWebImage().GetBytes(), "image/jpg");
    }

    public ActionResult Graphic4()
    {
        return View();
    }
    public ActionResult VisualizeProductResult()
    {
        return Json(data: ProductList(), JsonRequestBehavior.AllowGet);
    }
    public List<ClassGroup4> ProductList()
    {
        List<ClassGroup4> list = new List<ClassGroup4>
        {
            new()
            {
                ProductName = "Bilgisayar",
                Stock = 120
            },
            new()
            {
                ProductName = "Beyaz Eşya",
                Stock = 150
            },
            new()
            {
                ProductName = "Mobilya",
                Stock = 70
            },
            new()
            {
                ProductName = "Çamaşır Makinesi",
                Stock = 180
            },
            new()
            {
                ProductName = "Bulaşık Makinesi",
                Stock = 90
            }
        };
        return list;
    }

    public ActionResult Graphic5()
    {
        return View();
    }
    public ActionResult VisualizeProductResult2()
    {
        return Json(data: ProductList2(), JsonRequestBehavior.AllowGet);
    }
    public List<ClassGroup5> ProductList2()
    {
        List<ClassGroup5> list = new List<ClassGroup5>();
        list = _context.Products.Select(product => new ClassGroup5
        {
            ProductName = product.ProductName,
            Stock = product.Stock
        }).ToList();
        return list;
    }
}