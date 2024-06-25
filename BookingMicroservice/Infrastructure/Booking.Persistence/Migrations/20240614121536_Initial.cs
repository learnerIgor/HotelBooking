using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.ApplicationUserId);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "ApplicationUserId", "Login", "IsActive", "Email" },
                values: new object[,]
                {
                    { "35f37340-f9e5-4118-b949-08dc51cc57b7", "Admin", true, "igorekmu.levckovets@gmail.com" }
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSendEmail = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "PaymentDate", "Amount", "IsSendEmail", "IsActive" },
                values: new object[,]
                {
                     { "94271ac7-7ba0-4c51-abfc-492717388215", DateTime.UtcNow, 1000, true, true },
                     { "364c3370-81dd-4c55-ba72-4cbbfacbffaa", DateTime.UtcNow, 1000, true, true },
                     { "4ddb253b-2435-4072-96c6-328d6154d1aa", DateTime.UtcNow, 1000, true, true },
                     { "e549c827-a227-48f4-8be2-eb1c118fb0dc", DateTime.UtcNow, 1000, true, true },
                     { "f9a026fa-1755-4dec-a7a6-7e652fe9595d", DateTime.UtcNow, 1000, true, true },
                     { "1dd3cb68-6346-4e08-8d6e-6c473e1dc3e4", DateTime.UtcNow, 1000, true, true },
                     { "ed383ca8-1281-424c-8f54-cd9f103e1de6", DateTime.UtcNow, 1000, true, true },
                     { "59fb6e6b-add7-4ef8-ad60-a3936e71ad8a", DateTime.UtcNow, 1000, true, true },
                     { "ddecd1f2-833d-4ead-89e5-f72b6e115cdf", DateTime.UtcNow, 1000, true, true },
                     { "7bd48d7d-3a36-4473-a0b4-a24f4aa2bbd6", DateTime.UtcNow, 1000, true, true },
                     { "c1abfc19-a854-4352-9a6f-4598d31ff088", DateTime.UtcNow, 1000, true, true },
                     { "242f2677-57f7-48ef-9ccc-4a06cc5bb503", DateTime.UtcNow, 1000, true, true },

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
                    { "8c6d84dc-01ef-4451-89cb-d8af4af7be2c", "Стандартный", 1000.5m, true},
                    { "24255a44-d32f-47bf-8deb-4ac3e69adf3a", "Улучшенный", 2000.5m, true},
                    { "ca7083ba-52f1-4c83-bcbd-8232ebb139ab", "Люкс", 3000.5m, true},
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
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
                columns: new[] { "HotelId", "Name", "Description", "Rating", "IsActive", "IBAN", "Image", "AddressId" },
                values: new object[,]
                {
                    { "35f37340-f9e5-4118-b949-08dc51cc57b7", "Roma Camping In Town", "Комплекс для отдыха Roma Camping In Town расположен всего в 15 минутах езды от Ватикана. К услугам гостей сезонный открытый бассейн, гидромассажная ванна и бар у бассейна.", 5, true, "IT60X0542811101000000123456", "https://www.example.com", "d078823a-95b4-4bc7-b9a6-a8543b973db9" },
                    { "35f37340-f9e5-4118-b949-08dc51cc57b8", "Hotel Via Veneto", "Hotel Via Veneto — это отель в городе Рим, в 700 м от такой достопримечательности, как Площадь Пьяцца Барберини.", 5, true, "IT60X0542811101000000123456", "https://www.example.com", "73cd0dc9-eb12-4022-894e-6bb295b6c8e7" },
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CheckInDate", "CheckOutDate", "IsActive", "ApplicationUserId", "PaymentId", "RoomId" },
                values: new object[,]
                {
                    { "35f37347-f9e5-4118-b949-08dc51cc57b2", DateTime.UtcNow, DateTime.UtcNow.AddDays(7), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "94271ac7-7ba0-4c51-abfc-492717388215", "35F37340-F9E5-4118-B949-08DC51CC57B1" },
                    { "148e326b-710e-4610-833c-787af8b4f316", DateTime.UtcNow.AddDays(15), DateTime.UtcNow.AddDays(30), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "364c3370-81dd-4c55-ba72-4cbbfacbffaa", "35f37340-f9e5-4118-b949-08dc51cc57b4" },
                    { "318183e9-86be-480e-8e03-d97aa9813230", DateTime.UtcNow.AddDays(35), DateTime.UtcNow.AddDays(50), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "4ddb253b-2435-4072-96c6-328d6154d1aa", "35F37340-F9E5-4118-B949-08DC51CC57B3" },
                    { "cceacc72-c17b-41bc-9cc1-bbfbaca484c5", DateTime.UtcNow.AddDays(31), DateTime.UtcNow.AddDays(45), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "e549c827-a227-48f4-8be2-eb1c118fb0dc", "35f37340-f9e5-4118-b949-08dc51cc57b4" },
                    { "09dafdd0-6c9c-416f-a3d7-7c47467c4e31", DateTime.UtcNow.AddDays(30), DateTime.UtcNow.AddDays(37), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "f9a026fa-1755-4dec-a7a6-7e652fe9595d", "35F37340-F9E5-4118-B949-08DC51CC57B1" },
                    { "b6268ce1-f1a6-4782-a864-466df4677041", DateTime.UtcNow.AddDays(50), DateTime.UtcNow.AddDays(64), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "1dd3cb68-6346-4e08-8d6e-6c473e1dc3e4", "35F37340-F9E5-4118-B949-08DC51CC57B2" },
                    { "e38e35b6-379c-47e2-83d0-75f19425c9bb", DateTime.UtcNow.AddDays(11), DateTime.UtcNow.AddDays(30), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "ed383ca8-1281-424c-8f54-cd9f103e1de6", "35F37340-F9E5-4118-B949-08DC51CC57B3" },
                    { "35f37000-f9e5-4118-b949-08dc51cc57b2", DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(14), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "59fb6e6b-add7-4ef8-ad60-a3936e71ad8a", "35F37340-F9E5-4118-B949-08DC51CC57B2" },
                    { "dc70b57b-7803-415f-99df-44f574187539", DateTime.UtcNow, DateTime.UtcNow.AddDays(10), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "ddecd1f2-833d-4ead-89e5-f72b6e115cdf", "35f37340-f9e5-4118-b949-08dc51cc57b4" },
                    { "1114d20e-e6d4-4308-9d76-afbe7050994c", DateTime.UtcNow.AddDays(17), DateTime.UtcNow.AddDays(27), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "7bd48d7d-3a36-4473-a0b4-a24f4aa2bbd6", "35F37340-F9E5-4118-B949-08DC51CC57B1" },
                    { "2f1bbdec-b04a-4dab-9860-ff2dd4b9da55", DateTime.UtcNow.AddDays(21), DateTime.UtcNow.AddDays(42), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "c1abfc19-a854-4352-9a6f-4598d31ff088", "35F37340-F9E5-4118-B949-08DC51CC57B2" },
                    { "abcbaf91-62d2-4d18-83f9-b32fc0444f96", DateTime.UtcNow, DateTime.UtcNow.AddDays(10), true, "35F37340-F9E5-4118-B949-08DC51CC57B7", "242f2677-57f7-48ef-9ccc-4a06cc5bb503", "35F37340-F9E5-4118-B949-08DC51CC57B3" },

                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

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
                name: "IX_Reservations_ApplicationUserId",
                table: "Reservations",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PaymentId",
                table: "Reservations",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

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
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Payments");

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