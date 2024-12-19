using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Contact;

namespace Savills.SIA.API.Extensions.ClientMapper
{
    public class ContactMapper : AutoMapper.Profile
    {
        public ContactMapper() 
        {
            CreateMap<CreateContactTypeRequest, ContactType>();
            CreateMap<UpdateContactTypeRequest, ContactType>();
        }
    }
}
