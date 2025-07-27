using System.Data;

namespace Raven.Core.Abstractions.Factories;

public interface IDbConnectionFactory
{
    IDbConnection GetRavenMySqlDbConnection();
}
