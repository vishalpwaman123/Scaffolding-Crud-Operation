using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Savills.SIA.Entities.Models;
using TIPS.DataSource;

namespace Savills.SIA.Entities
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Siaclient> Siaclient {  get; }
        IRepository<ContactType> ContactType { get; }
        IRepository<Country> Country { get; }
        Task<decimal> NextSequence(string sequenceName);
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
