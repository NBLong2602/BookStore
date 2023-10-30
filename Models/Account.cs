using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
