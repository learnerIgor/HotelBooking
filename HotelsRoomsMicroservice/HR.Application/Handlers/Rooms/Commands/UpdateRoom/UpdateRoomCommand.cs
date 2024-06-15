using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommand : IImage, IRequest<GetRoomDto>
    {
        public string Id { get; init; }
        public int Floor { get; init; }
        public int Number { get; init; }
        public string RoomTypeId { get; init; }
        public string HotelId { get; init; }
        public string Image { get; init; }

        public Amenities Amenities { get; init; }

        public UpdateRoomCommand(string id, UpdateRoomPayload payload)
        {
            Id = id;
            Floor = payload.Floor;
            Number = payload.Number;
            RoomTypeId = payload.RoomTypeId;
            HotelId = payload.HotelId;
            Image = payload.Image;

            Amenities = new Amenities
            {
                Bed = payload.Amenities.Bed,
                Chair = payload.Amenities.Chair,
                Table = payload.Amenities.Table,
                BedsideTable = payload.Amenities.BedsideTable,
                Wardrobe = payload.Amenities.Wardrobe,
                Balcony = payload.Amenities.Balcony,
                WiFi = payload.Amenities.WiFi,
                TV = payload.Amenities.TV,
                AirConditioner = payload.Amenities.AirConditioner,
                Phone = payload.Amenities.Phone,
                Bar = payload.Amenities.Bar,
                Safe = payload.Amenities.Safe,
                Food = payload.Amenities.Food,
                NonSmoking = payload.Amenities.NonSmoking,
                Smoking = payload.Amenities.Smoking,
                Pets = payload.Amenities.Pets,
                PrivateBathroom = payload.Amenities.PrivateBathroom,
                SeaView = payload.Amenities.SeaView,
                HydromassageBath = payload.Amenities.HydromassageBath,
                Terrace = payload.Amenities.Terrace,
                Sofa = payload.Amenities.Sofa,
                Dishwasher = payload.Amenities.Dishwasher,
                Bath = payload.Amenities.Bath,
                Soundproofing = payload.Amenities.Soundproofing,
                Refrigerator = payload.Amenities.Refrigerator,
                IroningAccessories = payload.Amenities.IroningAccessories,
                Shower = payload.Amenities.Shower,
                WashingMachine = payload.Amenities.WashingMachine,
                Toilet = payload.Amenities.Toilet,
                Pool = payload.Amenities.Pool
            };
        }
    }
}
