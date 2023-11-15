using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class Department
{
    public string DepartmentId { get; set; } = null!;

    public string? DepartmentName { get; set; }

    public string? DepartmentHeadId { get; set; }

    public virtual Person? DepartmentHead { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<Request> Requests { get; } = new List<Request>();
}
