using System;
using System.Collections.Generic;

namespace MyCanteenAPI.Models;

public partial class FoodItem
{
    public int Id { get; set; }

    public string ItemName { get; set; } = null!;

    public int? FoodCategoryId { get; set; }

    public float? Price { get; set; }

    public string? DayOfTheWeek { get; set; }

    public virtual FoodCategory? FoodCategory { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
