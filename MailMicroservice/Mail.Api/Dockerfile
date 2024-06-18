#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MailMicroservice/Mail.Api/Mail.Api.csproj", "MailMicroservice/Mail.Api/"]
COPY ["UsersMicroservice/Users.Application/Users.Application.csproj", "UsersMicroservice/Users.Application/"]
COPY ["UsersMicroservice/Users.Domain/Users.Domain.csproj", "UsersMicroservice/Users.Domain/"]
COPY ["MailMicroservice/Infrastructure/Mail.Persistence/Mail.Persistence.csproj", "MailMicroservice/Infrastructure/Mail.Persistence/"]
COPY ["MailMicroservice/Mail.Application/Mail.Application.csproj", "MailMicroservice/Mail.Application/"]
COPY ["MailMicroservice/Mail.Domain/Mail.Domain.csproj", "MailMicroservice/Mail.Domain/"]
RUN dotnet restore "./MailMicroservice/Mail.Api/Mail.Api.csproj"
COPY . .
WORKDIR "/src/MailMicroservice/Mail.Api"
RUN dotnet build "./Mail.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mail.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mail.Api.dll"]