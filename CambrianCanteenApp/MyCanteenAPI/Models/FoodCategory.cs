using System;
using System.Collections.Generic;

namespace MyCanteenAPI.Models;

public partial class FoodCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
}
