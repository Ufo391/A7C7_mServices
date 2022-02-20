using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestUserService.Models;

namespace TestUserService.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly UserDbContext dbContext;

        public Repository(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            dbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            Delete(Get(id));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }

            dbContext.Set<TEntity>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            await DeleteAsync(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
            dbContext.SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public TEntity Get(Guid id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            Expression<Func<TEntity, bool>> returnAll = x => true;
            return await dbContext.Set<TEntity>().Where(predicate ?? returnAll).ToListAsync();
        }

        public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>>? predicate = null)
        {
            Expression<Func<TEntity, bool>> returnAll = x => true;
            return dbContext.Set<TEntity>().Where(predicate ?? returnAll);
        }

        public void Save(TEntity entity)
        {
            if (dbContext.Set<TEntity>().Find(GetKey(entity)) is null)
            {
                dbContext.Set<TEntity>().Add(entity);
            }
            else
            {
                dbContext.Set<TEntity>().Update(entity);
            }

            dbContext.SaveChanges();
        }

        public async Task SaveAsync(TEntity entity)
        {
            if (dbContext.Set<TEntity>().Find(GetKey(entity)) is null)
            {
                dbContext.Set<TEntity>().Add(entity);
            }
            else
            {
                dbContext.Set<TEntity>().Update(entity);
            }

            await dbContext.SaveChangesAsync();
        }

        public void SaveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (dbContext.Set<TEntity>().Find(GetKey(entity)) is null)
                {
                    dbContext.Set<TEntity>().Add(entity);
                }
                else
                {
                    dbContext.Set<TEntity>().Update(entity);
                }
            }

            dbContext.SaveChanges();
        }

        public async Task SaveRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (dbContext.Set<TEntity>().Find(GetKey(entity)) is null)
                {
                    dbContext.Set<TEntity>().Add(entity);
                }
                else
                {
                    dbContext.Set<TEntity>().Update(entity);
                }
            }

            await dbContext.SaveChangesAsync();
        }

        private Guid GetKey(TEntity entity)
        {
            var keyName = dbContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            return (Guid)entity.GetType().GetProperty(keyName).GetValue(entity, null);
        }
    }
}
