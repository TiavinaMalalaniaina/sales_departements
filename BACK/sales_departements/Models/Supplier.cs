using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class Supplier
{
    public string SupplierId { get; set; } = null!;

    public string? Name { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Proforma> Proformas { get; } = new List<Proforma>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; } = new List<PurchaseOrder>();

    public virtual ICollection<SupplierProduct> SupplierProducts { get; } = new List<SupplierProduct>();
}
