using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;

namespace Savills.SIA.API.Extensions.ClientMapper
{
    public class ClientMapper : AutoMapper.Profile
    {
        public ClientMapper()
        {
            CreateMap<CreateClientRequest, Siaclient>();
        }
    }
}
