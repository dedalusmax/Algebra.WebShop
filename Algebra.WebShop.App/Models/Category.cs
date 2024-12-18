﻿using System.ComponentModel.DataAnnotations;

namespace Algebra.WebShop.App.Models;

public class Category
{
    [Key]
    public required int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public required string Name { get; set; }

    public required virtual ICollection<ProductCategory> Products { get; set; }
}
