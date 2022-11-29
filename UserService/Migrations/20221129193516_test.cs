using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    DoorDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_CityInfo_CityInfoId",
                        column: x => x.CityInfoId,
                        principalTable: "CityInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CityInfo",
                columns: new[] { "Id", "City", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Hillerød", "3400" },
                    { 2, "Fredensborg", "3480" },
                    { 3, "Taastrup", "2630" },
                    { 4, "Hedehusene", "2640" },
                    { 5, "Charlottenlund", "2920" },
                    { 6, "", "3000" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { 1, "Customer" },
                    { 2, "DeliveryPerson" },
                    { 3, "RestaurantOwner" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "CityInfoId", "DoorDesignation", "Floor", "HouseNumber", "StreetName" },
                values: new object[] { 1, 1, "tv", 3, "94A", "Skovledet" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "CityInfoId", "DoorDesignation", "Floor", "HouseNumber", "StreetName" },
                values: new object[] { 2, 5, null, 2, "23", "Cphbusinessvej" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "CreatedAt", "Email", "FirstName", "ModifiedAt", "Password", "RoleId" },
                values: new object[] { 1, 1, new DateTime(2022, 11, 29, 19, 35, 15, 536, DateTimeKind.Utc).AddTicks(4552), "phillip.andersen1999@gmail.com", "Phillip", new DateTime(2022, 11, 29, 19, 35, 15, 536, DateTimeKind.Utc).AddTicks(4554), "$2a$11$ESap8QUHcYFD4/fAX4sYHOGcPmLcsKp9kepE1HJHb.E1Z8x96NdGu", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "CreatedAt", "Email", "FirstName", "ModifiedAt", "Password", "RoleId" },
                values: new object[] { 2, 2, new DateTime(2022, 11, 29, 19, 35, 15, 648, DateTimeKind.Utc).AddTicks(6599), "lukasbangstoltz@gmail.com", "Lukas", new DateTime(2022, 11, 29, 19, 35, 15, 648, DateTimeKind.Utc).AddTicks(6604), "$2a$11$KuNIBlLCXLLzjeTgYohKb.1yLPe3DmmVaBuwulgc16SB..vVR9O1.", 3 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "CreatedAt", "Email", "FirstName", "ModifiedAt", "Password", "RoleId" },
                values: new object[] { 3, 2, new DateTime(2022, 11, 29, 19, 35, 15, 760, DateTimeKind.Utc).AddTicks(5839), "christofferiw@gmail.com", "Christoffer", new DateTime(2022, 11, 29, 19, 35, 15, 760, DateTimeKind.Utc).AddTicks(5843), "$2a$11$Kx.C7H72DHoclLU8oB57muL1qMD2XnfN1OHqTPAM9EZpDJOj0OBl2", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityInfoId",
                table: "Address",
                column: "CityInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "CityInfo");
        }
    }
}
