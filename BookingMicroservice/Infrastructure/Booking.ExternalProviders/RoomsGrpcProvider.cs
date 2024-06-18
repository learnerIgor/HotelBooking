using Booking.Domain;
using Booking.Application.Abstractions.Persistence.Repositories.Write;
using Booking.ExternalProviders.Exceptions;
using Microsoft.Extensions.Configuration;
using Grpc.Net.Client;
using GrpcGreeter;
using Booking.Application.Abstractions.ExternalProviders;

namespace Booking.ExternalProviders
{
    public class RoomsGrpcProvider : IRoomProvider
    {
        private readonly IBaseWriteRepository<Room> _rooms;
        private readonly IBaseWriteRepository<Domain.RoomType> _roomType;
        private readonly IBaseWriteRepository<Domain.Country> _country;
        private readonly IBaseWriteRepository<Domain.City> _city;
        private readonly IBaseWriteRepository<Domain.Address> _address;
        private readonly IBaseWriteRepository<Domain.Hotel> _hotel;
        private readonly IConfiguration _configuration;

        public RoomsGrpcProvider(
            IConfiguration configuration,
            IBaseWriteRepository<Room> rooms,
            IBaseWriteRepository<Domain.RoomType> roomType,
            IBaseWriteRepository<Domain.Country> country,
            IBaseWriteRepository<Domain.City> city,
            IBaseWriteRepository<Domain.Address> address,
            IBaseWriteRepository<Domain.Hotel> hotel)
        {
            _rooms = rooms;
            _roomType = roomType;
            _configuration = configuration;
            _country = country;
            _city = city;
            _address = address;
            _hotel = hotel;
        }

        public async Task<Room> GetRoomAsync(string roomId, CancellationToken cancellationToken)
        {
            var result = await _rooms.AsAsyncRead().SingleOrDefaultAsync(u => u.RoomId == Guid.Parse(roomId) & u.IsActive, cancellationToken);
            if (result != null)
            {
                return result;
            }

            var requestUrl = _configuration["AccommoGrpcServiceApiUrl"];
            var channel = GrpcChannel.ForAddress(requestUrl!);
            var client = new RoomService.RoomServiceClient(channel);
            try
            {
                var resultRoom = client.GetRoom(new GetRoomRequest
                {
                    RoomId = roomId,
                }, cancellationToken: cancellationToken);

                var idRoomType = Guid.Parse(resultRoom.RoomType.RoomTypeId);
                var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(t => t.RoomTypeId == idRoomType, cancellationToken);
                if (roomType == null)
                {
                    var newRoomType = new Domain.RoomType(idRoomType, resultRoom.RoomType.Name, (decimal)resultRoom.RoomType.BaseCost, resultRoom.RoomType.IsActive);
                    await _roomType.AddAsync(newRoomType, cancellationToken);
                }

                var idCountry = Guid.Parse(resultRoom.Hotel.Address.City.Country.CountryId);
                var country = await _country.AsAsyncRead().SingleOrDefaultAsync(t => t.CountryId == idCountry, cancellationToken);
                if (country == null)
                {
                    var newCountry = new Domain.Country(idCountry, resultRoom.Hotel.Address.City.Country.Name, resultRoom.Hotel.Address.City.Country.IsActive);
                    await _country.AddAsync(newCountry, cancellationToken);
                }

                var idCity = Guid.Parse(resultRoom.Hotel.Address.City.CityId);
                var city = await _city.AsAsyncRead().SingleOrDefaultAsync(t => t.CityId == idCity, cancellationToken);
                if (city == null)
                {
                    var newCity = new Domain.City(idCity, resultRoom.Hotel.Address.City.Name, idCountry, resultRoom.Hotel.Address.City.IsActive);
                    await _city.AddAsync(newCity, cancellationToken);
                }

                var idAddress = Guid.Parse(resultRoom.Hotel.Address.AddressId);
                var address = await _address.AsAsyncRead().SingleOrDefaultAsync(t => t.AddressId == idAddress, cancellationToken);
                if (address == null)
                {
                    var newAddress = new Domain.Address(idAddress, resultRoom.Hotel.Address.Street, resultRoom.Hotel.Address.HouseNumber, (decimal)resultRoom.Hotel.Address.Latitude, (decimal)resultRoom.Hotel.Address.Longitude, resultRoom.Hotel.Address.IsActive, idCity);
                    await _address.AddAsync(newAddress, cancellationToken);
                }

                var idHotel = Guid.Parse(resultRoom.Hotel.HotelId);
                var hotel = await _hotel.AsAsyncRead().SingleOrDefaultAsync(t => t.HotelId == idHotel, cancellationToken);
                if (hotel == null)
                {
                    var newHotel = new Domain.Hotel(idHotel, resultRoom.Hotel.Name, idAddress, resultRoom.Hotel.Description, resultRoom.Hotel.Rating, resultRoom.Hotel.IsActive, resultRoom.Hotel.Image, "IT60X0542811101000000123456");
                    await _hotel.AddAsync(newHotel, cancellationToken);
                }

                var newRoom = new Domain.Room(Guid.Parse(roomId), resultRoom.Floor, resultRoom.Number, idRoomType, resultRoom.IsActive, idHotel, resultRoom.Image);
                return await _rooms.AddAsync(newRoom, cancellationToken);
            }
            catch (Exception ex)
            {
                var serviceName = "UserService";
                throw new ExternalServiceNotAvailable(serviceName, requestUrl!);
            }
        }
    }
}
