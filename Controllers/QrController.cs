using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Breadcrumb;
using QRCoder;

namespace MvcOnlineTicariOtomasyon.Controllers;

//[Authorize]
[BreadCrumb(Manual = false)]
public class QrController : Controller
{
    [BreadCrumb(Clear = true)]
    [HttpGet]
    public ActionResult Generator()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Generator(string code)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = generator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            using (Bitmap image = qrCode.GetGraphic(10))
            {
                image.Save(memoryStream, ImageFormat.Png);
                ViewBag.qrCode = "data:image/png/;base64," + Convert.ToBase64String(memoryStream.ToArray());
            }
        }
        ViewBag.code = code;
        return View();
    }
}