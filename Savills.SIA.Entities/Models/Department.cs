using System;
using System.Collections.Generic;

namespace Savills.SIA.Entities.Models;

public partial class Department
{
    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<CountryDepartment> CountryDepartments { get; set; } = new List<CountryDepartment>();
}
