# Summary
This repo contains the source code for finding the location for an IP address

## Table of Contents
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)

## Technologies
The project uses the following technologies:

- .NET Core 7
- .NET API
- nUnit
- Moq
- FluentAssertions
- AutoFixture

## Getting Started
To get started with the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio or another IDE of your choice.
3. Restore the NuGet packages.
4. Set the startup projects to `LocationFromIP.Api`.
5. Run the project.

## Project Structure
The project is organized using the Clean Architecture principles. The solution is composed of the following projects:

- `LocationFromIP.Application`: Contains the application layer of the project, which contains the application logic and interfaces.
- `LocationFromIP.Infrastructure`: Contains the infrastructure layer of the project, which contains the implementation of the interfaces defined in the application layer for the third party integration.
- `LocationFromIP.Persistence`: Contains the infrastructure layer of the project, which contains the implementation of the interfaces defined in the application layer for the cache and persistence store.
- `LocationFromIP.Api`: Contains the API layer of the project
- `LocationFromIP.Application.UnitTest`: Contains the UnitTest for the LocationService
- `LocationFromIP.Api.IntegrationTest`: Contains the API Integration Test 