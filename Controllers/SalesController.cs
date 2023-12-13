using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class SalesController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult SalesList(int page = 1)
    {
        return View(_context.SalesMovements.Where(x => x.Deleted == false).ToList().ToPagedList(page, 5));
    }

    [HttpGet]
    public ActionResult SalesAdd()
    {
        List<SelectListItem> products = (from product in _context.Products.ToList()
            select new SelectListItem
            {
                Text = product.ProductName,
                Value = product.ProductId.ToString()
            }).ToList();
        ViewBag.products = products;

        List<SelectListItem> employees = (from employee in _context.Employees.ToList()
            select new SelectListItem
            {
                Text = $"{employee.EmployeeFirstName} {employee.EmployeeLastName}",
                Value = employee.EmployeeId.ToString()
            }).ToList();
        ViewBag.employees = employees;

        List<SelectListItem> customers = (from customer in _context.Customers.ToList()
            select new SelectListItem
            {
                Text = $"{customer.CustomerFirstName} {customer.CustomerLastName}",
                Value = customer.CustomerId.ToString()
            }).ToList();
        ViewBag.customers = customers;

        return View();
    }
    [HttpPost]
    public ActionResult SalesAdd(SalesMovement salesMovement, Product product)
    {
        var selectedProduct = _context.Products.Where(x => x.ProductId == salesMovement.ProductId);

        var product2 = selectedProduct.FirstOrDefault();
        var productStock = selectedProduct.Select(y => y.Stock).FirstOrDefault();
        product = product2;
        product.Stock = Convert.ToInt16(productStock - salesMovement.Piece);

        var price = selectedProduct.Select(y => y.SalePrice).FirstOrDefault();
        salesMovement.Price = price;
        salesMovement.Date = DateTime.Now.Date;
        salesMovement.TotalPrice = salesMovement.Price * salesMovement.Piece;
        _context.SalesMovements.Add(salesMovement);
        _context.SaveChanges();
        return RedirectToAction("SalesList");
    }

    [HttpGet]
    public ActionResult SalesUpdate(int id)
    {
        List<SelectListItem> products = (from product in _context.Products.ToList()
            select new SelectListItem
            {
                Text = product.ProductName,
                Value = product.ProductId.ToString()
            }).ToList();
        ViewBag.products = products;

        List<SelectListItem> employees = (from employee in _context.Employees.ToList()
            select new SelectListItem
            {
                Text = $"{employee.EmployeeFirstName} {employee.EmployeeLastName}",
                Value = employee.EmployeeId.ToString()
            }).ToList();
        ViewBag.employees = employees;

        List<SelectListItem> customers = (from customer in _context.Customers.ToList()
            select new SelectListItem
            {
                Text = $"{customer.CustomerFirstName} {customer.CustomerLastName}",
                Value = customer.CustomerId.ToString()
            }).ToList();
        ViewBag.customers = customers;

        var salesMovement = _context.SalesMovements.Find(id);
        return View(salesMovement);
    }

    [HttpPost]
    public ActionResult SalesUpdate(SalesMovement salesMovement, Product product)
    {
        var salesMovement2 = _context.SalesMovements.Find(salesMovement.SalesMovementId);

        var Selectedproduct = _context.Products.Where(x => x.ProductId == salesMovement.ProductId);

        var product2 = Selectedproduct.FirstOrDefault();
        var productStock = Selectedproduct.Select(y => y.Stock).FirstOrDefault();
        product = product2;
        product.Stock = Convert.ToInt16(
            salesMovement.Piece > salesMovement2.Piece
                ? productStock - (salesMovement2.Piece - salesMovement.Piece)
                : productStock + (salesMovement2.Piece - salesMovement.Piece));

        var price = Selectedproduct.Select(y => y.SalePrice).FirstOrDefault();

        salesMovement2.Price = price;
        salesMovement2.Piece = salesMovement.Piece;
        salesMovement2.TotalPrice = salesMovement2.Price * salesMovement2.Piece;
        _context.SaveChanges();
        return RedirectToAction("SalesList");
    }

    public ActionResult SalesDetail(int id)
    {
        var salesMovement = _context.SalesMovements.Find(id);
        return View(salesMovement);
    }
}