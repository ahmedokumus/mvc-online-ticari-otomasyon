using System;
using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class InvoiceController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult InvoiceList(int page = 1)
    {
        return View(_context.Invoices.Where(x => x.Deleted == false).ToList().ToPagedList(page, 5));
    }

    [HttpGet]
    public ActionResult InvoiceAdd()
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

        return View();
    }
    [HttpPost]
    public ActionResult InvoiceAdd(Invoice invoice)
    {
        invoice.Date = DateTime.Now;
        _context.Invoices.Add(invoice);
        _context.SaveChanges();
        return RedirectToAction("InvoiceList");
    }

    [HttpGet]
    public ActionResult InvoiceUpdate(int id)
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

        var invoice = _context.Invoices.Find(id);
        return View(invoice);
    }
    [HttpPost]
    public ActionResult InvoiceUpdate(Invoice invoice)
    {
        var invoice2 = _context.Invoices.Find(invoice.InvoiceId);
        invoice2.InvoiceSerialNumber = invoice.InvoiceSerialNumber;
        invoice2.InvoiceNumber = invoice.InvoiceNumber;
        invoice2.TaxAdministration = invoice.TaxAdministration;
        invoice2.TotalPrice = invoice.TotalPrice;
        invoice2.DeliveryPerson = invoice.DeliveryPerson;
        invoice2.DeliveryArea = invoice.DeliveryArea;
        _context.SaveChanges();
        return RedirectToAction("InvoiceList");
    }

    //public ActionResult InvoiceDetail(int id)
    //{
    //    var invoiceItems = _context.InvoiceItems.Where(x => x.InvoiceId == id).ToList();
    //    ViewBag.value = _context.Invoices.Where(x => x.InvoiceId == id)
    //        .Select(y => y.InvoiceSerialNumber + " " + y.InvoiceNumber).FirstOrDefault();
    //    return View(invoiceItems);
    //}

    [HttpPost]
    public ActionResult PartialInvoiceDetail(int id)
    {
        ViewBag.value = _context.Invoices.Where(x => x.InvoiceId == id)
            .Select(y => y.InvoiceSerialNumber + " " + y.InvoiceNumber).FirstOrDefault();
        return PartialView("PartialInvoiceDetail", _context.InvoiceItems.Where(z => z.InvoiceId == id).ToList());
    }

    [HttpGet]
    public ActionResult InvoiceItemsAdd()
    {
        List<SelectListItem> products = (from product in _context.Products.ToList()
                                         select new SelectListItem
                                         {
                                             Text = $"{product.ProductName}",
                                             Value = $"{product.ProductName}"
                                         }).ToList();
        ViewBag.products = products;

        return View();
    }
    [HttpPost]
    public ActionResult InvoiceItemsAdd(InvoiceItem invoiceItem)
    {
        _context.InvoiceItems.Add(invoiceItem);
        _context.SaveChanges();
        return RedirectToAction("InvoiceList");
    }
}