using BlsDistributedChat.Infra.Repository.Entities;
using BlsDistributedChat.Infra.Repository.Entities.EfEntities;
using System.Linq.Expressions;

namespace BlsDistributedChat.Infra.Repository.IRepositories
{
    public interface IEfBasicRepository<TEntity,Tkey> : IEfBaseRepository<TEntity, Tkey>
            where TEntity : Entity<Tkey>, IEfEntity<Tkey>
    {
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<TEntity?> GetAsync(Tkey keyValue, Expression<Func<TEntity, dynamic>>? navigationPropertyPath = null, bool writeDb = false, CancellationToken cancellationToken = default);
    }
}
