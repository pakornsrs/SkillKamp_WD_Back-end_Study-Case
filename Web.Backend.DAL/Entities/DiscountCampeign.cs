﻿using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class DiscountCampeign
{
    public int Id { get; set; }

    public string? NameTh { get; set; }

    public string? NameEn { get; set; }

    public decimal? DisconutPercent { get; set; }

    public string? DescTh { get; set; }

    public string? DescEn { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
