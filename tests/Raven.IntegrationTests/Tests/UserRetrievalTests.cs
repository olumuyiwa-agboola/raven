using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Responses;
using Raven.IntegrationTests.DTOs;
using Raven.Core.Libraries.Constants;
using Raven.IntegrationTests.Fixtures;
using Raven.IntegrationTests.Utilities;
using Raven.IntegrationTests.Data.TestData;

namespace Raven.IntegrationTests.Tests
{
    [Collection("API Test Collection")]
    public partial class UserRetrievalTests
    {
        private readonly HttpClient HttpClient;
        private readonly ApiTestFixture _fixture;

        public UserRetrievalTests(ApiTestFixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

            if (_fixture.HttpClient == null)
                throw new InvalidOperationException("HttpClient is not initialized in the fixture.");
            HttpClient = _fixture.HttpClient;

            if (_fixture.TestUsers == null)
                throw new InvalidOperationException("Seed data is not initialized in the fixture.");
        }

        [Fact]
        public async Task Getting_a_user_that_exists_returns_a_200OK_rsponse_with_the_users_details()
        {
            #region Arrange
            User existingUser = _fixture.TestUsers![3];
            var queryParameters = new List<KeyValuePair<string, string>>
            {
                new("UserId", existingUser.UserId),
                new("PhoneNumber", existingUser.PhoneNumber),
                new("EmailAddress", existingUser.EmailAddress),
            };
            #endregion

            foreach (var queryParameter in queryParameters)
            {
                #region Act
                var response = await HttpClient.GetAsync(Routes.GetUser + $"?SearchType={queryParameter.Key}&Value={queryParameter.Value}");
                #endregion

                #region Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                response.Content.Should().NotBeNull();
                var responseContent = await response.Content.ReadAsStringAsync();
                var getUserResponse = JsonConvert.DeserializeObject<GetUserResponse>(responseContent);

                getUserResponse.Should().NotBeNull();
                getUserResponse.UserId.Should().Be(existingUser.UserId);
                getUserResponse.LastName.Should().Be(existingUser.LastName);
                getUserResponse.FirstName.Should().Be(existingUser.FirstName);
                getUserResponse.PhoneNumber.Should().Be(existingUser.PhoneNumber);
                getUserResponse.EmailAddress.Should().Be(existingUser.EmailAddress);
                getUserResponse.CreatedAt.Should().Contain(existingUser.CreatedAt.ToString("dddd dd MMMM, yyyy, hh:mm:ss tt"));
                getUserResponse.LastUpdatedAt.Should().Contain(existingUser.LastUpdatedAt.ToString("dddd dd MMMM, yyyy, hh:mm:ss tt"));
                #endregion
            }
        }

        [Fact]
        public async Task Getting_a_user_that_does_not_exist_returns_a_422UnprocessableEntity_rsponse_with_the_problem_details()
        {
            #region Arrange
            User nonExistingUser = Users.Generate(1)[0];
            var queryParameters = new List<KeyValuePair<string, string>>
            {
                new("UserId", nonExistingUser.UserId),
                new("PhoneNumber", nonExistingUser.PhoneNumber),
                new("EmailAddress", nonExistingUser.EmailAddress),
            };
            #endregion

            foreach (var queryParameter in queryParameters)
            {
                #region Act
                var response = await HttpClient.GetAsync(Routes.GetUser + $"?SearchType={queryParameter.Key}&Value={queryParameter.Value}");
                #endregion

                #region Assert
                response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
                var responseContent = await response.Content.ReadAsStringAsync();

                response.Content.Should().NotBeNull();
                var problemDetails = JsonConvert.DeserializeObject<ProblemDetailsDto>(responseContent);

                problemDetails.Should().NotBeNull();
                problemDetails.Title.Should().Be(ErrorMessages.UserNotFound);
                #endregion
            }
        }

        [Fact]
        public async Task Getting_a_user_without_a_search_type_and_or_value_returns_a_400BadRequest_response_with_the_problem_details()
        {
            #region Arrange
            User nonExistingUser = Users.Generate(1)[0];
            var queryParameters = new List<KeyValuePair<string, string>>
            {
                new("", ""),
                new("UserId", ""),
                new("PhoneNumber", ""),
                new("EmailAddress", ""),
                new("", nonExistingUser.UserId),
                new("", nonExistingUser.PhoneNumber),
                new("", nonExistingUser.EmailAddress),
            };
            #endregion

            foreach (var queryParameter in queryParameters)
            {
                #region Act
                var response = await HttpClient.GetAsync(Routes.GetUser + $"?SearchType={queryParameter.Key}&Value={queryParameter.Value}");
                #endregion

                #region Assert
                response.Content.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseBody = JsonConvert.DeserializeObject<ProblemDetailsDto>(responseContent);

                responseBody.Should().NotBeNull();
                responseBody.Errors.Should().NotBeNull();
                responseBody.Status.Should().Be((int)HttpStatusCode.BadRequest);
                responseBody.Title.Should().Be(ErrorMessages.FailedValidations);
                responseBody.Instance?.ToLower().Should().Be($"/{Routes.GetUser}");
                #endregion
            }
        }
    }
}
