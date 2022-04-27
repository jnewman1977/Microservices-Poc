#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Msvc.IAdapter/Msvc.IAdapter.csproj", "Msvc.IAdapter/"]
COPY ["Msvc.IAdapter.Abstraction/Msvc.IAdapter.Abstraction.csproj", "Msvc.IAdapter.Abstraction/"]
COPY ["Nitro.Core.Configuraiton.Abstraction/Nitro.Core.Configuration.Abstraction.csproj", "Nitro.Core.Configuraiton.Abstraction/"]
COPY ["Nitro.Core.Configuration/Nitro.Core.Configuration.csproj", "Nitro.Core.Configuration/"]
RUN dotnet restore "Msvc.IAdapter/Msvc.IAdapter.csproj"
COPY . .
WORKDIR "/src/Msvc.IAdapter"
RUN dotnet build "Msvc.IAdapter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Msvc.IAdapter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Msvc.IAdapter.dll"]
