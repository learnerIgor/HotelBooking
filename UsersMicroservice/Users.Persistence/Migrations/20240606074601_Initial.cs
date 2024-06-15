using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserRoles",
                columns: table => new
                {
                    ApplicationUserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRoles", x => x.ApplicationUserRoleId);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUserRoles",
                columns: new[] { "ApplicationUserRoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Client" },
                    { 2, "Admin" },
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.ApplicationUserId);
                });

            //Password 12345678
            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "ApplicationUserId", "Login", "PasswordHash", "Email", "IsActive", "CreatedDate" },
                values: new object[,]
                {
                    { "35f37340-f9e5-4118-b949-08dc51cc57b7", "Admin", "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv", "email@mail.com", true, DateTime.UtcNow }
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserApplicationUserRole",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserApplicationUserRole", x => new { x.ApplicationUserRoleId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplicationUserRole_ApplicationUserRoles_ApplicationUserRoleId",
                        column: x => x.ApplicationUserRoleId,
                        principalTable: "ApplicationUserRoles",
                        principalColumn: "ApplicationUserRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplicationUserRole_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
   table: "ApplicationUserApplicationUserRole",
   columns: new[] { "ApplicationUserId", "ApplicationUserRoleId" },
   values: new object[,]
   {
        { "35f37340-f9e5-4118-b949-08dc51cc57b7", 2 }
   });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserApplicationUserRole_ApplicationUserId",
                table: "ApplicationUserApplicationUserRole",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserApplicationUserRole");

            migrationBuilder.DropTable(
                name: "ApplicationUserRoles");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");
        }
    }
}
