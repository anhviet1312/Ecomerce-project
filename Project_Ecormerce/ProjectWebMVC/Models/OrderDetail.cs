using System;
using System.Collections.Generic;

namespace ProjectWebMVC.Models;

public partial class OrderDetail
{
    public int Oid { get; set; }

    public int Pid { get; set; }

    public int? Quantity { get; set; }

    public virtual Order OidNavigation { get; set; } = null!;

    public virtual Phone PidNavigation { get; set; } = null!;
}
