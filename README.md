# Raven

[![License](https://img.shields.io/badge/License-GNU_GPL_v3-yellow.svg)](https://www.gnu.org/licenses/gpl-3.0.en.html)
[![Language](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Frameworks](https://img.shields.io/badge/Frameworks-.NET_9.0,_ASP.NET_Core_9.0_-green.svg)](https://dotnet.microsoft.com/download/dotnet-core)
[![gRPC](https://img.shields.io/badge/gRPC-v1.x-lightgrey.svg)](https://grpc.io/)
[![Maintenance](https://img.shields.io/badge/Maintained-Yes-brightgreen.svg)](#maintenance)

## Overview

**Raven** is a robust and scalable backend service designed for generating, sending, and validating one-time passwords (OTPs)
exposed via a REST API and a gRPC service. 
Implemented as a backend development practice project, it demonstrates adherence to the hexagonal architecture, clean and secure
coding principles, implementation of relevant design patterns, and thorough API documentation for both REST and gRPC interfaces.

This project serves as a portfolio piece to showcase my understanding and application of various backend development concepts and 
best practices.

## Key Features

* **OTP Generation & Delivery:** Secure and configurable OTP generation and delivery via email and SMS (placeholder for actual implementation).
* **OTP Validation:** Secure and reliable OTP validation with configurable expiration and retry count.
* **REST API:** Well-documented RESTful API for easy integration with web and mobile applications.
* **gRPC Service:** High-performance gRPC service for internal microservice communication.
* **Hexagonal Architecture:** Clear separation of concerns with distinct layers for presentation, domain, and infrastructure.
* **Design Patterns:** Implementation of relevant design patterns to promote maintainability and scalability.
* **Clean Code:** Focus on writing readable, maintainable, and well-tested code.
* **Configuration:** Flexible configuration options for various service parameters.
* **Logging:** Comprehensive and structured logging for monitoring and debugging.
* **Error Handling:** Robust error handling and consistent error response formats.

## Tech Stack
Raven was implemented using the following technologies and libraries:
* **Language:** [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* **Backend Frameworks:** [.NET Core & ASP.NET Core](https://dotnet.microsoft.com/download/dotnet-core)
* **REST API:** [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0)
* **gRPC:** [ASP.NET Core gRPC (Grpc.AspNetCore NuGet package)](https://github.com/grpc/grpc-dotnet)
* **API Documentation (REST):** [Microsoft.OpenAPI](https://github.com/microsoft/OpenAPI.NET), [Scalar](https://github.com/scalar/scalar/blob/main/integrations/aspnetcore/README.md).
* **Dependency Injection:** [Microsoft.Extensions.DependencyInjection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
* **Configuration:** [ASP.NET Core configuration system](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-9.0)
* **Input Validation:** [FluentValidation](https://github.com/FluentValidation/FluentValidation)
* **Logging:** [Serilog](https://serilog.net/)
* **Testing:** [xUnit](https://xunit.net/)
* **Data Access:** [Dapper](https://github.com/DapperLib/Dapper)
* **Database**: [MySQL](https://dev.mysql.com/) (not tightly coupled)

## License

This project is licensed under the [GNU General Public License v3](https://opensource.org/license/gpl-3-0) License.

-----

**Thank you for checking out Raven\!**