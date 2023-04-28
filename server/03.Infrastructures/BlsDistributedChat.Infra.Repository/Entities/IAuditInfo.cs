namespace BlsDistributedChat.Infra.Repository.Entities
{
    public interface IAuditInfo
    {
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
