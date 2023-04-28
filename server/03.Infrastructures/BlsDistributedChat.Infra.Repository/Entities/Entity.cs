namespace BlsDistributedChat.Infra.Repository.Entities
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get ; set ; }
    }
}
