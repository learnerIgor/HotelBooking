using Accommo.Application.Handlers.External.Rooms.GetRoomById;
using Grpc.Core;
using GrpcGreeter;
using MediatR;

namespace Accommo.Api.gRPC
{
    /// <summary>
    /// Get room from Accommo by gRPC
    /// </summary>
    public class GRPCRoomsService : RoomService.RoomServiceBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Get room from Accommo by gRPC
        /// </summary>
        public GRPCRoomsService(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get room
        /// </summary>
        /// <returns></returns>
        public override async Task<RoomReply> GetRoom(GetRoomRequest request, ServerCallContext context)
        {
            var query = new GetRoomByIdQuery
            {
                Id = request.RoomId
            };
            var dto = await _mediator.Send(query, context.CancellationToken);
            var replay = new RoomReply
            {
                RoomId = dto.RoomId.ToString(),
                Floor = dto.Floor,
                Number = dto.Number,
                IsActive = dto.IsActive,
                Image = dto.Image,
                RoomType = new RoomType
                {
                    RoomTypeId = dto.RoomType.RoomTypeId.ToString(),
                    Name = dto.RoomType.Name,
                    BaseCost = (int)dto.RoomType.BaseCost,
                    IsActive = dto.RoomType.IsActive
                },
                Hotel = new Hotel
                {
                    HotelId = dto.Hotel.HotelId.ToString(),
                    Name = dto.Hotel.Name,
                    Description = dto.Hotel.Description,
                    Rating = dto.Hotel.Rating,
                    IsActive = dto.Hotel.IsActive,
                    /*Iban = dto.Hotel.IBAN,*/
                    Image = dto.Hotel.Image,
                    Address = new Address
                    {
                        AddressId = dto.Hotel.Address.AddressId.ToString(),
                        Street = dto.Hotel.Address.Street,
                        HouseNumber = dto.Hotel.Address.HouseNumber,
                        Latitude = (double)dto.Hotel.Address.Latitude,
                        Longitude = (double)dto.Hotel.Address.Longitude,
                        IsActive = dto.Hotel.Address.IsActive,
                        City = new City
                        {
                            CityId = dto.Hotel.Address.City.CityId.ToString(),
                            Name = dto.Hotel.Address.City.Name,
                            IsActive = dto.Hotel.Address.City.IsActive,
                            Country = new Country
                            {
                                CountryId = dto.Hotel.Address.City.Country.CountryId.ToString(),
                                Name = dto.Hotel.Address.City.Country.Name,
                                IsActive = dto.Hotel.Address.City.Country.IsActive
                            }
                        }
                    }
                }
            };

            return replay;
        }
    }
}
