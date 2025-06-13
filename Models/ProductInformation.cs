using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottleStoreWebApp.Models
{
 public class ProductInformation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int AmountBoxes { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string? ProductSupplier { get; set; }

        // Navigation Property
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;
    }
}
