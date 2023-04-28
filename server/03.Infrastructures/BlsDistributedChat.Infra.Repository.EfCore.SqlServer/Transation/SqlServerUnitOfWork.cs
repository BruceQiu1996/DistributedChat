using BlsDistributedChat.Infra.Repository.EfCore.Transaction;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BlsDistributedChat.Infra.Repository.EfCore.SqlServer.Transation
{
    public class SqlServerUnitOfWork<TDbContext> : UnitOfWork<TDbContext> where TDbContext : SqlServerDbContext
    {
        private ICapPublisher? _publisher;

        public SqlServerUnitOfWork(TDbContext context, ICapPublisher? capPublisher) : base(context)
        {
            _publisher = capPublisher;
        }

        protected override IDbContextTransaction GetDbContextTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, bool distributed = false)
        {
            if (distributed)
            {
                if (_publisher is null)
                {
                    throw new ArgumentException("CapPublisher is null");
                }
                else
                {
                    return BlsDistributedChatDbContext.Database.BeginTransaction(_publisher, false);
                }
            }
            else 
            {
                return BlsDistributedChatDbContext.Database.BeginTransaction(isolationLevel);
            }
        }
    }
}
