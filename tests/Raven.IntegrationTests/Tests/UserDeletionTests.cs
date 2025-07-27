using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using Raven.Core.Models.Entities;
using Raven.IntegrationTests.DTOs;
using Raven.Core.Libraries.Constants;
using Raven.IntegrationTests.Fixtures;
using Raven.IntegrationTests.Utilities;
using Raven.IntegrationTests.Data.TestData;

namespace Raven.IntegrationTests.Tests;

[Collection("API Test Collection")]
public class UserDeletionTests
{
    private readonly HttpClient HttpClient;
    private readonly ApiTestFixture _fixture;

    public UserDeletionTests(ApiTestFixture fixture)
    {
        _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        if (_fixture.HttpClient == null)
            throw new InvalidOperationException("HttpClient is not initialized in the fixture.");
        HttpClient = _fixture.HttpClient;

        if (_fixture.TestUsers == null)
            throw new InvalidOperationException("Seed data is not initialized in the fixture.");
    }

    [Fact]
    public async Task Deleting_a_user_that_exists_returns_a_204NoContent_response()
    {
        #region Arrange
        User existingUser = _fixture.TestUsers![1];
        #endregion

        #region Act
        var response = await HttpClient.DeleteAsync(Routes.DeleteUser.Replace("{userId}", existingUser.UserId));
        #endregion

        #region Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().BeNullOrWhiteSpace();
        #endregion
    }

    [Fact]
    public async Task Deleting_a_user_that_does_not_exist_returns_a_422UnprocessableEntity_response_with_the_problem_details()
    {
        #region Arrange
        User nonExistingUser = Users.Generate(1)[0];
        #endregion

        #region Act
        var response = await HttpClient.DeleteAsync(Routes.DeleteUser.Replace("{userId}", nonExistingUser.UserId));
        #endregion

        #region Assert
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        var responseContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetailsDto>(responseContent);
        problemDetails.Should().NotBeNull();
        problemDetails.Title.Should().Be(ErrorMessages.UserNotFound);
        #endregion
    }

    [Fact]
    public async Task Deleting_a_user_with_an_invalid_userId_returns_a_400BadRequest_response_with_the_problem_details()
    {
        #region Arrange
        string userId = "01JXTEK$FG4^GNTD-_+BN9M";
        #endregion

        #region Act
        var response = await HttpClient.DeleteAsync(Routes.DeleteUser.Replace("{userId}", userId));
        #endregion

        #region Assert
        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.MethodNotAllowed);
        var responseContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetailsDto>(responseContent);
        problemDetails.Should().NotBeNull();
        problemDetails.Title.Should().Be(ErrorMessages.FailedValidations);
        #endregion
    }
}
