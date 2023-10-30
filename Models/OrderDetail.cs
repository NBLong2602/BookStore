using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class OrderDetail
{
    public int BookIsbn { get; set; }

    public int OrderId { get; set; }

    public string TransactionDate { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Note { get; set; }

    public virtual Book BookIsbnNavigation { get; set; } = null!;

    public virtual OrderInfo Order { get; set; } = null!;
}
