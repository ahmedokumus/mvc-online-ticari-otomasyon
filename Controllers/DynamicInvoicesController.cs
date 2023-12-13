using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Breadcrumb;
using MvcOnlineTicariOtomasyon.Models;

namespace MvcOnlineTicariOtomasyon.Controllers;

[BreadCrumb(Manual = false)]
public class DynamicInvoicesController : Controller
{
    private readonly Context _context = new Context();

    [BreadCrumb(Clear = true)]
    public ActionResult DynamicInvoiceList(int page = 1)
    {
        List<SelectListItem> employees = (from employee in _context.Employees.ToList()
                                          select new SelectListItem
                                          {
                                              Text = $@"{employee.EmployeeFirstName} {employee.EmployeeLastName}",
                                              Value = $"{employee.EmployeeFirstName} {employee.EmployeeLastName}"
                                          }).ToList();
        ViewBag.employees = employees;

        List<SelectListItem> customers = (from customer in _context.Customers.ToList()
                                          select new SelectListItem
                                          {
                                              Text = $@"{customer.CustomerFirstName} {customer.CustomerLastName}",
                                              Value = $"{customer.CustomerFirstName} {customer.CustomerLastName}"
                                          }).ToList();
        ViewBag.customers = customers;

        ClassForDynamicInvoice dynamicInvoice = new ClassForDynamicInvoice();
        dynamicInvoice.ValueInvoices = _context.Invoices.ToList();
        dynamicInvoice.ValueInvoiceItems = _context.InvoiceItems.ToList();
        return View(dynamicInvoice);
    }

    public ActionResult InvoiceSave(Invoice invoice, InvoiceItem[] kalemler)
    {
        invoice.Date=DateTime.Now;
        _context.Invoices.Add(invoice);
        for (int i = 0; i < kalemler.Length; i++)
        {
            _context.InvoiceItems.Add(kalemler[i]);
        }
        _context.SaveChanges();
        return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
    }
}