using System;
using System.Collections.Generic;

namespace ProjectWebMVC.Models;

public partial class Category
{
    public int Cid { get; set; }

    public string? Cname { get; set; }

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
