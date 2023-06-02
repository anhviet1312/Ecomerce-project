using System;
using System.Collections.Generic;

namespace ProjectWebMVC.Models;

public partial class Order
{
    public int Id { get; set; }

    public string? Userid { get; set; }

    public double? Total { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
