using Dapper;
using Testcontainers.MySql;
using FluentMigrator.Runner;
using Raven.Core.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Raven.Core.Libraries.Constants;
using Microsoft.AspNetCore.Mvc.Testing;
using Raven.Core.Abstractions.Factories;
using Microsoft.Extensions.Configuration;
using Raven.IntegrationTests.Data.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Raven.IntegrationTests.Fixtures
{
    public class ApiTestFixture : IAsyncLifetime
    {
        private MySqlContainer? _MySqlContainer = new MySqlBuilder()
                                            .WithName("raven_test")
                                            .WithImage("mysql:9.3")
                                            .WithUsername("raven_test")
                                            .WithPassword("raven_test123")
                                            .WithPortBinding(63184, 3306)
                                            .Build();

        public List<User>? TestUsers { get; set; }
        private IServiceScope? ServiceScope { get; set; }
        public HttpClient? HttpClient { get; private set; }
        private string? MySqlDbConnectionString { get; set; }
        public WebApplicationFactory<Program>? Factory { get; private set; }

        public async Task InitializeAsync()
        {
            if (_MySqlContainer == null)
                throw new Exception("_MySqlContainer is null.");

            await _MySqlContainer.StartAsync();

            MySqlDbConnectionString = _MySqlContainer.GetConnectionString();

            Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddInMemoryCollection(new Dictionary<string, string>
                        {
                            ["ConnectionStrings:RavenMySQLConnectionString"] = MySqlDbConnectionString
                        }!);
                    });

                    builder.UseEnvironment("Testing");
                });

            HttpClient = Factory.CreateClient();
            ServiceScope = Factory.Services.CreateScope();

            await RunMySqlDbMigration();
            TestUsers = await GetSeedData();
        }

        private async Task RunMySqlDbMigration()
        {
            var services = new ServiceCollection()
                  .AddFluentMigratorCore()
                  .ConfigureRunner(rb => rb
                      .AddMySql8()
                      .WithGlobalConnectionString(MySqlDbConnectionString)
                      .ScanIn(typeof(UsersTable).Assembly).For.Migrations())
                  .AddLogging(lb => lb.AddFluentMigratorConsole())
                  .BuildServiceProvider(false);

            await Task.Run(() =>
            {
                using var scope = services.CreateScope();
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            });
        }

        private async Task<List<User>> GetSeedData()
        {
            string command = $"""
                SELECT 
                    {DataStores.Users.Attributes.UserId} AS UserId,
                    {DataStores.Users.Attributes.LastName} AS LastName,
                    {DataStores.Users.Attributes.FirstName} AS FirstName,
                    {DataStores.Users.Attributes.CreatedAt} AS CreatedAt,
                    {DataStores.Users.Attributes.PhoneNumber} AS PhoneNumber,
                    {DataStores.Users.Attributes.EmailAddress} AS EmailAddress,
                    {DataStores.Users.Attributes.LastUpdatedAt} AS LastUpdatedAt
                FROM 
                    {DataStores.Users.Name} 
                """;
            var dbConnectionFactory = GetService<IDbConnectionFactory>();
            using var connection = dbConnectionFactory.GetRavenMySqlDbConnection();
            var result = await connection.QueryAsync<User>(command);
            return result.ToList();
        }

        public async Task DisposeAsync()
        {
            ServiceScope?.Dispose();
            HttpClient?.Dispose();
            Factory?.Dispose();

            if (_MySqlContainer != null)
            {
                await _MySqlContainer.StopAsync();
                await _MySqlContainer.DisposeAsync();
            }
        }

        public T GetService<T>() where T : class
        {
            if (ServiceScope == null)
                throw new InvalidOperationException("ServiceScope is null.");

            return ServiceScope.ServiceProvider.GetRequiredService<T>();
        }
    }

    [CollectionDefinition("API Test Collection")]
    public class ApiTestCollection : ICollectionFixture<ApiTestFixture>
    {

    }
}
