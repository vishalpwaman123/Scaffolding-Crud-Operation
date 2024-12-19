using System;
using System.Collections.Generic;

namespace Savills.SIA.Entities.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public Guid? CityId { get; set; }

    public Guid? StateId { get; set; }

    public string? Zip { get; set; }

    public string CountryCode { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public string BusinessCountry { get; set; } = null!;

    public string BusinessCountryCode { get; set; } = null!;

    public string Company { get; set; } = null!;

    public string? LicenseNumber { get; set; }

    public string? OfficeNumber { get; set; }

    public string? Teams { get; set; }

    public bool IsActive { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public string? Title { get; set; }

    public bool? IsApplicationUser { get; set; }

    public virtual City? City { get; set; }

    public virtual CountryDepartment CountryDepartment { get; set; } = null!;

    public virtual ICollection<Siaclient> SiaclientCreatedByNavigations { get; set; } = new List<Siaclient>();

    public virtual ICollection<Siaclient> SiaclientLastUpdatedByNavigations { get; set; } = new List<Siaclient>();

    public virtual State? State { get; set; }
}
