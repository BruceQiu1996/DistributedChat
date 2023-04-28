using BlsDistributedChat.Infra.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BlsDistributedChat.Infra.Repository.EfCore.Transaction
{
    public abstract class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        protected TDbContext? BlsDistributedChatDbContext { get; init; }
        protected IDbContextTransaction? DbTransaction { get; set; }
        public bool IsStartingUow => BlsDistributedChatDbContext?.Database.CurrentTransaction is not null;

        public UnitOfWork(TDbContext context)
        {
            BlsDistributedChatDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected abstract IDbContextTransaction GetDbContextTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, bool distributed = false);

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, bool distributed = false)
        {
            if (BlsDistributedChatDbContext.Database.CurrentTransaction is not null)
                throw new ArgumentException($"UnitOfWork Error,{BlsDistributedChatDbContext.Database.CurrentTransaction}");
            else
                DbTransaction = GetDbContextTransaction(isolationLevel, distributed);
        }

        public void Commit()
        {
            if (DbTransaction is null)
                throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");
            else
                DbTransaction.Commit();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (DbTransaction is null)
                throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");
            else
                await DbTransaction.CommitAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbTransaction is not null)
                {
                    DbTransaction.Dispose();
                    DbTransaction = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            if (DbTransaction is null)
                throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");
            else
                DbTransaction.Rollback();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (DbTransaction is null)
                throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");
            else
                await DbTransaction.RollbackAsync(cancellationToken);
        }
    }
}