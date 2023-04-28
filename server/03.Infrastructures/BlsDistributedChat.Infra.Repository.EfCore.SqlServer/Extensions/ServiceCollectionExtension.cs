using BlsDistributedChat.Infra.Repository.EfCore.Repositories;
using BlsDistributedChat.Infra.Repository.EfCore.SqlServer.Options;
using BlsDistributedChat.Infra.Repository.EfCore.SqlServer.Transation;
using BlsDistributedChat.Infra.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace BlsDistributedChat.Infra.Repository.EfCore.SqlServer.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraEfCoreSQLServer(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            services.TryAddScoped<IUnitOfWork, SqlServerUnitOfWork<SqlServerDbContext>>();
            services.TryAddScoped(typeof(IEfBasicRepository<,>), typeof(EfBasicRepository<,>));
            services.AddDbContext<DbContext, SqlServerDbContext>(optionsBuilder);

            return services;
        }

        public static IServiceCollection AddAdncInfraEfCoreSQLServer(this IServiceCollection services, IConfigurationSection sqlServerSection)
        {
            var sqlServerOption = sqlServerSection.GetValue<SqlServerOption>("SqlServerOption");

            return AddAdncInfraEfCoreSQLServer(services, options =>
            {
                options.UseLowerCaseNamingConvention();
                options.UseSqlServer(sqlServerOption.ConnectionString, optionsBuilder =>
                {
                    optionsBuilder.MinBatchSize(4).MigrationsAssembly(sqlServerOption.MigrationAssembly).UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env is not null && string.Equals(env, "development", StringComparison.OrdinalIgnoreCase))
                {
                    options.LogTo(Console.WriteLine, LogLevel.Information)
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors();
                }
            });
        }
    }
}
