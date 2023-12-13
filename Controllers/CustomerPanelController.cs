using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
public class CustomerPanelController : Controller
{
    private readonly Context _context;

    private string email;

    public CustomerPanelController()
    {
        _context = new Context();
    }

    public ActionResult Profile()
    {
        email = (string)Session["customerEmail"];

        var id = _context.Customers.Where(x => x.Email == email).Select(y => y.CustomerId).FirstOrDefault();

        ViewBag.totalPurchase = _context.SalesMovements.Count(x => x.CustomerId == id);
        ViewBag.totalPrice = _context.SalesMovements.Where(x => x.CustomerId == id).Sum(y => y.TotalPrice);
        ViewBag.totalProductCount = _context.SalesMovements.Where(x => x.CustomerId == id).Sum(y => y.Piece);

        var result = _context.Customers.FirstOrDefault(x => x.Email == email);
        return View(result);
    }

    public PartialViewResult PartialProfileMessages()
    {
        email = (string)Session["customerEmail"];

        var messages = _context.Messages.Where(x => x.Receiver == email).ToList();

        return PartialView(messages);
    }

    public PartialViewResult PartialProfileAnnouncements()
    {
        email = (string)Session["customerEmail"];

        var datas = _context.Messages.Where(x => x.Sender == email).ToList();
        return PartialView(datas);
    }

    public PartialViewResult PartialProfileSettings()
    {
        email = (string)Session["customerEmail"];

        var id = _context.Customers.Where(x => x.Email == email).Select(y => y.CustomerId).FirstOrDefault();
        var result = _context.Customers.Find(id);

        return PartialView("PartialProfileSettings", result);
    }

    public ActionResult Update(Customer customer)
    {
        email = (string)Session["customerEmail"];

        var id = _context.Customers.Where(x => x.Email == email).Select(y => y.CustomerId)
            .FirstOrDefault();
        var customer2 = _context.Customers.Find(id);
        customer2.CustomerFirstName = customer.CustomerFirstName;
        customer2.CustomerLastName = customer.CustomerLastName;
        customer2.CustomerCity = customer.CustomerCity;
        customer2.Password = customer.Password;
        _context.SaveChanges();

        return RedirectToAction("Profile", "CustomerPanel");
    }

    public ActionResult Orders()
    {
        email = (string)Session["customerEmail"];

        var id = _context.Customers.Where(x => x.Email == email).Select(y => y.CustomerId)
            .FirstOrDefault();
        var result = _context.SalesMovements.Where(x => x.CustomerId == id).ToList();
        return View(result);
    }

    public ActionResult InComingMessages()
    {
        email = (string)Session["customerEmail"];

        ViewBag.comingMessages = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Receiver == email);
        ViewBag.sent = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Sender == email);
        ViewBag.deletedMessages =
            _context.Messages.Where(x => x.Deleted == true).Count(y => y.Sender == email || y.Receiver == email);

        var result = _context.Messages.Where(x => x.Deleted == false && x.Receiver == email)
            .OrderByDescending(y => y.Date).ToList();
        return View(result);
    }

    public ActionResult SentMessages()
    {
        email = (string)Session["customerEmail"];

        ViewBag.comingMessages = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Receiver == email);
        ViewBag.sent = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Sender == email);
        ViewBag.deletedMessages =
            _context.Messages.Where(x => x.Deleted == true).Count(y => y.Sender == email || y.Receiver == email);

        var result = _context.Messages.Where(x => x.Deleted == false && x.Sender == email).OrderByDescending(y => y.Date).ToList();
        return View(result);
    }

    public ActionResult DeletedMessages()
    {
        email = (string)Session["customerEmail"];

        ViewBag.comingMessages = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Receiver == email);
        ViewBag.sent = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Sender == email);
        ViewBag.deletedMessages =
            _context.Messages.Where(x => x.Deleted == true).Count(y => y.Sender == email || y.Receiver == email);

        var result = _context.Messages.Where(x => x.Deleted == true)
            .Where(y => y.Sender == email || y.Receiver == email).OrderByDescending(y => y.Date).ToList();
        return View(result);
    }

    public ActionResult MessageContent(int id)
    {
        email = (string)Session["customerEmail"];

        ViewBag.comingMessages = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Receiver == email);
        ViewBag.sent = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Sender == email);
        ViewBag.deletedMessages =
            _context.Messages.Where(x => x.Deleted == true).Count(y => y.Sender == email || y.Receiver == email);

        var result = _context.Messages.Where(y => y.MessageId == id)
            .FirstOrDefault(x => x.Sender == email || x.Receiver == email);

        return View(result);
    }

    [HttpGet]
    public ActionResult NewMessage()
    {
        email = (string)Session["customerEmail"];

        ViewBag.comingMessages = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Receiver == email);
        ViewBag.sent = _context.Messages.Where(x => x.Deleted == false).Count(y => y.Sender == email);
        ViewBag.deletedMessages =
            _context.Messages.Where(x => x.Deleted == true).Count(y => y.Sender == email || y.Receiver == email);

        return View();
    }
    [HttpPost]
    public ActionResult NewMessage(Message message)
    {
        email = (string)Session["customerEmail"];

        message.Sender = email;
        message.Date = DateTime.Now;

        _context.Messages.Add(message);
        _context.SaveChanges();
        return RedirectToAction("SentMessages");
    }

    public ActionResult MessageDelete(int id)
    {
        var message = _context.Messages.Find(id);
        message.Deleted = true;
        _context.SaveChanges();
        return RedirectToAction("DeletedMessages");
    }
}