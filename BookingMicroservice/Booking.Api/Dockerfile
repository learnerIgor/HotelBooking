#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookingMicroservice/Booking.Api/Booking.Api.csproj", "BookingMicroservice/Booking.Api/"]
COPY ["BookingMicroservice/Booking.Application/Booking.Application.csproj", "BookingMicroservice/Booking.Application/"]
COPY ["BookingMicroservice/Booking.Domain/Booking.Domain.csproj", "BookingMicroservice/Booking.Domain/"]
COPY ["BookingMicroservice/Infrastructure/Booking.DistributedCache/Booking.DistributedCache.csproj", "BookingMicroservice/Infrastructure/Booking.DistributedCache/"]
COPY ["BookingMicroservice/Infrastructure/Booking.Exchanger/Booking.Exchanger.csproj", "BookingMicroservice/Infrastructure/Booking.Exchanger/"]
COPY ["BookingMicroservice/Infrastructure/Booking.ExternalProviders/Booking.ExternalProviders.csproj", "BookingMicroservice/Infrastructure/Booking.ExternalProviders/"]
COPY ["BookingMicroservice/Infrastructure/Booking.Persistence/Booking.Persistence.csproj", "BookingMicroservice/Infrastructure/Booking.Persistence/"]
RUN dotnet restore "./BookingMicroservice/Booking.Api/Booking.Api.csproj"
COPY . .
WORKDIR "/src/BookingMicroservice/Booking.Api"
RUN dotnet build "./Booking.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Booking.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Booking.Api.dll"]