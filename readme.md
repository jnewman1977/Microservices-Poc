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
        - REST
        - GraphQL
- **Nitro.GraphQL (Project)**
    - GraphQL Server Schema, Queries, Mutations and Types used for parsing and execution.

### Microservices (Solution Folder)
_Includes basic AMQP messaging/comms and Mongo persistence._
- **Tenant (Solution Folder) - _Sample Microservice_**
    - **Nitro.Msvc.Tenant (Project)** - _Service Application_
    - **Nitro.Msvc.Tenant.Access (Project)** - _Db Repoitory and Data Entities_
    - **Nitro.Msvc.Tenant.Entities (Project)** - _Domain Objects / Business Entities_
    - **Nitro.Msvc.Tenant.Messaging (Project)** - _Comms Client_
    - **Nitro.Msvc.Tenant.Messaging.Abstraction (Project)** - _Comms Client Interface and Message Contracts_
- **User (Solution Folder) - _Sample Microservice_**
    - **Nitro.Msvc.User (Project)** - _Service Application_
    - **Nitro.Msvc.User.Access (Project)** - _Db Repoitory and Data Entities_
    - **Nitro.Msvc.User.Entities (Project)** - _Domain Objects / Business Entities_
    - **Nitro.Msvc.User.Messaging (Project)** - _Comms Client_
    - **Nitro.Msvc.User.Messaging.Abstraction (Project)** - _Comms Client Interface and Message Contracts_

## Running Locally

### Visual Studio
- Run Docker Compose Target.

### JetBrains Rider
- Docker Compose doesn't seem to work here currently.

## Resource Links
- **GraphQL**
    - https://graphql.org/
    - https://graphql-dotnet.github.io/docs/getting-started/introduction/
- **MassTransit**
    - http://masstransit-project.com/
