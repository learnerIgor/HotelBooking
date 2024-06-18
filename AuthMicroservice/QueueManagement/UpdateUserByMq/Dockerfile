#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthMicroservice/QueueManagement/UpdateUserByMq/UpdateUserByMq.csproj", "AuthMicroservice/QueueManagement/UpdateUserByMq/"]
RUN dotnet restore "./AuthMicroservice/QueueManagement/UpdateUserByMq/UpdateUserByMq.csproj"
COPY . .
WORKDIR "/src/AuthMicroservice/QueueManagement/UpdateUserByMq"
RUN dotnet build "./UpdateUserByMq.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UpdateUserByMq.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UpdateUserByMq.dll"]