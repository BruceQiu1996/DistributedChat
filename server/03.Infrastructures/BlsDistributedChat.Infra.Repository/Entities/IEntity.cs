namespace BlsDistributedChat.Infra.Repository.Entities
{
    /// <summary>
    /// 实体基本接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
