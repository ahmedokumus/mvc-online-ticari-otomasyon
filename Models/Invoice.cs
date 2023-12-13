using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models
{
    public class Invoice : ModelsBase
    {
        [Key]
        public int InvoiceId { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(2)]
        public string InvoiceSerialNumber { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string InvoiceNumber { get; set; }

        public DateTime Date { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        public string TaxAdministration { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string DeliveryPerson { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string DeliveryArea { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}