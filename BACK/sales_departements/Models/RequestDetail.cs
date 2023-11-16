using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class RequestDetail
{
    public string RequestDetailsId { get; set; } = null!;

    public string? RequestId { get; set; }

    public string? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Request? Request { get; set; }
}
