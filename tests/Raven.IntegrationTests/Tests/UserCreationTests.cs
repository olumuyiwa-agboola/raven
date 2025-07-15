using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net.Http.Json;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Responses;
using Raven.IntegrationTests.DTOs;
using Raven.IntegrationTests.Fixtures;
using Raven.IntegrationTests.Utilities;
using Raven.IntegrationTests.Data.TestData;

namespace Raven.IntegrationTests.Tests
{
    [Collection("API Test Collection")]
    public class UserCreationTests
    {
        private readonly HttpClient HttpClient;
        private readonly ApiTestFixture _fixture;

        public UserCreationTests(ApiTestFixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

            if (_fixture.HttpClient == null)
                throw new InvalidOperationException("HttpClient is not initialized in the fixture.");
            HttpClient = _fixture.HttpClient;

            if (_fixture.TestUsers == null)
                throw new InvalidOperationException("Seed data is not initialized in the fixture.");
        }

        [Fact]
        public async Task Creating_a_user_with_valid_data_returns_201Created_response_with_user_details()
        {
            #region Arrange
            User nonExistingUser = Users.Generate(1)[0];
            var requestBody = new
            {
                LastName = nonExistingUser.LastName,
                FirstName = nonExistingUser.FirstName,
                PhoneNumber = nonExistingUser.PhoneNumber,
                EmailAddress = nonExistingUser.EmailAddress
            };
            #endregion

            #region Act
            var response = await HttpClient.PostAsJsonAsync(Routes.CreateUser, requestBody);
            #endregion


            #region Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            response.Content.Should().NotBeNull();
            var responseContent = await response.Content.ReadAsStringAsync();
            var createUserResponse = JsonConvert.DeserializeObject<CreateUserResponse>(responseContent);

            createUserResponse.Should().NotBeNull();
            createUserResponse.UserId.Should().NotBeNullOrWhiteSpace();
            #endregion
        }

        [Fact]
        public async Task Creating_a_user_with_an_existing_email_address_or_phone_number_returns_422UnprocessableEntity_response_with_the_problem_details()
        {
            #region Arrange
            User existingUser = _fixture.TestUsers![2];
            User nonExistingUser = Users.Generate(1)[0];
            var payloads = new List<object>
            {
                new
                {
                    LastName = nonExistingUser.LastName,
                    FirstName = nonExistingUser.FirstName,
                    EmailAddress = existingUser.EmailAddress,
                    PhoneNumber = nonExistingUser.PhoneNumber,
                },
                new
                {
                    LastName = nonExistingUser.LastName,
                    FirstName = nonExistingUser.FirstName,
                    PhoneNumber = existingUser.PhoneNumber,
                    EmailAddress = nonExistingUser.EmailAddress,
                },
                new
                {
                    LastName = nonExistingUser.LastName,
                    FirstName = nonExistingUser.FirstName,
                    PhoneNumber = existingUser.PhoneNumber,
                    EmailAddress = existingUser.EmailAddress,
                }
            };
            #endregion

            foreach (var payload in payloads)
            {
                #region Act
                var response = await HttpClient.PostAsJsonAsync(Routes.CreateUser, payload);
                #endregion

                #region Assert
                response.StatusCode.Should().Be(HttpStatusCode.Conflict);

                response.Content.Should().NotBeNull();
                var responseContent = await response.Content.ReadAsStringAsync();
                var problemDetails = JsonConvert.DeserializeObject<ProblemDetailsDto>(responseContent);
                problemDetails.Should().NotBeNull();
                #endregion
            }
        }
    }
}
