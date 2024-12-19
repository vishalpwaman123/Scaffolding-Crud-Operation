using System;
using System.Collections.Generic;

namespace Savills.SIA.Entities.Models;

public partial class Country
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public int Seq { get; set; }

    public int? MapZoomLevel { get; set; }

    public string? DialCode { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Siaclient> SiaclientCountryCodeNavigations { get; set; } = new List<Siaclient>();

    public virtual ICollection<Siaclient> SiaclientSourceCountryCodeNavigations { get; set; } = new List<Siaclient>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
