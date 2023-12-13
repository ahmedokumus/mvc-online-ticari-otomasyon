using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models
{
    public class InvoiceItem : ModelsBase
    {
        [Key]
        public int InvoiceItemsId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Description { get; set; }

        public int Piece { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal ItemsTotalPrice { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}