using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public bool Gender { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public float? Salary { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();
}
