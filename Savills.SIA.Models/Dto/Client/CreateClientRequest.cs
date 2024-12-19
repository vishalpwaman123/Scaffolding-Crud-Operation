using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savills.SIA.Models.Dto.Client
{
    public class CreateClientRequest
    {
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
    }
}
