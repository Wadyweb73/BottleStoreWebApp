using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottleStoreWebApp.Models  
{
    public class UserApprovalRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public ApprovalStatus Status { get; set; } = ApprovalStatus.Pendente;

        [StringLength(500)]
        public string? Comments { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("ApprovedBy")]
        public virtual User? ApprovedByUser { get; set; }
    }

    public enum UserType
    {
        Admin = 1,
        Funcionario = 2
    }

    public enum ApprovalStatus
    {
        Pendente = 0,
        Aprovado = 1,
        Rejeitado = 2
    }
}
