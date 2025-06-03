using Testcontainers.MySql;
using FluentMigrator.Runner;
using Raven.Infrastructure.Factories;
using Raven.Core.Models.Configuration;
using Raven.Infrastructure.Migrations;
using Raven.Core.Abstractions.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Raven.Tests.UnitTests.RepositoryTests
{
    public class RavenMySQLDbFixture : IAsyncLifetime
    {
        public IServiceProvider Services { get; private set; }

        private MySqlContainer Container { get; } = new MySqlBuilder().Build();

        public async Task InitializeAsync()
        {
            await Container.StartAsync();
            ConfigureServices();
            MigrateDatabase();
        }

        public async Task DisposeAsync()
        {
            await Container.StopAsync();
            await Container.DisposeAsync();
        }

        private void MigrateDatabase()
        {
            var runner = Services.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
                {
                    new ("ConnectionStrings:RavenMySQLConnectionString", Container.GetConnectionString())
                }).Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddMySql().WithGlobalConnectionString(Container.GetConnectionString())
                    .ScanIn(typeof(CreateOtpUsersTable).Assembly).For.Migrations());

            Services = services.BuildServiceProvider();
        }
    }
}
