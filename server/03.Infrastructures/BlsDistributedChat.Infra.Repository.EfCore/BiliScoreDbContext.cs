using BlsDistributedChat.Infra.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlsDistributedChat.Infra.Repository.EfCore
{
    public abstract class BlsDistributedChatDbContext : DbContext
    {
        private readonly IEntityInfo _entityInfo;

        protected BlsDistributedChatDbContext(DbContextOptions options, IEntityInfo entityInfo) : base(options)
        {
            _entityInfo = entityInfo;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedEntities = SetAuditFields();

            //没有自动开启事务的情况下,保证主从表插入，主从表更新开启事务。
            var isManualTransaction = false;
            if (Database.AutoTransactionBehavior== AutoTransactionBehavior.Never && Database.CurrentTransaction is null && changedEntities > 1)
            {
                isManualTransaction = true;
                Database.AutoTransactionBehavior = AutoTransactionBehavior.WhenNeeded;
            }

            var result = base.SaveChangesAsync(cancellationToken);

            //如果手工开启了自动事务，用完后关闭。
            if (isManualTransaction)
                Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => _entityInfo.OnModelCreating(modelBuilder);

        protected virtual int SetAuditFields()
        {
            var allBasicAuditEntities = ChangeTracker.Entries<IAuditInfo>().Where(x => x.State == EntityState.Added);
            foreach (var entry in allBasicAuditEntities)
            {
                entry.Entity.CreateTime = DateTime.Now;
                entry.Entity.LastUpdateTime = DateTime.Now;
            }

            var auditFullEntities = ChangeTracker.Entries<IAuditInfo>().Where(x => x.State == EntityState.Modified);
            foreach (var entry in allBasicAuditEntities)
            {
                entry.Entity.LastUpdateTime = DateTime.Now;
            }

            return ChangeTracker.Entries().Count();
        }
    }
}