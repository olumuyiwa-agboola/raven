﻿using Testcontainers.MySql;
using FluentMigrator.Runner;
using Raven.Infrastructure.Factories;
using Raven.Core.Models.Configuration;
using Raven.Core.Abstractions.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.IntegrationTests.Data.Migrations;

namespace Raven.Tests.UnitTests.RepositoryTests
{
    public class RavenMySQLDbFixture : IAsyncLifetime
    {
        public IServiceProvider? Services { get; private set; }

        private MySqlContainer Container { get; } = new MySqlBuilder()
            .WithName("raven_test")
            .WithImage("mysql:9.3")
            .WithUsername("raven_test")
            .WithPassword("raven_test123")
            .WithPortBinding(63184, 3306)
            .Build();

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
            var runner = Services!.GetRequiredService<IMigrationRunner>();
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
                    .ScanIn(typeof(OtpUsersTable).Assembly).For.Migrations());

            Services = services.BuildServiceProvider();
        }
    }
}
