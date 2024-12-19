using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Savills.SIA.Entities.Models;
using TIPS.DataSource;

namespace Savills.SIA.Entities
{
    public class UnitOfWork : IUnitOfWork
    {
        private ModelContext _context;
        private IDbContextTransaction _dbTransaction;
        private bool _disposed;

        public UnitOfWork(ModelContext context) => _context = context;

        public IRepository<Siaclient> _Siaclient;
        public virtual IRepository<Siaclient> Siaclient
        {
            get
            {
                if (_Siaclient != null)
                {
                    return _Siaclient;
                }
                _Siaclient = new Repository<Siaclient>(_context);
                return _Siaclient;
            }
        }

        public IRepository<ContactType> _contactType;
        public virtual IRepository<ContactType> ContactType
        {
            get
            {
                if (_contactType != null)
                {
                    return _contactType;
                }
                _contactType = new Repository<ContactType>(_context);
                return _contactType;
            }
        }

        public IRepository<Country> _country;
        public virtual IRepository<Country> Country
        {
            get
            {
                if (_country != null)
                {
                    return _country;
                }
                _country = new Repository<Country>(_context);
                return _country;
            }
        }

        public virtual void BeginTransaction()
        {
            _dbTransaction = _context.Database.BeginTransaction();
        }

        public virtual void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (!disposing) return;


            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }

            _disposed = true;
        }

        public async Task<decimal> NextSequence(string sequenceName)
        {
            /*var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = $"SELECT SWBAPPS.{sequenceName}.NEXTVAL FROM DUAL";
            var nextSequence = (decimal?)(await command.ExecuteScalarAsync());
            return nextSequence.Value;*/
            throw new NotImplementedException();
        }

        public virtual void Rollback()
        {
            _dbTransaction.Rollback();
        }

        public Task<int> SaveChangesAsync()
        {
           return _context.SaveChangesAsync();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
