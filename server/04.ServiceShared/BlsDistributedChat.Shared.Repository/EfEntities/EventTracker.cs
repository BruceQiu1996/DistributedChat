using BlsDistributedChat.Infra.Repository.Entities.EfEntities;

namespace BlsDistributedChat.Shared.Repository.EfEntities
{
    public class EventTracker : EfBasicAuditEntity<long>
    {
        public long EventId { get; set; }

        public string TrackerName { get; set; } = string.Empty;
    }
}
