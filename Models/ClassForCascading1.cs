using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Models;

public class ClassForCascading1
{
    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<SelectListItem> Products { get; set; }
}