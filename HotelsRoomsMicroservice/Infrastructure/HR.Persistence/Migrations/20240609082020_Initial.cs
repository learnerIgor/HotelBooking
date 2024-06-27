using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.AmenityId);
                });

            migrationBuilder.InsertData(
                   table: "Amenities",
                   columns: new[] { "AmenityId", "Name" },
                   values: new object[,]
                   {
                        { 1, "Bed" },
                        { 2, "Chair" },
                        { 3, "Table" },
                        { 4, "BedsideTable" },
                        { 5, "Wardrobe" },
                        { 6, "Balcony" },
                        { 7, "WiFi" },
                        { 8, "TV" },
                        { 9, "AirConditioner" },
                        { 10, "Phone" },
                        { 11, "Bar" },
                        { 12, "Safe" },
                        { 13, "Food" },
                        { 14, "NonSmoking" },
                        { 15, "Smoking" },
                        { 16, "Pets" },
                        { 17, "PrivateBathroom" },
                        { 18, "SeaView" },
                        { 19, "HydromassageBath" },
                        { 20, "Terrace" },
                        { 21, "Sofa" },
                        { 22, "Dishwasher" },
                        { 23, "Bath" },
                        { 24, "Soundproofing" },
                        { 25, "Refrigerator" },
                        { 26, "IroningAccessories" },
                        { 27, "Shower" },
                        { 28, "WashingMachine" },
                        { 29, "Toilet" },
                        { 30, "Pool" }
                   });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name", "IsActive" },
                values: new object[,]
                {
                    { "7ee80d45-33f6-4ed5-a729-55d0372a3075", "Италия", true },
                    { "aef107bc-e839-4ca7-9fdf-31580887d2c8", "Испания", true },
                    { "bef28182-c76f-4467-a393-b39790b2cd0a", "Англия", true },
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaseCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeId);
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "RoomTypeId", "Name", "BaseCost", "IsActive" },
                values: new object[,]
                {
                    { "8c6d84dc-01ef-4451-89cb-d8af4af7be2c", "Standart", 1000.5m, true},
                    { "24255a44-d32f-47bf-8deb-4ac3e69adf3a", "Improved", 2000.5m, true},
                    { "ca7083ba-52f1-4c83-bcbd-8232ebb139ab", "Lux", 3000.5m, true},
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name", "IsActive", "CountryId" },
                values: new object[,]
                {
                    { "12884305-a62a-4ac5-87e1-7f5e8630bd3b", "Рим", true, "7ee80d45-33f6-4ed5-a729-55d0372a3075" },
                    { "72d878a3-4819-48b3-b83e-03d255bf6c9b", "Мадрид", true, "aef107bc-e839-4ca7-9fdf-31580887d2c8" },
                    { "e2ed8552-529a-49c0-9d1d-ad0e3b9a456f", "Лондон", true, "bef28182-c76f-4467-a393-b39790b2cd0a" },
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Street", "HouseNumber", "Latitude", "Longitude", "IsActive", "CityId" },
                values: new object[,]
                {
                    { "d078823a-95b4-4bc7-b9a6-a8543b973db9", "ул. Аурелио", "831", 41.88738m , 12.40484m, true, "12884305-a62a-4ac5-87e1-7f5e8630bd3b" },
                    { "73cd0dc9-eb12-4022-894e-6bb295b6c8e7", "ул. Виа Венето", "118", 41.90864m , 12.48877m, true, "12884305-a62a-4ac5-87e1-7f5e8630bd3b" },
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotels_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "Name", "Description", "IBAN", "Rating", "IsActive", "Image", "AddressId" },
                values: new object[,]
                {
                    { "35f37340-f9e5-4118-b949-08dc51cc57b7", "Roma Camping In Town", "Комплекс для отдыха Roma Camping In Town расположен всего в 15 минутах езды от Ватикана. К услугам гостей сезонный открытый бассейн, гидромассажная ванна и бар у бассейна.", "IT60X0542811101000000123456", 5, true, "https://www.example.com", "d078823a-95b4-4bc7-b9a6-a8543b973db9" },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b8", "Hotel Via Veneto", "Hotel Via Veneto — это отель в городе Рим, в 700 м от такой достопримечательности, как Площадь Пьяцца Барберини.", "IT60X0542811101000000123456", 5, true, "https://www.example.com", "73cd0dc9-eb12-4022-894e-6bb295b6c8e7" },
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Floor = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    Number = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Floor", "Number", "IsActive", "Image", "RoomTypeId", "HotelId" },
                values: new object[,]
                {
                    { "35f37340-f9e5-4118-b949-08dc51cc57b1", 1, 2, true, "https://www.room1.com", "8c6d84dc-01ef-4451-89cb-d8af4af7be2c", "35f37340-f9e5-4118-b949-08dc51cc57b7"},
                    { "35f37340-f9e5-4118-b949-08dc51cc57b2", 5, 51, true, "https://www.room2.com", "24255a44-d32f-47bf-8deb-4ac3e69adf3a", "35f37340-f9e5-4118-b949-08dc51cc57b7"},
                    { "35f37340-f9e5-4118-b949-08dc51cc57b3", 1, 6, true, "https://www.room3.com", "ca7083ba-52f1-4c83-bcbd-8232ebb139ab", "35f37340-f9e5-4118-b949-08dc51cc57b8"},
                    { "35f37340-f9e5-4118-b949-08dc51cc57b4", 3, 33, true, "https://www.room4.com", "24255a44-d32f-47bf-8deb-4ac3e69adf3a", "35f37340-f9e5-4118-b949-08dc51cc57b8"},
                });

            migrationBuilder.CreateTable(
                name: "AmenityRoom",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityRoom", x => new { x.AmenityId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_AmenityRoom_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "AmenityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AmenityRoom",
                columns: new[] { "RoomId", "AmenityId" },
                values: new object[,]
                {
                    { "35f37340-f9e5-4118-b949-08dc51cc57b1", 1 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b1", 2 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b1", 3 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b1", 4 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b1", 5 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b2", 1 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b2", 2 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b2", 3 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b3", 1 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b3", 2 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b3", 5 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b3", 6 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b3", 7 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b4", 1 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b4", 2 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b4", 3 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b4", 4 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b4", 5 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b4", 6 },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b4", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityRoom_RoomId",
                table: "AmenityRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_AddressId",
                table: "Hotels",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityRoom");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
