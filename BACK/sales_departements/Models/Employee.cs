using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string? PersonId { get; set; }

    public string? DepartmentId { get; set; }

    public DateOnly? HireDate { get; set; }

    public string? JobTitle { get; set; }

    public decimal? Salary { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Person? Person { get; set; }
}
