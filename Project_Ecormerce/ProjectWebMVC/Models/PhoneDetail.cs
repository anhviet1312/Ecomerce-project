using System;
using System.Collections.Generic;

namespace ProjectWebMVC.Models;

public partial class PhoneDetail
{
    public int Pdid { get; set; }

    public int? Ram { get; set; }

    public int? Storage { get; set; }

    public int? Camera { get; set; }

    public int? Pin { get; set; }

    public virtual Phone Pd { get; set; } = null!;
}
