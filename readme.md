# Microservices POC

## Solution Structure

_Below is the description of the solution/project structure and what each item represents._

### Core (Solution Folder)

- **Nitro.Core.Configuration (Project)**
    - Database configuration impl for DI
    - Messaging configuration impl for DI
- **Intro.Core.Configuration.Abstraction (Project)**
    - Database configuration interface for DI
    - Messaging configuraiton interface for DI

### GatewayAPI (Solution Folder)

- **Nitro.Channel.Api (Project)**
    - Web API / Gateway API
      - Base URL - `http://localhost:57596`
      - REST
        - Route - `/api`
      - GraphQL
        - Route - `/graphql` 
          - One endpoint for query and mutation
        - Altair query client tool (UI used for Dev/QA schema and data exploration)
          - Route - `/ui/altair`
          - `examples.agc` - Example query collection in solution root

- **Nitro.GraphQL (Project)**
    - GraphQL Server Types
      - Schema
      - Root Query and SubQueries
      - Root Mutation and SubMutations
      - GraphTypes

### Microservices (Solution Folder)

**_Includes basic AMQP messaging/comms and Mongo persistence._**

> RabbitMQ Management Plugin
• http://localhost:15672/
• user: `guest`
• pwd: `guest`
> 
#### Tenant (Solution Folder) - _Sample Microservice_

- **Nitro.Msvc.Tenant (Project)** - _Service Application_
  - `AddTenantConsumer`
    - Consumes `AddTenantRequest`.
    - Adds `Tenant` via the `ITenantRepository`.
    - Publishes `AddTenantResponse` with the result.
  - `GetAllTenantsConsumer`
    - Consumes `GetAllTenantsRequest`.
    - Queries list of all tenants from the `ITenantRepository`.
    - Publishes `GetAllTenantsResponse` with the results. 
- **Nitro.Msvc.Tenant.Access (Project)**
  - Repository and Tenant Entity only used in data access.
- **Nitro.Msvc.Tenant.Entities (Project)**
  - Business Entity only used everywhere else on the server side.
- **Nitro.Msvc.Tenant.Messaging (Project)**
  - Wraps / Hides usage of MassTransit in a service client
- **Nitro.Msvc.Tenant.Messaging.Abstraction (Project)**
  - Messaging Client Interface and Message Contracts.

#### User (Solution Folder) - _Sample Microservice_

- **Nitro.Msvc.User (Project)** - _Service Application_
  - `AddUserConsumer`
    - Consumes `AddUserRequest`.
    - Adds `User` via the `IUserRepository`.
    - Publishes `AddUserResponse` with result.
  - `GetAllUsersConsumer`
    - Consumes `GetAllUsersRequest`.
    - Queries list of all users from the `IUserRepository`.
    - Publishes `GetAllUsersResponse` with results.
  - `GetUserByUserIdConsumer`
    - Consumes `GetUserByUserIdRequest`.
    - Queries `User` by UserId from the `IUserRepository`.
    - Publishes `GetUserByUserIdResponse` with result.
- **Nitro.Msvc.User.Access (Project)**
  - Repository and User Entity only used in data access.
- **Nitro.Msvc.User.Entities (Project)**
  - Business Entity only used everywhere else on the server side.
- **Nitro.Msvc.User.Messaging (Project)**
  - Wraps / Hides usage of MassTransit in a service client.
- **Nitro.Msvc.User.Messaging.Abstraction (Project)**
  - Messaging Client Interface and Message Contracts.

---

## Running Locally

### Visual Studio

- Run Docker Compose Target.

### JetBrains Rider

- Run Docker Compose Run Configuration.

## Resource Links

- **GraphQL**
    - Documentation
      - https://graphql.org/
      - https://graphql-dotnet.github.io/docs/getting-started/introduction/
    - Altair UI - https://altair.sirmuel.design/
- **MassTransit**
    - http://masstransit-project.com/
- **RabbitMQ**
  - https://www.rabbitmq.com/
  - Plugin List - https://www.rabbitmq.com/plugins.html
    - Most Helpful Plugins
      - Management - https://www.rabbitmq.com/management.html
        - **Included with the rabbit docker image used in this example**
      - Shovel - https://www.rabbitmq.com/shovel.html