using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class Proforma
{
    public string ProformaId { get; set; } = null!;

    public DateOnly? IssueDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public string? SupplierId { get; set; }

    public virtual ICollection<ProformaDetail> ProformaDetails { get; } = new List<ProformaDetail>();

    public virtual Supplier? Supplier { get; set; }
}
