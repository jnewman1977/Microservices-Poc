#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV ASPNETCORE_URLS=http://+:57596  
EXPOSE 57596

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Nitro.Channel.Api/Nitro.Channel.Api.csproj", "Nitro.Channel.Api/"]
COPY ["Nitro.Msvc.User.Messaging/Nitro.Msvc.User.Messaging.csproj", "Nitro.Msvc.User.Messaging/"]
COPY ["Nitro.Msvc.User.Messaging.Abstraction/Nitro.Msvc.User.Messaging.Abstraction.csproj", "Nitro.Msvc.User.Messaging.Abstraction/"]
COPY ["Nitro.Msvc.User.Entities/Nitro.Msvc.User.Entities.csproj", "Nitro.Msvc.User.Entities/"]
COPY ["Nitro.Msvc.Tenant.Messaging/Nitro.Msvc.Tenant.Messaging.csproj", "Nitro.Msvc.Tenant.Messaging/"]
COPY ["Nitro.Msvc.Tenant.Messaging.Abstraction/Nitro.Msvc.Tenant.Messaging.Abstraction.csproj", "Nitro.Msvc.Tenant.Messaging.Abstraction/"]
COPY ["Nitro.Msvc.Tenant.Entities/Nitro.Msvc.Tenant.Entities.csproj", "Nitro.Msvc.Tenant.Entities/"]
COPY ["Nitro.Core.Configuraiton.Abstraction/Nitro.Core.Configuration.Abstraction.csproj", "Nitro.Core.Configuraiton.Abstraction/"]
COPY ["Nitro.Core.Configuration/Nitro.Core.Configuration.csproj", "Nitro.Core.Configuration/"]
COPY ["Nitro.GraphQL/Nitro.GraphQL.csproj", "Nitro.GraphQL/"]
RUN dotnet restore "Nitro.Channel.Api/Nitro.Channel.Api.csproj"
COPY . .
WORKDIR "/src/Nitro.Channel.Api"
RUN dotnet build "Nitro.Channel.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nitro.Channel.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nitro.Channel.Api.dll"]