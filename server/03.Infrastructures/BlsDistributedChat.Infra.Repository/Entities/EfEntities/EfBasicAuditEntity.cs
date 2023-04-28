namespace BlsDistributedChat.Infra.Repository.Entities.EfEntities
{
    public abstract class EfBasicAuditEntity<TKey> : EfEntity<TKey>, IAuditInfo
    {
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
