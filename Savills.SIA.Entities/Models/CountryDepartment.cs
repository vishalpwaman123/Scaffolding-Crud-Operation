using System;
using System.Collections.Generic;

namespace Savills.SIA.Entities.Models;

public partial class CountryDepartment
{
    public Guid Id { get; set; }

    public string CountryCode { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public byte IsActive { get; set; }

    public virtual Department DepartmentCodeNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
