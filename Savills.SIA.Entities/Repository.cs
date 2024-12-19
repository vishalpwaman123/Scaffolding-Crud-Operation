using Microsoft.EntityFrameworkCore;
using Savills.SIA.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace TIPS.DataSource
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ModelContext _dbContext;

        public Repository(ModelContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Create / Update / Delete

        public TEntity Add(TEntity entity)
        {
            return _dbContext.Set<TEntity>().Add(entity).Entity;
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            return Task.FromResult(Add(entity));
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        #endregion

        #region Query

        public DbSet<TEntity> GetDbSet()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQueryable(bool ignoreGlobalFilter = false)
        {
            if (ignoreGlobalFilter)
                return _dbContext.Set<TEntity>().IgnoreQueryFilters();
            return _dbContext.Set<TEntity>();
        }

        #endregion

        #region Get

        public async Task<TEntity?> GetById(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> FindQueryable(Expression<Func<TEntity, bool>> expression,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var query = _dbContext.Set<TEntity>().Where(expression);
            return orderBy != null ? orderBy(query) : query;
        }

        public Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>>? expression, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default)
            
        {
            var query = expression != null ? _dbContext.Set<TEntity>().Where(expression) : _dbContext.Set<TEntity>();
            return orderBy != null
            ? orderBy(query).ToListAsync(cancellationToken)
                : query.ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken)
        {
            return _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string includeProperties)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                => current.Include(includeProperty));

            return query.SingleOrDefaultAsync(expression);
        }

        public List<TEntity> ToList()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public Task<List<TEntity>> ToListAsync()
        {
            return _dbContext.Set<TEntity>().ToListAsync();
        }

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(expression);
        }

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        #endregion

        #region Aggregation

        public TEntity FindByKeyValues(object[] keyValues)
        {
            try
            {
                return _dbContext.Set<TEntity>().Find(keyValues);
            }
            catch (ValidationException dbEx)
            {
                throw dbEx;
            }
        }

        public Task<TEntity> FindByKeyValues(object[] keyValues, CancellationToken cancellationToken = default)
        {
            try
            {
                return _dbContext.Set<TEntity>().FindAsync(keyValues).AsTask();
            }
            catch (ValidationException dbEx)
            {
                throw dbEx;
            }
        }

        public bool Exist(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            var result = query.Any();

            return result;
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            query = Include(query, includeProperties);
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            var result = query.AsNoTracking().FirstOrDefault();

            return result;
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            query = Include(query, includeProperties);
            var result = query.AsNoTracking().FirstOrDefault();

            return result;
        }

        IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> query, string includeProperties) where TEntity : class
        {
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }

        public IList<dynamic> FindDynamicList<T>(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> dynamicField) where T : class
        {
            var query = _dbContext.Set<T>().Where(where);
            var result = query.Select(dynamicField).AsNoTracking().ToList();

            return result;
        }

        public IList<T> FindList<T>(Func<DbContext, IQueryable<T>> fnQuery, Expression<Func<T, bool>> where, string includeProperties = "") where T : class
        {
            if (fnQuery == null)
            {
                return null;
            }
            IQueryable<T> query = fnQuery(_dbContext);
            query = query.Where(where);
            query = Include(query, includeProperties);
            var result = query.AsNoTracking().ToList();

            return result;
        }

        public IList<T> FindList<T>(Expression<Func<T, bool>> where, string includeProperties = "") where T : class
        {
            var query = _dbContext.Set<T>().Where(where);
            query = Include(query, includeProperties);
            var result = query.AsNoTracking().ToList();

            return result;
        }

        public IList<T> FindTopList<T>(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int topCount, string includeProperties = "") where T : class
        {
            var query = _dbContext.Set<T>().Where(where);
            query = Include(query, includeProperties);
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            var result = query.Take(topCount).AsNoTracking().ToList();

            return result;
        }

        public TProperty FindProperty<TEntity, TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> propertyFilter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null) where TEntity : class
        {

            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            var result = query.Select(propertyFilter).FirstOrDefault();

            return result;
        }

        public IList<TProperty> FindPropertyList<TEntity, TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> propertyFilter) where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            var result = query.Select(propertyFilter).ToList();

            return result;
        }

        public IList<TProperty> FindPropertyList<TEntity, TProperty>(IList<Expression<Func<TEntity, bool>>> whereList, Expression<Func<TEntity, TProperty>> propertyFilter) where TEntity: class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            foreach (var where in whereList)
            {
                query = query.Where(where);
            }
            var result = query.Select(propertyFilter).ToList();

            return result;
        }

        public IList<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(sql, parameters).ToList();
        }

        public IList<IDictionary<string, object>> SqlQueryDic(string sql, params object[] parameters)
        {
            var conn = _dbContext.Database.GetDbConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            conn.Open();
            var dicList = new List<IDictionary<string, object>>();
            IDictionary<string, object> tempEntDic = null; ;
            object tempVal = null;
            var r = cmd.ExecuteReader();
            while (r.Read())
            {
                tempEntDic = new Dictionary<string, object>();
                for (int i = 0; i < r.FieldCount; i++)
                {
                    tempVal = r.GetValue(i);
                    if (tempVal is DBNull)
                        tempVal = null;
                    tempEntDic.Add(r.GetName(i), tempVal);
                }
                dicList.Add(tempEntDic);
            }
            conn.Close();

            return dicList;
        }

        public TEntity SqlSingleQuery<T>(string sql, params object[] parameters)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(sql, parameters).SingleOrDefault();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            var conn = _dbContext.Database.GetDbConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            conn.Open();
            var result = cmd.ExecuteNonQuery();
            conn.Close();

            return result;
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default, params object[] parameters)
        {
            var conn = _dbContext.Database.GetDbConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            conn.Open();
            var result = cmd.ExecuteNonQueryAsync(cancellationToken);
            conn.Close();

            return result;
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            var result = query.Count();

            return result;
        }

        public int Count(IList<Expression<Func<TEntity, bool>>> whereList)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            foreach (var where in whereList)
            {
                query = query.Where(where);
            }
            var result = query.Count();

            return result;
        }

        public TMax Max<TEntity, TMax>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TMax>> sumFilter) where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            var result = query.Max<TEntity, TMax>(sumFilter);

            return result;
        }

        public decimal SumDecimal(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, decimal?>> sumFilter)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            var result = query.Sum(sumFilter);

            if (result == null)
                return 0;
            return result.Value;
        }

        public decimal SumDecimal(IList<Expression<Func<TEntity, bool>>> where, Expression<Func<TEntity, decimal?>> sumFilter)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (where != null)
            {
                foreach (var filter in where)
                {
                    query = query.Where(filter);
                }
            }
            var result = query.Sum(sumFilter);

            if (result == null)
                return 0;
            return result.Value;
        }

        public long SumLong(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, long?>> sumFilter)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(where);
            var result = query.Sum<TEntity>(sumFilter);

            if (result == null)
                return 0;
            return result.Value;
        }

        public long SumLong(IList<Expression<Func<TEntity, bool>>> where, Expression<Func<TEntity, long?>> sumFilter)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (where != null)
            {
                foreach (var filter in where)
                {
                    query = query.Where(filter);
                }
            }
            var result = query.Sum<TEntity>(sumFilter);

            if (result == null)
                return 0;
            return result.Value;
        }

        public TEntity SqlSingleQuery(string sql, params object[] parameters)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(sql, parameters).SingleOrDefault();
        }

        #endregion
    
    }
}
