using System.Linq.Expressions;

namespace TestUserService.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        Task<TEntity> GetAsync(Guid id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);

        IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>>? predicate = null);

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        void DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        void Save(TEntity entity);
        Task SaveAsync(TEntity entity);

        void SaveRange(IEnumerable<TEntity> entities);
        Task SaveRangeAsync(IEnumerable<TEntity> entities);
    }
}
