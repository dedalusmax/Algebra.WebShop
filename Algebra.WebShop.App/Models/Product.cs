﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Algebra.WebShop.App.Models;

public class Product
{
    [Key]
    public required int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 2)]
    public required string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(9, 2)")]
    public decimal Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ProductCategory> Categories { get; set; }

    [ForeignKey("ProductId")]
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    [NotMapped, DisplayName("Categories")]
    public string CategoriesDisplay => 
        Categories != null ? string.Join(", ", Categories.Select(x => x.Category.Name)) : string.Empty;

    public string? FileName { get; set; }

    public byte[]? FileContent { get; set; }
}
