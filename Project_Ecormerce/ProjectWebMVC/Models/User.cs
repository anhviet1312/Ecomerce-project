using System;
using System.Collections.Generic;

namespace ProjectWebMVC.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Admin { get; set; }

    public string? Address { get; set; }

    public string? Telephone { get; set; }

    public double? Balance { get; set; }

    public virtual ICollection<BalanceChange> BalanceChanges { get; set; } = new List<BalanceChange>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
