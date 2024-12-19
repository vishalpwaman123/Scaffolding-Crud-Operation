
using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;

namespace Savills.SIA.Services.Interface
{
    public interface IClientService
    {
        Task<Guid> Create(Siaclient request);
        Task<int> Update(UpdateClientRequest request);
        Task<List<Siaclient>> GetList();
        Task<int> Delete(Guid id);
    }
}
