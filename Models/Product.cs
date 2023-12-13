using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models
{
    public class Product : ModelsBase
    {
        [Key]
        public int ProductId { get; set; }//Id

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string ProductName { get; set; }//Ürün Adı

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string ProductBrand { get; set; }//Marka

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string ProductImage { get; set; }//Görsel

        public short Stock { get; set; }//Stok

        public decimal PurchasePrice { get; set; }//Alış Fiyatı

        public decimal SalePrice { get; set; }//Satış Fiyatı

        //public decimal ProductTax { get; set; }//Vergi

        public bool Status { get; set; }//Durum

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<SalesMovement> SalesMovements { get; set; }
    }
}