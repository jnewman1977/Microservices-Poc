#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Nitro.Msvc.User/Nitro.Msvc.User.csproj", "Nitro.Msvc.User/"]
COPY ["Nitro.Msvc.User.Access/Nitro.Msvc.User.Access.csproj", "Nitro.Msvc.User.Access/"]
COPY ["Nitro.Core.Configuraiton.Abstraction/Nitro.Core.Configuration.Abstraction.csproj", "Nitro.Core.Configuraiton.Abstraction/"]
COPY ["Nitro.Msvc.User.Messaging.Abstraction/Nitro.Msvc.User.Messaging.Abstraction.csproj", "Nitro.Msvc.User.Messaging.Abstraction/"]
COPY ["Nitro.Msvc.User.Entities/Nitro.Msvc.User.Entities.csproj", "Nitro.Msvc.User.Entities/"]
COPY ["Nitro.Core.Configuration/Nitro.Core.Configuration.csproj", "Nitro.Core.Configuration/"]
RUN dotnet restore "Nitro.Msvc.User/Nitro.Msvc.User.csproj"
COPY . .
WORKDIR "/src/Nitro.Msvc.User"
RUN dotnet build "Nitro.Msvc.User.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nitro.Msvc.User.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nitro.Msvc.User.dll"]