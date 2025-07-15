# Raven

[![License](https://img.shields.io/badge/License-GNU_GPL_v3-yellow.svg)](https://www.gnu.org/licenses/gpl-3.0.en.html)
[![Language](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Frameworks](https://img.shields.io/badge/Frameworks-.NET_9.0,_ASP.NET_Core_9.0_-green.svg)](https://dotnet.microsoft.com/download/dotnet-core)

**Raven** is a unit & integration testing practice project that implements a REST API that exposes simple CRUD operations.
My goal is to try out the ideas from [Vladimir Khorikov's Unit Testing: Principles, Practices and Patterns](https://www.manning.com/books/unit-testing)
and [Gerard Meszaros's xUnit Test Patterns](https://www.oreilly.com/library/view/xunit-test-patterns/9780131495050/) while learning to use tools like:
- [xUnit](https://xunit.net/) as my testing framework, 
- [FluentAssertions](https://fluentassertions.com/) for writing readable assertions, 
- [Bogus](https://github.com/bchavez/Bogus) for generating fake data, 
- [FakeItEasy](https://github.com/FakeItEasy/FakeItEasy) for creatng fake objects and mocks,
- [FluentMigrator](https://fluentmigrator.github.io/) for running migrations on test databases,
- [TestContainers](https://testcontainers.com/) for creating throwaway test databases against which integration tests will be run,
- [Coverlet](https://github.com/coverlet-coverage/coverlet) for measuring code coverage,
- [ReportGenerator](https://marketplace.visualstudio.com/items?itemName=Palmmedia.reportgenerator) for visualizing code coverage results,
- [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing) for writing functional tests against the API.

## API Specification
| API | Description | Request body | Response body |
|-----|-------------|--------------|---------------|
|GET /api/users?searchType=\{searchType\}&value=\{value\} | Get a user whose information matches the given search value | None | Single user object |
|POST /api/users | Add a user to the database | Single user details object | Operation response object |
|PUT /api/users/{userid} | Update the details of the user whose ID is given | Single user details object | Operation response object |
|DELETE /api/users/{userid} | Delete the user whose ID from the database | None | Operation response object |