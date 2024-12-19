using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;

namespace Savills.SIA.Services.Interface
{
    public interface IContactService
    {
        Task<string> Create(ContactType request);
        Task<string> Update(ContactType request);
        Task<List<ContactType>> GetList();
        Task<int> Delete(Guid id);
    }
}
