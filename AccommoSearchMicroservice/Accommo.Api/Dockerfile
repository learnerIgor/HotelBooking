#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AccommoSearchMicroservice/Accommo.Api/Accommo.Api.csproj", "AccommoSearchMicroservice/Accommo.Api/"]
COPY ["AccommoSearchMicroservice/Infrastructure/Accommo.DistributedCache/Accommo.DistributedCache.csproj", "AccommoSearchMicroservice/Infrastructure/Accommo.DistributedCache/"]
COPY ["AccommoSearchMicroservice/Accommo.Application/Accommo.Application.csproj", "AccommoSearchMicroservice/Accommo.Application/"]
COPY ["AccommoSearchMicroservice/Accommo.Domain/Accommo.Domain.csproj", "AccommoSearchMicroservice/Accommo.Domain/"]
COPY ["AccommoSearchMicroservice/Infrastructure/Accommo.Persistence/Accommo.Persistence.csproj", "AccommoSearchMicroservice/Infrastructure/Accommo.Persistence/"]
RUN dotnet restore "./AccommoSearchMicroservice/Accommo.Api/Accommo.Api.csproj"
COPY . .
WORKDIR "/src/AccommoSearchMicroservice/Accommo.Api"
RUN dotnet build "./Accommo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Accommo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accommo.Api.dll"]