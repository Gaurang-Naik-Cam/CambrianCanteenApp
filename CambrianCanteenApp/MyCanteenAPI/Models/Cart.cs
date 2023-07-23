using System;
using System.Collections.Generic;

namespace MyCanteenAPI.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int FoodItemId { get; set; }

    public int Studentid { get; set; }

    public DateTime? AddedOn { get; set; }

    public int Qty { get; set; }

    public virtual FoodItem FoodItem { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
