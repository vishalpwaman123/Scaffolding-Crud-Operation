using Savills.SIA.Entities;
using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;
using Savills.SIA.Repositories.Interface;

namespace Savills.SIA.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Create(Siaclient request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var result = _unitOfWork.Siaclient.Add(request);
                await _unitOfWork.SaveChangesAsync();
                return result.Id;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Siaclient>> GetList()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(UpdateClientRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
