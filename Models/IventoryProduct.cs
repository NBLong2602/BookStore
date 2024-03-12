using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class IventoryProduct
{
    public int Id { get; set; }

    public int? MaSach { get; set; }

    public int? SoLuongNhap { get; set; }

    public decimal? DonGiaNhap { get; set; }

    public decimal? Vat { get; set; }

    public decimal? ThanhTien { get; set; }

    public DateTime? NgayNhap { get; set; }

    public int? TrangThaiDuyet { get; set; }

    public int? UserId { get; set; }

    public virtual Book? MaSachNavigation { get; set; }

    public virtual Customer? User { get; set; }
}
