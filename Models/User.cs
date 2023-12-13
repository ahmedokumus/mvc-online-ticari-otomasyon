using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models
{
    public class User : ModelsBase
    {
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Password { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string Role { get; set; }
    }
}