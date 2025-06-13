using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottleStoreWebApp.Models
{
    public class InventoryLog 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int MovementType { get; set; }

        [Required] 
        public int UserId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int PreviousStock { get; set; }

        [Required]
        [StringLength(50)]
        public string Reason { get; set; } = string.Empty;

        public int NewStock { get; set; }

        [Required]
        public DateTime MovementDate { get; set; }

        // Navigation Properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}
