using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class ProductRating
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public decimal? Rating { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
