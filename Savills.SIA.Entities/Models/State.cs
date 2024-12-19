using System;
using System.Collections.Generic;

namespace Savills.SIA.Entities.Models;

public partial class State
{
    public string CountryCode { get; set; } = null!;

    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public int Seq { get; set; }

    public int? MapZoomLevel { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country CountryCodeNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
