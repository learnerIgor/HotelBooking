#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UsersMicroservice/Users.Api/Users.Api.csproj", "UsersMicroservice/Users.Api/"]
COPY ["UsersMicroservice/Users.Application/Users.Application.csproj", "UsersMicroservice/Users.Application/"]
COPY ["UsersMicroservice/Users.Domain/Users.Domain.csproj", "UsersMicroservice/Users.Domain/"]
COPY ["UsersMicroservice/Users.Persistence/Users.Persistence.csproj", "UsersMicroservice/Users.Persistence/"]
RUN dotnet restore "./UsersMicroservice/Users.Api/Users.Api.csproj"
COPY . .
WORKDIR "/src/UsersMicroservice/Users.Api"
RUN dotnet build "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Api.dll"]