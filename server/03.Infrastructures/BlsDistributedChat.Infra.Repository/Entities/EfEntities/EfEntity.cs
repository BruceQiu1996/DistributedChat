namespace BlsDistributedChat.Infra.Repository.Entities.EfEntities
{
    public abstract class EfEntity<TKey> : Entity<TKey>, IEfEntity<TKey>
    {
    }
}
