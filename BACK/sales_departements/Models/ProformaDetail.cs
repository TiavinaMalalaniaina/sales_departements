using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class ProformaDetail
{
    public string ProformaDetailsId { get; set; } = null!;

    public string? ProformaId { get; set; }

    public string? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Proforma? Proforma { get; set; }
}
