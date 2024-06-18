#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HotelsRoomsMicroservice/HR.Api/HR.Api.csproj", "HotelsRoomsMicroservice/HR.Api/"]
COPY ["HotelsRoomsMicroservice/HR.Application/HR.Application.csproj", "HotelsRoomsMicroservice/HR.Application/"]
COPY ["HotelsRoomsMicroservice/HR.Domain/HR.Domain.csproj", "HotelsRoomsMicroservice/HR.Domain/"]
COPY ["HotelsRoomsMicroservice/Infrastructure/HR.DistributedCache/HR.DistributedCache.csproj", "HotelsRoomsMicroservice/Infrastructure/HR.DistributedCache/"]
COPY ["HotelsRoomsMicroservice/Infrastructure/HR.ExternalProviders/HR.ExternalProviders.csproj", "HotelsRoomsMicroservice/Infrastructure/HR.ExternalProviders/"]
COPY ["HotelsRoomsMicroservice/Infrastructure/HR.Persistence/HR.Persistence.csproj", "HotelsRoomsMicroservice/Infrastructure/HR.Persistence/"]
RUN dotnet restore "./HotelsRoomsMicroservice/HR.Api/HR.Api.csproj"
COPY . .
WORKDIR "/src/HotelsRoomsMicroservice/HR.Api"
RUN dotnet build "./HR.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HR.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR.Api.dll"]