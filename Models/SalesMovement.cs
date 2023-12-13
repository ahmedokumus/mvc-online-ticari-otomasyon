using System;
using System.ComponentModel.DataAnnotations;

namespace MvcOnlineTicariOtomasyon.Models
{
    public class SalesMovement : ModelsBase
    {
        [Key]
        public int SalesMovementId { get; set; }
        public DateTime Date { get; set; }
        public int Piece { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}