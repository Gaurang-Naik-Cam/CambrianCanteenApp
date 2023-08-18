using System;
using System.Collections.Generic;

namespace MyCanteenAPI.Models;

public partial class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int StudentId { get; set; }

    public DateTime CreatedOn { get; set; }

    public int StatusId { get; set; }

    public double Total { get; set; }

    public double Tax { get; set; }

    public double? ServiceCharge { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
