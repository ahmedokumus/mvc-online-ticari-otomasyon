using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly Context _context = new Context();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user, string radioGroup)
        {
            ViewBag.style = "color:red";
            switch (radioGroup)
            {
                case "customer":
                    var resultCustomer = _context.Customers.FirstOrDefault(x =>
                        x.Email == user.Email && x.Password == user.Password);
                    if (resultCustomer != null)
                    {
                        FormsAuthentication.SetAuthCookie(resultCustomer.Email, false);
                        Session["customerEmail"] = resultCustomer.Email;
                        return RedirectToAction("Profile", "CustomerPanel");
                    }
                    ViewBag.userInformationError = "E posta veya parola hatalı";
                    break;
                case "employee":
                    var resultEmployee = _context.Employees.FirstOrDefault(x =>
                        x.Email == user.Email && x.Password == user.Password);
                    if (resultEmployee != null)
                    {
                        FormsAuthentication.SetAuthCookie(resultEmployee.Email, false);
                        Session["employeeEmail"] = resultEmployee.Email;
                        Session["employeeName"] =
                            $"{resultEmployee.EmployeeFirstName} {resultEmployee.EmployeeLastName}";
                        Session["employeeImg"] = resultEmployee.EmployeeImage;
                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.userInformationError = "E posta veya parola hatalı";
                    break;
                default:
                    ViewBag.error = "Giriş Türü Seçmediniz";
                    break;
            }
            return View();
        }

        [HttpGet]
        public ActionResult CustomerRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerRegister(Customer customer)
        {
            customer.Role = "customer";
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Login", "Login");
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}