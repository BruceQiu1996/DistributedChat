using BlsDistributedChat.Infra.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlsDistributedChat.Infra.Repository.EfCore.SqlServer
{
    public class SqlServerDbContext : BlsDistributedChatDbContext
    {
        public SqlServerDbContext(DbContextOptions options, IEntityInfo entityInfo) : base(options, entityInfo)
        {
        }
    }
}