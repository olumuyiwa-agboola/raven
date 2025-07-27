using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net.Http.Json;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Responses;
using Raven.IntegrationTests.DTOs;
using Raven.Core.Libraries.Constants;
using Raven.IntegrationTests.Fixtures;
using Raven.IntegrationTests.Utilities;
using Raven.IntegrationTests.Data.TestData;

namespace Raven.IntegrationTests.Tests;

[Collection("API Test Collection")]
public class UserUpdateTests
{
    private readonly HttpClient HttpClient;
    private readonly ApiTestFixture _fixture;

    public UserUpdateTests(ApiTestFixture fixture)
    {
        _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        if (_fixture.HttpClient == null)
            throw new InvalidOperationException("HttpClient is not initialized in the fixture.");
        HttpClient = _fixture.HttpClient;

        if (_fixture.TestUsers == null)
            throw new InvalidOperationException("Seed data is not initialized in the fixture.");
    }

    [Fact]
    public async Task Updating_an_existing_user_with_valid_data_returns_a_204NoContent_response()
    {
        #region Arrange
        User newUserDetails = Users.Generate(1)[0];
        User existingUser = _fixture.TestUsers![0];
        var requestBody = new
        {
            LastName = newUserDetails.LastName,
            FirstName = newUserDetails.FirstName,
            PhoneNumber = newUserDetails.PhoneNumber,
            EmailAddress = newUserDetails.EmailAddress
        };
        #endregion

        #region Act
        var response = await HttpClient.PutAsJsonAsync(Routes.UpdateUser.Replace("{userId}", existingUser.UserId), requestBody);
        var userDetailsResponse = await HttpClient.GetAsync(Routes.GetUser + $"?SearchType=UserId&Value={existingUser.UserId}");
        #endregion

        #region Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        response.Content.Should().NotBeNull();
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().BeNullOrWhiteSpace();
        var userDetailsResponseContent = await userDetailsResponse.Content.ReadAsStringAsync();
        var userDetails = JsonConvert.DeserializeObject<GetUserResponse>(userDetailsResponseContent);
        userDetails.Should().NotBeNull();
        userDetails.LastName.Should().Be(requestBody.LastName);
        userDetails.FirstName.Should().Be(requestBody.FirstName);
        userDetails.PhoneNumber.Should().Be(requestBody.PhoneNumber);
        userDetails.EmailAddress.Should().Be(requestBody.EmailAddress);
        #endregion
    }

    [Fact]
    public async Task Updating_a_non_existing_user_with_valid_data_returns_a_422UnprocessableEntity_response_with_the_problem_details()
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
        var response = await HttpClient.PutAsJsonAsync(Routes.UpdateUser.Replace("{userId}", nonExistingUser.UserId), requestBody);
        #endregion

        #region Assert
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        response.Content.Should().NotBeNull();
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().NotBeNullOrWhiteSpace();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetailsDto>(responseContent);

        problemDetails.Should().NotBeNull();
        problemDetails.Title.Should().Be(ErrorMessages.UserNotFound);
        #endregion
    }

    [Fact]
    public async Task Updating_an_exsiting_user_with_invalid_data_returns_a_400BadRequest_response_with_the_problem_details()
    {
        #region Arrange
        User existingUser = _fixture.TestUsers![0];
        var requestBody = new
        {
            LastName = "#$%^yt09uyy-0i8906)+ {}",
            FirstName = "sxhcgfykghm485 ^&^^*()jd",
            PhoneNumber = "2434754654056*2+@ty7789",
            EmailAddress = "rfhn5y9hb235)=_O_)%@gmail.com"
        };
        #endregion

        #region Act
        var response = await HttpClient.PutAsJsonAsync(Routes.UpdateUser.Replace("{userId}", existingUser.UserId), requestBody);
        #endregion

        #region Assert
        response.Content.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseBody = JsonConvert.DeserializeObject<ProblemDetailsDto>(responseContent);

        responseBody.Should().NotBeNull();
        responseBody.Errors.Should().NotBeNull();
        responseBody.Errors.Count.Should().Be(4);
        responseBody.Status.Should().Be((int)HttpStatusCode.BadRequest);
        responseBody.Title.Should().Be(ErrorMessages.FailedValidations);
        #endregion
    }
}
