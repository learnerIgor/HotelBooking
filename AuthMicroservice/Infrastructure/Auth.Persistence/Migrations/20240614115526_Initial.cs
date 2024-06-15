using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Persistence.Migrations
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.ApplicationUserId);
                });

            //Password 12345678
            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "ApplicationUserId", "Login", "PasswordHash", "IsActive" },
                values: new object[,]
                {
                    { "35f37340-f9e5-4118-b949-08dc51cc57b7", "Admin", "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv", true }
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserApplicationUserRole",
                columns: table => new
                {
                    RolesApplicationUserRoleId = table.Column<int>(type: "int", nullable: false),
                    UsersApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserApplicationUserRole", x => new { x.RolesApplicationUserRoleId, x.UsersApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplicationUserRole_ApplicationUserRoles_RolesApplicationUserRoleId",
                        column: x => x.RolesApplicationUserRoleId,
                        principalTable: "ApplicationUserRoles",
                        principalColumn: "ApplicationUserRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplicationUserRole_ApplicationUsers_UsersApplicationUserId",
                        column: x => x.UsersApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
   table: "ApplicationUserApplicationUserRole",
   columns: new[] { "UsersApplicationUserId", "RolesApplicationUserRoleId" },
   values: new object[,]
   {
        { "35f37340-f9e5-4118-b949-08dc51cc57b7", 2 }
   });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expired = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserApplicationUserRole_UsersApplicationUserId",
                table: "ApplicationUserApplicationUserRole",
                column: "UsersApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ApplicationUserId",
                table: "RefreshTokens",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserApplicationUserRole");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ApplicationUserRoles");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");
        }
    }
}
