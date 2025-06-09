using Dapper;
using System.Text;
using System.Data;
using Raven.Core.Models.DTOs;
using MySql.Data.MySqlClient;
using Raven.Core.Models.Shared;
using Raven.Core.Libraries.Enums;
using Raven.Core.Models.Entities;
using Raven.Core.Abstractions.Factories;
using Raven.Core.Abstractions.Repositories;
using System.Reflection;

namespace Raven.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Implementation of the <see cref="IUsersRepository"/> interface for managing OTP (One-Time Password) users in a database.
    /// </summary>
    public class UsersMySQLRepository(IDbConnectionFactory dbConnectionFactory) : IUsersRepository
    {
        public async Task<(bool, Error?)> SaveUser(User otpUser)
        {
            DynamicParameters parameters = new();
            parameters.Add("UserId", otpUser.UserId);
            parameters.Add("LastName", otpUser.LastName);
            parameters.Add("FirstName", otpUser.FirstName);
            parameters.Add("CreatedAt", otpUser.CreatedAt);
            parameters.Add("PhoneNumber", otpUser.PhoneNumber);
            parameters.Add("EmailAddress", otpUser.EmailAddress);
            parameters.Add("LastUpdatedAt", otpUser.LastUpdatedAt);

            string command = """
                INSERT INTO otp_users
                (
                    user_id, first_name, last_name, email_address, phone_number, created_at, last_updated_at
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
                        _ => (false, Error.NewError(ErrorType.DatabaseError, $"An error occured while trying to insert the OTP user data into the database: {numberOfRowsAffected} rows affected.")),
                    };
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062)
                    {
                        if (ex.Message.Contains(otpUser.EmailAddress))
                            return (false, Error.NewError(ErrorType.RecordAlreadyExists, "Email address already exists."));

                        if (ex.Message.Contains(otpUser.PhoneNumber))
                            return (false, Error.NewError(ErrorType.RecordAlreadyExists, "Phone number already exists."));
                    }

                    return (false, Error.NewError(ErrorType.Exception, $"An exception occured while trying to save the OTP user data to the database: {ex.Message}."));
                }
                catch (Exception ex)
                {
                    return (false, Error.NewError(ErrorType.Exception, $"An exception occured while trying to save the OTP user data to the database: {ex.Message}."));
                }
            }
        }

        public async Task<(bool, Error?)> DeleteUser(string userId)
        {
            DynamicParameters parameters = new();
            parameters.Add("UserId", userId);

            string command = """
                DELETE FROM otp_users WHERE user_id = @UserId;
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
                        0 => (false, Error.NewError(ErrorType.NotFound, "The OTP user was not found in the database.")),
                        _ => (false, Error.NewError(ErrorType.DatabaseError, $"An error occured while trying to delete the OTP user data from the database: {numberOfRowsAffected} rows affected.")),
                    };
                }
                catch (Exception ex)
                {
                    return (false, Error.NewError(ErrorType.Exception, $"An exception occured while trying to delete the OTP user data from the database: {ex.Message}."));
                }
            }
        }

        public async Task<(bool, User?, Error?)> GetUser(string userId)
        {
            DynamicParameters parameters = new();
            parameters.Add("UserId", userId);

            string command = """
                SELECT 
                    user_id AS UserId, first_name AS FirstName, last_name AS LastName, email_address AS EmailAddress,
                    phone_number AS PhoneNumber, created_at AS CreatedAt, last_updated_at AS LastUpdatedAt 
                FROM 
                    otp_users WHERE user_id = @UserId;
                """;

            IDbConnection ravenMySqlConnection = dbConnectionFactory.GetRavenMySqlDbConnection();

            using (ravenMySqlConnection)
            {
                try
                {
                    User? otpUser = await ravenMySqlConnection.QuerySingleOrDefaultAsync<User>(command, parameters);
                    if (otpUser is null)
                        return (false, null, Error.NewError(ErrorType.NotFound, "The OTP user was not found in the database."));
                    return (true, otpUser, null);
                }
                catch (Exception ex)
                {
                    return (false, null, Error.NewError(ErrorType.Exception, $"An exception occured while trying to retrieve the OTP user data from the database: {ex.Message}."));
                }
            }
        }

        public async Task<(bool, Error?)> UpdateUser(string userId, UserUpdateDto updates)
        {
            DynamicParameters parameters = new();
            parameters.Add("UserId", userId);

            StringBuilder commandBuilder = new("UPDATE otp_users SET ");

            foreach (PropertyInfo propertyInfo in typeof(UserUpdateDto).GetProperties())
            {
                var property = (Property)propertyInfo.GetValue(updates)!;

                if (string.IsNullOrWhiteSpace(property?.Value)) continue;

                parameters.Add(property.ColumnName, property.Value);
                commandBuilder.Append($"{property.ColumnName} = @{property.ColumnName}, ");
            }

            commandBuilder.Length -= 2;
            commandBuilder.Append(" WHERE user_id = @UserId;");

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
                        0 => (false, Error.NewError(ErrorType.NotFound, "The OTP user was not found in the database.")),
                        _ => (false, Error.NewError(ErrorType.DatabaseError, $"An error occured while trying to update the OTP user data in the database: {numberOfRowsAffected} rows affected.")),
                    };
                }
                catch (Exception ex)
                {
                    return (false, Error.NewError(ErrorType.Exception, $"An exception occured while trying to update the OTP user data in the database: {ex.Message}."));
                }
            }
        }
    }
}
