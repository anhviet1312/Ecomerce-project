using System;
using System.Collections.Generic;

namespace ProjectWebMVC.Models;

public partial class BalanceChange
{
    public int Id { get; set; }

    public string? Userid { get; set; }

    public double? Value { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public virtual User? User { get; set; }
}
