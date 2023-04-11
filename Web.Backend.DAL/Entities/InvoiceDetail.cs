﻿using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class InvoiceDetail
{
    public int Id { get; set; }

    public int? PurchaseId { get; set; }

    public string? Detail { get; set; }

    public int? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }
}
