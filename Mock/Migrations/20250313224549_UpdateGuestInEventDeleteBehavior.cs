using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    public partial class UpdateGuestInEventDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestInEvents_Events_eventId",
                table: "GuestInEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestInEvents_Events_eventId",
                table: "GuestInEvents",
                column: "eventId",
                principalTable: "Events",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestInEvents_Events_eventId",
                table: "GuestInEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestInEvents_Events_eventId",
                table: "GuestInEvents",
                column: "eventId",
                principalTable: "Events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
