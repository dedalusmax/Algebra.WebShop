using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Algebra.WebShop.App.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayName("Created on"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateTimeCreated { get; set; }

    [Required]
    [DisplayName("Grand total")]
    [Column(TypeName = "decimal(9, 2)")]
    public decimal Total { get; set; }

    [Required(ErrorMessage = "Customer's first name is required.")]
    [DisplayName("Customer's first name")]
    [StringLength(50)]
    public string CustomerFirstName { get; set; }

    [Required(ErrorMessage = "Customer's last name is required.")]
    [DisplayName("Customer's last name")]
    [StringLength(50)]
    public string CustomerLastName { get; set; }

    [Required(ErrorMessage = "Customer's email is required.")]
    [DisplayName("Customer's email address")]
    [StringLength(50), EmailAddress]
    public string CustomerEmailAddress { get; set; }

    [Required(ErrorMessage = "Customer's phone number is required.")]
    [DisplayName("Customer's phone number")]
    [StringLength(50)]
    public string CustomerPhoneNumber { get; set; }

    [Required(ErrorMessage = "Customer's address is required.")]
    [DisplayName("Customer's address")]
    [StringLength(250)]
    public string CustomerAddress { get; set; }

    [ForeignKey("OrderId")]
    public virtual ICollection<OrderItem> Items { get; set; }

    [Required]
    public string UserId { get; set; }

    public virtual ApplicationUser User { get; set; }
}
