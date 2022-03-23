#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Nitro.Msvc.Tenant/Nitro.Msvc.Tenant.csproj", "Nitro.Msvc.Tenant/"]
COPY ["Nitro.Msvc.Tenant.Messaging.Interfaces/Nitro.Msvc.Tenant.Messaging.Interfaces.csproj", "Nitro.Msvc.Tenant.Messaging.Interfaces/"]
COPY ["Nitro.Msvc.Tenant.Entities/Nitro.Msvc.Tenant.Entities.csproj", "Nitro.Msvc.Tenant.Entities/"]
COPY ["Nitro.Msvc.Tenant.Entities.Interfaces/Nitro.Msvc.Tenant.Entities.Interfaces.csproj", "Nitro.Msvc.Tenant.Entities.Interfaces/"]
COPY ["Nitro.Msvc.Tenant.Access/Nitro.Msvc.Tenant.Access.csproj", "Nitro.Msvc.Tenant.Access/"]
COPY ["Nitro.Msvc.Tenant.Configuration.Interfaces/Nitro.Msvc.Tenant.Configuration.Interfaces.csproj", "Nitro.Msvc.Tenant.Configuration.Interfaces/"]
COPY ["Nitro.Msvc.Tenant.Access.Interfaces/Nitro.Msvc.Tenant.Access.Interfaces.csproj", "Nitro.Msvc.Tenant.Access.Interfaces/"]
COPY ["Nitro.Msvc.Tenant.Configuration/Nitro.Msvc.Tenant.Configuration.csproj", "Nitro.Msvc.Tenant.Configuration/"]
RUN dotnet restore "Nitro.Msvc.Tenant/Nitro.Msvc.Tenant.csproj"
COPY . .
WORKDIR "/src/Nitro.Msvc.Tenant"
RUN dotnet build "Nitro.Msvc.Tenant.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nitro.Msvc.Tenant.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nitro.Msvc.Tenant.dll"]
