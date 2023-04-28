using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlsDistributedChat.Shared.Repository.EfEntities.Config
{
    public class EventTrackerConfig : AbstractEntityTypeConfiguration<EventTracker,long>
    {
        public override void Configure(EntityTypeBuilder<EventTracker> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.TrackerName).HasMaxLength(50);
            builder.HasIndex(x => new { x.EventId, x.TrackerName }, "uk_eventid_trackername").IsUnique();
        }
    }
}
