using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TIPS.DataSource
{
    public interface IRepository<TEntity> where TEntity : class
    {

        #region Create / Update / Delete
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);

        #endregion

        #region Query

        DbSet<TEntity> GetDbSet();
        IQueryable<TEntity> GetQueryable(bool ignoreGlobalFilter = false);
        TEntity FindByKeyValues(object[] keyValues);
        Task<TEntity> FindByKeyValues(object[] keyValues, CancellationToken cancellationToken = default);
        bool Exist(Expression<Func<TEntity, bool>> where);
        TEntity Find(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties = "");
        TEntity Find(Expression<Func<TEntity, bool>> where, string includeProperties = "");
        IList<dynamic> FindDynamicList<T>(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> dynamicField) where T : class;
        IList<T> FindList<T>(Func<DbContext, IQueryable<T>> fnQuery, Expression<Func<T, bool>> where, string includeProperties = "") where T : class;
        IList<T> FindList<T>(Expression<Func<T, bool>> where, string includeProperties = "") where T : class;
        IList<T> FindTopList<T>(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int topCount, string includeProperties = "") where T : class;
        TProperty FindProperty<TEntity, TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> propertyFilter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null) where TEntity : class;
        IList<TProperty> FindPropertyList<TEntity, TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> propertyFilter) where TEntity : class;
        IList<TProperty> FindPropertyList<TEntity, TProperty>(IList<Expression<Func<TEntity, bool>>> whereList, Expression<Func<TEntity, TProperty>> propertyFilter) where TEntity : class;
        IList<TEntity> SqlQuery(string sql, params object[] parameters);
        IList<IDictionary<string, object>> SqlQueryDic(string sql, params object[] parameters);
        TEntity SqlSingleQuery(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, params object[] parameters);
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default, params object[] parameters);

        #endregion

        #region Aggregation

        int Count(Expression<Func<TEntity, bool>> where);
        int Count(IList<Expression<Func<TEntity, bool>>> whereList);
        TMax Max<TEntity, TMax>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TMax>> sumFilter) where TEntity : class;
        decimal SumDecimal(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, decimal?>> sumFilter);
        decimal SumDecimal(IList<Expression<Func<TEntity, bool>>> where, Expression<Func<TEntity, decimal?>> sumFilter);
        long SumLong(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, long?>> sumFilter);
        long SumLong(IList<Expression<Func<TEntity, bool>>> where, Expression<Func<TEntity, long?>> sumFilter);

        #endregion

        #region Get
        
        Task<TEntity?> GetById(int id);
        IQueryable<TEntity> FindQueryable(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>>? expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string includeProperties);
        List<TEntity> ToList();
        Task<List<TEntity>> ToListAsync();
        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        #endregion
    }
}
