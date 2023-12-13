using System;
using MvcOnlineTicariOtomasyon.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Breadcrumb;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
[Authorize(Roles = "A,B")]
public class ProductController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult ProductList(string search, int page = 1)
    {
        var products = from product in _context.Products where product.Deleted == false select product;
        if (!string.IsNullOrEmpty(search))
        {
            products = products.Where(y => y.ProductName.Contains(search));
        }

        return View(products.ToList().ToPagedList(page, 5));
    }

    [HttpGet]
    public ActionResult ProductAdd()
    {
        List<SelectListItem> result = (from category in _context.Categories.ToList()
                                       select new SelectListItem
                                       {
                                           Text = category.CategoryName,
                                           Value = category.CategoryId.ToString()
                                       }).ToList();
        ViewBag.categories = result;
        return View();
    }
    [HttpPost]
    public ActionResult ProductAdd(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return RedirectToAction("ProductList");
    }

    public ActionResult ProductRemove(int id)
    {
        var product = _context.Products.Find(id);
        product.Deleted = true;
        _context.SaveChanges();
        return RedirectToAction("ProductList");
    }

    [HttpGet]
    public ActionResult ProductUpdate(int id)
    {
        var product = _context.Products.Find(id);
        List<SelectListItem> result = (from category in _context.Categories.ToList()
                                       select new SelectListItem
                                       {
                                           Text = category.CategoryName,
                                           Value = category.CategoryId.ToString()
                                       }).ToList();
        ViewBag.categories = result;
        return View(product);
    }
    [HttpPost]
    public ActionResult ProductUpdate(Product product)
    {
        var product2 = _context.Products.Find(product.ProductId);
        product2.ProductName = product.ProductName;
        product2.CategoryId = product.CategoryId;
        product2.ProductBrand = product.ProductBrand;
        product2.ProductImage = product.ProductImage;
        product2.Stock = product.Stock;
        product2.PurchasePrice = product.PurchasePrice;
        product2.SalePrice = product.SalePrice;
        product2.Status = true;
        _context.SaveChanges();
        return RedirectToAction("ProductList");
    }

    public ActionResult ProductDetail(int id)
    {
        var result = _context.Products.Find(id);
        return View(result);
    }

    [HttpGet]
    public ActionResult ProductSales(int id)
    {
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

        ViewBag.productName = _context.Products.Find(id)?.ProductName;
        ViewBag.productId = _context.Products.Find(id)?.ProductId;
        ViewBag.salesPrice = _context.Products.Find(id)?.SalePrice;
        ViewBag.productStockCount = _context.Products.Find(id)?.Stock;

        return View();
    }
    [HttpPost]
    public ActionResult ProductSales(SalesMovement salesMovement, Product product, int Piecee)
    {
        var selectedProduct = _context.Products.Where(x => x.ProductId == salesMovement.ProductId);

        salesMovement.Piece = Piecee;

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
        return RedirectToAction("SalesList", "Sales");
    }
}