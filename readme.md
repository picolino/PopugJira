# PopugJira

_PopugJira - is a task tracker with additional accounting and analytics utilities_

_Created as education project, showing my capabilities in building asynchronous microservice architecture_

_Many thanks to authors of the [course about asynchronous architecture](https://education.borshev.com/architecture): @f213, @davydovanton_

## Microservices
### PopugJira.Identity
IdentityServer4 with ASP.NET Identity authorization service.

Contains registration and signing logic. Using OpenID through oAuth with Jwt.

### PopugJira.GoalTracker
ASP.NET 5 WebApi service.

Used for creating/updating goals for users.

### PopugJira.Accounting
ASP.NET 5 WebApi service.

Used for payment processing to users

### PopugJira.Analytics
ASP.NET 5 WebApi service.

User for provide analytics information

### PopugJira.Notifications
ASP.NET 5 WebApi service

User for notify users about some actions in the system

### PopugJira
Blazor WebAssembly application

Used as client application for user's browser. Communicates with all microservices

### Other
**PopugJira.Microservice** - library, contains base logic for every microservice. Just for simplify building new microservices.

**PopugJira.AutoDI** - library, contains logic for automatic dependency injection.

**PopugJira.Common** - library, contains other shared logic

**PopugJira.EventBus** - library, contains schema registry for events and base logic for event bus.

## Event bus
Each microservice uses shared RabbitMQ instance.

## Databases
Each microservice contains separated SQLite database and manipulates data only within that database.

All databases places into PopugJira (blazor) project folder during initialization of each service.

## Quick Start
1. Deploy shared RabbitMQ instance
1. Download and install .NET 5 SDK
1. Clone repository into any local folder
1. Change RabbitMQ connection strings into each microservice (`appsettings.json`)
1. Run all services (order of launch doesn't matter)
1. Navigate to `https://localhost:5001` or `http://localhost:5000`
1. Register few users (with different roles, note that not all roles has access to all parts of the system)
1. Login as any registered user
1. Enjoy