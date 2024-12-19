using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Savills.SIA.Entities.Models;
using Savills.SIA.Repositories.Interface;
using Savills.SIA.Services.Interface;

namespace Savills.SIA.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contatRepository;
        public ContactService(IContactRepository contactRepository) => _contatRepository = contactRepository;
        public async Task<string> Create(ContactType request)
        {
            return await _contatRepository.Create(request);
        }

        public async Task<int> Delete(Guid id)
        {
            return await _contatRepository.Delete(id);
        }

        public async Task<List<ContactType>> GetList()
        {
            return await _contatRepository.GetList();
        }

        public async Task<string> Update(ContactType request)
        {
            return await _contatRepository.Update(request);
        }
    }
}
