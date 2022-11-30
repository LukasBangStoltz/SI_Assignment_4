using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbackService.Migrations
{
    public partial class yepppaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "DeliveryId", "Message", "Rating", "RestaurantId", "ReviewDate", "UserId" },
                values: new object[] { 1, 1, "Det var godt nok noget dårlig mad mand!!!", 1, 1, new DateTime(2022, 11, 30, 10, 3, 20, 440, DateTimeKind.Local).AddTicks(2850), 1 });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "DeliveryId", "Message", "Rating", "RestaurantId", "ReviewDate", "UserId" },
                values: new object[] { 2, 1, "Det var godt nok noget lækker mad mand!!!", 5, 1, new DateTime(2022, 11, 30, 10, 3, 20, 440, DateTimeKind.Local).AddTicks(2893), 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
