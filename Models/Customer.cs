using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Customer
{
    public int Id { get; set; }

    public int CustomerTypeId { get; set; }

    public string? FullName { get; set; }

    public bool Gender { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual CustomerType CustomerType { get; set; } = null!;

    public virtual ICollection<IventoryProduct> IventoryProducts { get; set; } = new List<IventoryProduct>();

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();
}
