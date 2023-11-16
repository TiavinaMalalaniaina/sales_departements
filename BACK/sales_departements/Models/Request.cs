using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class Request
{
    public string RequestId { get; set; } = null!;

    public string? DepartmentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsValidated { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<RequestDetail> RequestDetails { get; } = new List<RequestDetail>();
}
