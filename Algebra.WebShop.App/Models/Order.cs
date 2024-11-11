using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Algebra.WebShop.App.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DateTimeCreated { get; set; }

    [Required]
    [Column(TypeName = "decimal(9, 2)")]
    public decimal Total { get; set; }

    [Required(ErrorMessage = "Customer's first name is required.")]
    [StringLength(50)]
    public string CustomerFirstName { get; set; }

    [Required(ErrorMessage = "Customer's last name is required.")]
    [StringLength(50)]
    public string CustomerLastName { get; set; }

    [Required(ErrorMessage = "Customer's email is required.")]
    [StringLength(50)]
    public string CustomerEmailAddress { get; set; }

    [Required(ErrorMessage = "Customer's phone number is required.")]
    [StringLength(50)]
    public string CustomerPhoneNumber { get; set; }

    [Required(ErrorMessage = "Customer's address is required.")]
    [StringLength(50)]
    public string CustomerAddress { get; set; }

    [ForeignKey("OrderId")]
    public virtual ICollection<OrderItem> Items { get; set; }

    //public string UserId { get; set; }
}
