using FluentValidation;
using Raven.Core.Libraries.Constants;
using Raven.Core.Abstractions.Factories;

namespace Raven.Core.Models.Configuration
{
    /// <summary>
    /// Represents a collection of connection strings used for database connectivity.
    /// </summary>
    /// <remarks>This class provides properties to store connection strings for various databases. It is
    /// typically used to centralize and manage connection string configurations within an application.</remarks>
    public class ConnectionStrings
    {
        /// <summary>
        /// The connection string for the Raven MySQL database.
        /// </summary>
        public string? RavenMySQLConnectionString { get; set; }
    }

    /// <summary>
    /// Provides validation rules for the <see cref="ConnectionStrings"/> model, to be evaluated on start.
    /// </summary>
    /// <remarks>Thus validator ensures that the:
    /// <list type="bullet">
    ///     <item><description><c>RavenMySQLConnectionString</c> must not be empty.</description></item>
    /// </list> 
    /// </remarks>
    public class ConnectionStringsValidator : AbstractValidator<ConnectionStrings>
    {
        public ConnectionStringsValidator()
        {
            RuleFor(model => model.RavenMySQLConnectionString)
                .MustBeAbleToEstablishAMySqlDatabaseConnection();
        }
    }
}
