using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Responses;
using Raven.IntegrationTests.Fixtures;
using Raven.IntegrationTests.Utilities;

namespace Raven.IntegrationTests.Tests
{
    [Collection("API Test Collection")]
    public class UserRetrievalTests
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
                var responseContent = await response.Content.ReadAsStringAsync();

                response.Content.Should().NotBeNull();
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
    }
}
