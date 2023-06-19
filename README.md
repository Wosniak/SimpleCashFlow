
# Simple Cash Flow

## Objective

This project has the aim to demonstrate an CQRS implementation on Asp.Net Core 7 Solution.

Based on Clean Architecture with some Domain Driven Design concepts, the project shows how to implement an in-memory bus with MediatR, dealing with commands, queries and domain events handling.

For domain validation, a FluentValidator implementation was used.

This solution was intended to run under a Docker environment, and the dev docker was supported by a docker-compose with all resources needed to run the application.

The service authentication/authorization is not the scope of the project, so it´s provided by an external Identity Provider that supports OpenId/OAuth implementation, such as Auth0 (recomended).

## Technologies:
-	Asp.Net Core 7 Minimum API;
-	MediatR;
-	.Net Core Entity Framework;
-	.FluentValidator.

## Tests:
- xUnit;
- Moq.

## Database
- Postgres SQL.

## Use Case

As a business owner, I need to control my small business cash flow, with all daily movements (debit and credits) been registered.

Also I need an report with the daily summary of the consolidated balance.

## Business Requirement
- An end-point service for movements handling;
- An end-point for the consolidate report;

## Architecture

### C4 Model

![SimpleCashFlow_C4Model-Context drawio](https://github.com/Wosniak/SimpleCashFlow/assets/16797201/452c5abf-55b5-457d-afab-36b685157e72)

![SimpleCashFlow_C4Model-Container drawio (1)](https://github.com/Wosniak/SimpleCashFlow/assets/16797201/2f74089a-2c6f-4d70-9243-feb3a18aed68)

![SimpleCashFlow_C4Model-Component drawio](https://github.com/Wosniak/SimpleCashFlow/assets/16797201/2e61c181-d81f-4d0a-b26e-a6a56b06b3c4)

## Patterns

### CQRS
All operations on the application were based on CQRS (Command and Queries Responsibility Segregation), a pattern base on Single Responsibility Principle, where the commands, that handles data modifications, must be apart from queries (read operations).

In some scenarios, even a separate database is utilized for each purpose. For this project, for simplicity’s sake, all data resides on same place.

### Materialized View
For balance summary, a Materialized View was created. A materialized view is basically a pre-calculated data, tah cam be stored for a faster reading operation.

In the aplication, on every command execution an Domain Event is published and handled for the materilized view recalculation.

### SOLID
All code is based on SOLID principles: 
- Single Responsibility;
- Open/Closed;
- Liskov Substitution;
- Interface Segregation;
- Dependency Injection.

For more information about SOLID Principles: [SOLID Principles on Wikipedia](https://en.wikipedia.org/wiki/SOLID)

## How to run the application:

To run the project, is required an external Identity provider that implements OpenID/OAuth Reference.

To configure your IdP, change the URI paramenter on IdP Section of SimpleCashFlow.WebApi appsettings.json

On development fase, Auth0 was a chaice as IdP.

To configure

### Visual Studio 2022 Community

#### With Docker Compose

This application was developed using the Visual Studio Community IDE, and that's the easiest way to run/debug it.

Just open the solution in Visual Studio and start the Docker-Compose project.

#### Without Docker

To run without a docker, some configuration/requirements are needed:
- A instance of Postgres runing on 15.3 version;
- Set the correct connection string to the database on Minimal Api Project appsetings. (SimpleCashFlow.WebApi);
- Set Minimal Api Project appsetings. (SimpleCashFlow.WebApi) as a startup project.

With those ajustmens, just run the project.

### From Command Line

#### Run the docker-compose (no debugging)

    docker-compose -f docker-compose.yml -f docker-compose.yml up -d

### With Visual Studio Code

To run the project with Visual Studio Code, plese refer to:
[Use Docker Compose to work with multiple containers (visualstudio.com)](https://code.visualstudio.com/docs/containers/docker-compose)
