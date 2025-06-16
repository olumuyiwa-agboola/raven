using Dapper;
using System.Text;
using System.Data;
using System.Reflection;
using Raven.Core.Models.DTOs;
using MySql.Data.MySqlClient;
using Raven.Core.Models.Shared;
using Raven.Core.Libraries.Enums;
using Raven.Core.Models.Entities;
using Raven.Core.Libraries.Constants;
using Raven.Core.Abstractions.Factories;
using Raven.Core.Abstractions.Repositories;

namespace Raven.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Implementation of the <see cref="IUsersRepository"/> interface for managing users in a database.
    /// </summary>
    public class UsersMySQLRepository(IDbConnectionFactory dbConnectionFactory) : IUsersRepository
    {
        public async Task<(bool, Error?)> SaveUser(User user)
        {
            DynamicParameters parameters = new();
            parameters.Add("UserId", user.UserId);
            parameters.Add("LastName", user.LastName);
            parameters.Add("FirstName", user.FirstName);
            parameters.Add("CreatedAt", user.CreatedAt);
            parameters.Add("PhoneNumber", user.PhoneNumber);
            parameters.Add("EmailAddress", user.EmailAddress);
            parameters.Add("LastUpdatedAt", user.LastUpdatedAt);

            string command = $"""
                INSERT INTO {DataStores.Users.Name}
                (
                    {DataStores.Users.Attributes.UserId}, 
                    {DataStores.Users.Attributes.FirstName}, 
                    {DataStores.Users.Attributes.LastName}, 
                    {DataStores.Users.Attributes.EmailAddress}, 
                    {DataStores.Users.Attributes.PhoneNumber}, 
                    {DataStores.Users.Attributes.CreatedAt}, 
                    {DataStores.Users.Attributes.LastUpdatedAt}
                )
                VALUES
                (
                    @UserId, @FirstName, @LastName, @EmailAddress, @PhoneNumber, @CreatedAt, @LastUpdatedAt
                )
                """;
            IDbConnection ravenMySqlConnection = dbConnectionFactory.GetRavenMySqlDbConnection();

            using (ravenMySqlConnection)
            {
                try
                {
                    int numberOfRowsAffected = await ravenMySqlConnection.ExecuteAsync(command, parameters);

                    return numberOfRowsAffected switch
                    {
                        1 => (true, null),
                        _ => (false, Error.NewError(ErrorType.DatabaseError, $"An error occured while trying to insert the user data into the database: {numberOfRowsAffected} rows affected.")),
                    };
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062)
                    {
                        if (ex.Message.Contains(user.EmailAddress))
                            return (false, Error.NewError(ErrorType.RecordAlreadyExists, "Email address already exists."));

                        if (ex.Message.Contains(user.PhoneNumber))
                            return (false, Error.NewError(ErrorType.RecordAlreadyExists, "Phone number already exists."));
                    }

                    return (false, Error.NewError(ErrorType.Exception, $"An exception occured while trying to save the user data to the database: {ex.Message}."));
                }
                catch (Exception ex)
                {
                    return (false, Error.NewError(ErrorType.Exception, $"An exception occured while trying to save the user data to the database: {ex.Message}."));
                }
            }
        }

        public async Task<(bool, Error?)> DeleteUser(string userId)
        {
            DynamicParameters parameters = new();
            parameters.Add("UserId", userId);

            string command = $"""
                DELETE FROM {DataStores.Users.Name} WHERE {DataStores.Users.Attributes.UserId} = @UserId;
                """;

            IDbConnection ravenMySqlConnection = dbConnectionFactory.GetRavenMySqlDbConnection();

            using (ravenMySqlConnection)
            {
                try
                {
                    int numberOfRowsAffected = await ravenMySqlConnection.ExecuteAsync(command, parameters);
                    return numberOfRowsAffected switch
                    {
                        1 => (true, null),
                        0 => (false, Error.NewError(ErrorType.UserNotFound, "The user was not found in the database.")),
                        _ => (false, Error.NewError(ErrorType.DatabaseError, $"An error occured while trying to delete the user from the database: {numberOfRowsAffected} rows affected.")),
                    };
                }
                catch (Exception ex)
                {
                    return (false, Error.NewError(ErrorType.Exception, $"An exception occured while trying to delete the user from the database: {ex.Message}."));
                }
            }
        }

        public async Task<(bool, User?, Error?)> GetUser(string searchParameter, SearchType searchType)
        {
            DynamicParameters parameters = new();
            StringBuilder commandBuilder = new();
            commandBuilder.Append($"""
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
                """);

            switch (searchType)
            {
                case SearchType.UserId:
                    parameters.Add("UserId", searchParameter);
                    commandBuilder.Append($"WHERE {DataStores.Users.Attributes.UserId} = @UserId;");
                    break;

                case SearchType.EmailAddress:
                    parameters.Add("EmailAddress", searchParameter);
                    commandBuilder.Append($"WHERE {DataStores.Users.Attributes.EmailAddress} = @EmailAddress;");
                    break;

                case SearchType.PhoneNumber:
                    parameters.Add("PhoneNumber", searchParameter);
                    commandBuilder.Append($"WHERE {DataStores.Users.Attributes.PhoneNumber} = @PhoneNumber;");
                    break;

                default:
                    return (false, null, Error.NewError(ErrorType.InvalidSearchType, "The search type provided is invalid."));
            }

            string command = commandBuilder.ToString();
            IDbConnection ravenMySqlConnection = dbConnectionFactory.GetRavenMySqlDbConnection();

            using (ravenMySqlConnection)
            {
                try
                {
                    User? user = await ravenMySqlConnection.QuerySingleOrDefaultAsync<User>(command, parameters);
                    if (user is null)
                        return (false, null, Error.NewError(ErrorType.UserNotFound, "The user was not found in the database."));
                    return (true, user, null);
                }
                catch (Exception ex)
                {
                    return (false, null, Error.NewError(ErrorType.Exception, $"An exception occured while trying to retrieve the user data from the database: {ex.Message}."));
                }
            }
        }

        public async Task<(bool, Error?)> UpdateUser(string userId, UserUpdateDto updates)
        {
            DynamicParameters parameters = new();
            parameters.Add("UserId", userId);
            parameters.Add("LastUpdatedAt", DateTime.Now);

            StringBuilder commandBuilder = new($"UPDATE {DataStores.Users.Name} SET ");

            foreach (PropertyInfo propertyInfo in typeof(UserUpdateDto).GetProperties())
            {
                var property = (Property)propertyInfo.GetValue(updates)!;

                if (string.IsNullOrWhiteSpace(property?.Value)) continue;

                parameters.Add(property.ColumnName, property.Value);
                commandBuilder.Append($"{property.ColumnName} = @{property.ColumnName}, ");
            }

            commandBuilder.Append($"{DataStores.Users.Attributes.LastUpdatedAt} = @LastUpdatedAt");
            commandBuilder.Append($" WHERE {DataStores.Users.Attributes.UserId} = @UserId;");

            string command = commandBuilder.ToString();
            IDbConnection ravenMySqlConnection = dbConnectionFactory.GetRavenMySqlDbConnection();

            using (ravenMySqlConnection)
            {
                try
                {
                    int numberOfRowsAffected = await ravenMySqlConnection.ExecuteAsync(command, parameters);
                    return numberOfRowsAffected switch
                    {
                        1 => (true, null),
                        0 => (false, Error.NewError(ErrorType.UserNotFound, "The user was not found in the database.")),
                        _ => (false, Error.NewError(ErrorType.DatabaseError, $"An error occured while trying to update the user data in the database: {numberOfRowsAffected} rows affected.")),
                    };
                }
                catch (Exception ex)
                {
                    return (false, Error.NewError(ErrorType.Exception, $"An exception occured while trying to update the user data in the database: {ex.Message}."));
                }
            }
        }
    }
}
