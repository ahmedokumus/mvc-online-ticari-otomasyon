using System.Collections.Generic;

namespace MvcOnlineTicariOtomasyon.Models;

public class ClassForDynamicInvoice
{
    public IEnumerable<Invoice> ValueInvoices { get; set; }
    public IEnumerable<InvoiceItem> ValueInvoiceItems { get; set; }
}