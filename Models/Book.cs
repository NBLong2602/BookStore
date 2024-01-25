using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Book
{
    public int Isbn { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    public int BookCategoryId { get; set; }

    public string? BookName { get; set; }

    public float Price { get; set; }

    public string? PictureThumb { get; set; }

    public int PublishYear { get; set; }

    public string? Language { get; set; }

    public int? Status { get; set; }

    public float Discount { get; set; }

    public int TotalPage { get; set; }

    public string? Description { get; set; }

    public int Stock { get; set; }

    public virtual Author? Author { get; set; } 

    public virtual BookCategory? BookCategory { get; set; }

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
