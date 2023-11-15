using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class PurchaseOrderDetail
{
    public string PurchaseOrderDetailsId { get; set; } = null!;

    public string? PurchaseOrderId { get; set; }

    public string? ProductId { get; set; }

    public double? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Product? Product { get; set; }
}
