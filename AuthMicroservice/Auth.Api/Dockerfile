#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthMicroservice/Auth.Api/Auth.Api.csproj", "AuthMicroservice/Auth.Api/"]
COPY ["AuthMicroservice/Auth.Application/Auth.Application.csproj", "AuthMicroservice/Auth.Application/"]
COPY ["AuthMicroservice/Auth.Domain/Auth.Domain.csproj", "AuthMicroservice/Auth.Domain/"]
COPY ["AuthMicroservice/Infrastructure/Auth.ExternalProviders/Auth.ExternalProviders.csproj", "AuthMicroservice/Infrastructure/Auth.ExternalProviders/"]
COPY ["AuthMicroservice/Infrastructure/Auth.Persistence/Auth.Persistence.csproj", "AuthMicroservice/Infrastructure/Auth.Persistence/"]
RUN dotnet restore "./AuthMicroservice/Auth.Api/Auth.Api.csproj"
COPY . .
WORKDIR "/src/AuthMicroservice/Auth.Api"
RUN dotnet build "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]