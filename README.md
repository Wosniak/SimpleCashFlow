
# Simple Cash Flow

## Objective

This project has the aim to demonstrate an CQRS implementation on Asp.Net Core 7 Solution.

Based on Clean Architecture with some Domain Driven Design concepts, the project shows how to implement an in-memory bus with MediatR, dealing with commands, queries and domain events handling.

For domain validation, a FluentValidator implementation was used.

This solution was intended to run under a Docker environment, and the dev docker was supported by a docker-compose with all resources needed to run the application.

The service authentication/authorization is not the scope of the project, so it´s provided by an external Identity Provider that supports OpenId/OAuth implementation, such as Auth0 (recommended).

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

Also, I need an report with the daily summary of the consolidated balance.

## Business Requirement
- An endpoint service for movements handling;
- An endpoint for the consolidate report.

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
For balance summary, a Materialized View was created. A materialized view is basically a pre-calculated data, that can be stored for a faster reading operation.

In the application, on every command execution a Domain Event is published and handled for the materialized view recalculation.

### SOLID
All code is based on SOLID principles: 
- Single Responsibility;
- Open/Closed;
- Liskov Substitution;
- Interface Segregation;
- Dependency Injection.

For more information about SOLID Principles: [SOLID Principles on Wikipedia](https://en.wikipedia.org/wiki/SOLID)

### Observability

The web API implements a health check end point, based on Microsoft.Extensions.Diagnostics.HealthChecks

The Uri to access the health check is: \<Minimal Api URI\>/hc

### Loggin

For logging purposes, the solution uses Serilog Package. 

The choice is based on its many possible ways to store log, going from files to multiple types of data bases.

That flexibility allows to plan the growth of the application and the many possibilities on querying the logs to generate statistics.

## How to run the application:

To run the project, is required an external Identity provider that implements OpenID/OAuth Reference.

To configure your IdP, change the Authority and Audience parameter on IdP Section of SimpleCashFlow.WebApi appsettings.json, inserting the correct values for your environment

On development, Auth0 was a choice as IdP.

If you chose to run with docker compose, all data files needed to run Postgress are bundled, there´s no need to run migrations.

If you plan to run locally, it will be needed to run migrations on your Postgres instance first.

### Visual Studio 2022 Community

#### With Docker Compose

This application was developed using the Visual Studio Community IDE, and that's the easiest way to run/debug it.

Just open the solution in Visual Studio and start the Docker-Compose project.

#### Without Docker

To run without a docker, some configuration/requirements are needed:
- A instance of Postgres running on 15.3 version;
- Set the correct connection string to the database on Minimal Api Project app settings. (SimpleCashFlow.WebApi);
- Set Minimal Api Project (SimpleCashFlow.WebApi) as a startup project.

With those adjustments, just run the project.

### From Command Line

#### Run the docker-compose (no debugging)

    docker-compose -f docker-compose.yml up -d

### With Visual Studio Code

To run the project with Visual Studio Code, please refer to:
[Use Docker Compose to work with multiple containers (visualstudio.com)](https://code.visualstudio.com/docs/containers/docker-compose)
