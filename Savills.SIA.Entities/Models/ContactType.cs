using System;
using System.Collections.Generic;

namespace Savills.SIA.Entities.Models;

public partial class ContactType
{
    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Seq { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Siaclient> Siaclients { get; set; } = new List<Siaclient>();
}
