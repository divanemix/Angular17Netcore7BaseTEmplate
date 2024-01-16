using System;
using System.Collections.Generic;

namespace SmartWare;

public partial class ArticleDTO
{
    public long ArticleId { get; set; }

    public int ArticleType { get; set; }

    public string Name { get; set; } = null!;

    public string PartNumber { get; set; } = null!;

    public string? Note { get; set; }

    public string? Category { get; set; }

    public string? SubCategory1 { get; set; }

    public string? SubCategory2 { get; set; }

    public string? SubCategory3 { get; set; }

    public string? Room { get; set; }

    public string? Shelf { get; set; }

    public string? Box { get; set; }

    public int? Tag { get; set; }

    public int? Quantity { get; set; }

    public double Cost { get; set; }

    public double? Price { get; set; }

}
