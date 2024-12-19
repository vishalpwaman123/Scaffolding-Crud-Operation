using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Savills.SIA.Entities;
using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;
using Savills.SIA.Repositories.Interface;

namespace Savills.SIA.Repositories
{
    public class ContatRepository : IContactRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelContext _context;
        public ContatRepository(IUnitOfWork unitOfWork, ModelContext context) 
        { 
            _unitOfWork = unitOfWork; 
            _context = context;
        }

        public async Task<string> Create(ContactType request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var result = _unitOfWork.ContactType.Add(request);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();
                return result.Code;
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

        public async Task<List<ContactType>> GetList()
        {
            return await _unitOfWork.ContactType.ToListAsync();
        }

        public async Task<string> Update(ContactType request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var result = _unitOfWork.ContactType.FirstOrDefault(x => x.Code == request.Code);
                result.Seq = request.Seq;
                result.Name = request.Name;
                result.IsActive = request.IsActive;
                //_unitOfWork.ContactType.Update(request);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();
                return request.Code;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    
    }
}
