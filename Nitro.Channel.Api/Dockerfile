#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Nitro.Channel.Api/Nitro.Channel.Api.csproj", "Nitro.Channel.Api/"]
COPY ["Nitro.Msvc.Tenant.Messaging/Nitro.Msvc.Tenant.Messaging.csproj", "Nitro.Msvc.Tenant.Messaging/"]
COPY ["Nitro.Msvc.Tenant.Messaging.Interfaces/Nitro.Msvc.Tenant.Messaging.Interfaces.csproj", "Nitro.Msvc.Tenant.Messaging.Interfaces/"]
COPY ["Nitro.Msvc.Tenant.Entities/Nitro.Msvc.Tenant.Entities.csproj", "Nitro.Msvc.Tenant.Entities/"]
COPY ["Nitro.Msvc.Tenant.Entities.Interfaces/Nitro.Msvc.Tenant.Entities.Interfaces.csproj", "Nitro.Msvc.Tenant.Entities.Interfaces/"]
COPY ["Nitro.Msvc.Tenant.Configuration.Interfaces/Nitro.Msvc.Tenant.Configuration.Interfaces.csproj", "Nitro.Msvc.Tenant.Configuration.Interfaces/"]
COPY ["Nitro.Msvc.Tenant.Configuration/Nitro.Msvc.Tenant.Configuration.csproj", "Nitro.Msvc.Tenant.Configuration/"]
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
