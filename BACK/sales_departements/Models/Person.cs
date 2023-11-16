using System;
using System.Collections.Generic;

namespace sales_departements.Models;

public partial class Person
{
    public string PersonId { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Department> Departments { get; } = new List<Department>();

    public virtual Employee? Employee { get; set; }
}
