using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    public partial class groups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Guests_guestId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_guestId",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "Groupid",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_Groupid",
                table: "Guests",
                column: "Groupid");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Groups_Groupid",
                table: "Guests",
                column: "Groupid",
                principalTable: "Groups",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Groups_Groupid",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_Groupid",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Groupid",
                table: "Guests");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_guestId",
                table: "Groups",
                column: "guestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Guests_guestId",
                table: "Groups",
                column: "guestId",
                principalTable: "Guests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
