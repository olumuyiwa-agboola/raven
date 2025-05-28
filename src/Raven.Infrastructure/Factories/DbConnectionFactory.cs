using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Options;
using Raven.Core.Models.Configuration;
using Raven.Core.Abstractions.Factories;

namespace Raven.Infrastructure.Factories
{
    public class DbConnectionFactory(IOptions<ConnectionStrings> connectionStringsOptions) : IDbConnectionFactory
    {
        private readonly ConnectionStrings connectionStrings = connectionStringsOptions.Value;

        public IDbConnection GetRavenMySqlDbConnection()
        {
            return new MySqlConnection(connectionStrings.RavenMySQLConnectionString);
        }
    }
}
