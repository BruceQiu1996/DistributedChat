namespace BlsDistributedChat.Infra.Repository.Entities
{
    public interface IConcurrency
    {
        public byte[] RowVersion { get; set; }
    }
}
