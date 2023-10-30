using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class CustomerType
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
