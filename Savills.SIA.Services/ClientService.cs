using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;
using Savills.SIA.Repositories.Interface;
using Savills.SIA.Services.Interface;

namespace Savills.SIA.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository) { _clientRepository = clientRepository; }

        public async Task<Guid> Create(Siaclient request)
        {
           return await _clientRepository.Create(request);
        }

        public async Task<int> Delete(Guid id)
        {
            return await _clientRepository.Delete(id);
        }

        public async Task<List<Siaclient>> GetList()
        {
            return await _clientRepository.GetList();
        }

        public async Task<int> Update(UpdateClientRequest request)
        {
            return await _clientRepository.Update(request);
        }
    }
}
