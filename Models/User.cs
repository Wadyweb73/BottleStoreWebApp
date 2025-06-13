using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottleStoreWebApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required][StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [Required][StringLength(100)][EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required][StringLength(255)]
        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public UserType UserType { get; set; }

        public bool IsApproved { get; set; } = false;
        public bool IsActive   { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public virtual ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();

        public virtual ICollection<UserApprovalRequest> ApprovalRequests { get; set; } = new List<UserApprovalRequest>();
        public virtual ICollection<UserApprovalRequest> ApprovedRequests { get; set; } = new List<UserApprovalRequest>();
    }
}

