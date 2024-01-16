using System;
using System.Collections.Generic;

namespace SmartWareData.Models;

public partial class Movement
{
    public long MovementId { get; set; }

    public long? ArticleId { get; set; }

    public string Date { get; set; } = null!;

    public int MovementType { get; set; }

    public int Reason { get; set; }

    public string? ProjectCode { get; set; }

    public string? UrlLink { get; set; }

    public string? Note { get; set; }

    public int Quantity { get; set; }

    public double Cost { get; set; }

    public virtual Article? Article { get; set; }
}
