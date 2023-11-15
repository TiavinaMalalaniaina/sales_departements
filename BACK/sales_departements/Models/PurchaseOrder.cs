using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class PurchaseOrder
{
    public string PurchaseOrderId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? DeliveryDays { get; set; }

    public string? SupplierId { get; set; }

    public int? Validation { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
