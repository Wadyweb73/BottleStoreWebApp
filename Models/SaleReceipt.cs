using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottleStoreWebApp.Models 
{
    public class SaleReceipt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleId { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string? CustomerName { get; set; }

        [StringLength(20)]
        public string? CustomerPhone { get; set; }

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        // Navigation Property
        [ForeignKey("SaleId")]
        public virtual Sale Sale { get; set; } = null!;
    }
}
