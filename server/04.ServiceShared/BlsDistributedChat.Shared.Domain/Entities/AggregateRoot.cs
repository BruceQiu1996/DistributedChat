using BlsDistributedChat.Infra.Repository.Entities;
using BlsDistributedChat.Infra.Repository.Entities.EfEntities;

namespace BlsDistributedChat.Shared.Domain.Entities
{
    public abstract class AggregateRoot<TKey> : DomainEntity<TKey>,IConcurrency,IEfEntity<TKey>
    {
        public byte[] RowVersion { get; set; }
        //public Lazy<IEventPublisher> EventPublisher => new(() =>
        //{
        //    var httpContext = InfraHelper.Accessor.GetCurrentHttpContext();
        //    if (httpContext is not null)
        //        return httpContext.RequestServices.GetRequiredService<IEventPublisher>();
        //    if (ServiceLocator.Provider is not null)
        //        return ServiceLocator.Provider.GetRequiredService<IEventPublisher>();
        //    throw new NotImplementedException(nameof(IEventPublisher));
        //});
    }
}
