using Dapper;
using System.Data;
using Raven.Core.Enums;
using MySql.Data.MySqlClient;
using Raven.Core.Abstractions.Factories;
using Raven.Core.Abstractions.Repositories;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Shared;

namespace Raven.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Implementation of the <see cref="IUsersRepository"/> interface for managing OTP (One-Time Password) users in a database.
    /// </summary>
    public class UsersMySQLRepository(IDbConnectionFactory dbConnectionFactory) : IUsersRepository
    {
        private readonly IDbConnection ravenMySqlConnection = dbConnectionFactory.GetRavenMySqlDbConnection();

        public async Task<(bool, Error?)> SaveOtpUser(OtpUser otpUser)
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

            using (ravenMySqlConnection)
            {
                try
                {
                    int numberOfRowsAffected = await ravenMySqlConnection.ExecuteAsync(command, parameters);

                    return numberOfRowsAffected switch
                    {
                        1 => (true, null),
                        _ => (false, Error.NewError(ErrorType.DatabaseInsertError, $"An error occured while trying to insert the OTP user data into the database: {numberOfRowsAffected} rows affected.")),
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

        public Task<(bool, Error?)> DeleteOtpUser(OtpUser otpUser)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, OtpUser?, Error?)> GetOtpUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, Error?)> UpdateOtpUser(OtpUser otpUser)
        {
            throw new NotImplementedException();
        }
    }
}
