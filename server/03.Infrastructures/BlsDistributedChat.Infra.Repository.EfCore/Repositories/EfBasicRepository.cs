using BlsDistributedChat.Infra.Repository.Entities;
using BlsDistributedChat.Infra.Repository.Entities.EfEntities;
using BlsDistributedChat.Infra.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlsDistributedChat.Infra.Repository.EfCore.Repositories
{
    public class EfBasicRepository<TEntity, TKey> : AbstractEfBaseRepository<DbContext, TEntity, TKey>, IEfBasicRepository<TEntity, TKey>
            where TEntity : Entity<TKey>, IEfEntity<TKey>
    {
        public EfBasicRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.DbContext.UpdateRange(entities);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.DbContext.Remove(entity);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.DbContext.RemoveRange(entities);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> GetAsync(TKey keyValue, Expression<Func<TEntity, dynamic>>? navigationPropertyPath = null, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var query = this.GetDbSet(writeDb, false).Where(t => t.Id.Equals(keyValue));
            if (navigationPropertyPath is null)
                return await query.FirstOrDefaultAsync(cancellationToken);
            else
                return await query.Include(navigationPropertyPath).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
