﻿using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class OrderInfo
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int CustomerId { get; set; }

    public string? OrderDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
