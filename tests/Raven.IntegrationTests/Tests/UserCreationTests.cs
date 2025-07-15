using Raven.IntegrationTests.Fixtures;

namespace Raven.IntegrationTests.Tests
{
    [Collection("API Test Collection")]
    public class UserCreationTests
    {
        private readonly ApiTestFixture _fixture;

        public UserCreationTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }
    }
}
