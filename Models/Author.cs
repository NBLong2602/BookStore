using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Author
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
