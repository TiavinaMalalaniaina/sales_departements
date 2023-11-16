using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class SupplierProduct
{
    public string SupplierProductId { get; set; } = null!;

    public string? SupplierId { get; set; }

    public string? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
