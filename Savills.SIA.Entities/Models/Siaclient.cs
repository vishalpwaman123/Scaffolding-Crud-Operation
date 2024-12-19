using System;
using System.Collections.Generic;

namespace Savills.SIA.Entities.Models;

public partial class Siaclient
{
    public Guid Id { get; set; }

    public string ContactTypeCode { get; set; } = null!;

    public string? OrganizationName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string SourceCountryCode { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public string? SicdivisionCode { get; set; }

    public string? SicmajorCode { get; set; }

    public string? OfficeDialCode { get; set; }

    public string? OfficeNumber { get; set; }

    public string? MobileDialCode { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? MailingAddress { get; set; }

    public string? BusinessRegistrationNumber { get; set; }

    public string? Website { get; set; }

    public string Status { get; set; } = null!;

    public Guid? ClientId { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;

    public DateTime? LastUpdateDate { get; set; } = DateTime.Now;

    public Guid CreatedBy { get; set; }

    public Guid? LastUpdatedBy { get; set; } 

    public virtual ContactType ContactTypeCodeNavigation { get; set; } = null!;

    public virtual Country CountryCodeNavigation { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? LastUpdatedByNavigation { get; set; }

    public virtual Country SourceCountryCodeNavigation { get; set; } = null!;
}
