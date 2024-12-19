using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;

namespace Savills.SIA.Repositories.Interface
{
    public interface IContactRepository
    {
        Task<string> Create(ContactType request);
        Task<string> Update(ContactType request);
        Task<List<ContactType>> GetList();
        Task<int> Delete(Guid id);
    }
}
