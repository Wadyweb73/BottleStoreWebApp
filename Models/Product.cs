using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottleStoreWebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ProductCategory { get; set; } = string.Empty;

        [StringLength(50)]
        public string? ProductSize { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<ProductInformation> ProductInformations { get; set; } = new List<ProductInformation>();
        public virtual ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
 