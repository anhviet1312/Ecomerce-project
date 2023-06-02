using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectWebMVC.Models;

public partial class Phone
{
    [Required]
    public int Pid { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public int? Cid { get; set; }

    public string? Description { get; set; }
    [Required]
    public string? Img { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int Quantity { get; set; }

    public virtual Category? CidNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PhoneDetail? PhoneDetail { get; set; }
}
