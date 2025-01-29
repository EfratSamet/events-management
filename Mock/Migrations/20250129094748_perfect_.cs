using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    public partial class perfect_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "GuestInEvents");

            migrationBuilder.AddColumn<string>(
                name: "group",
                table: "GuestInEvents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GuestInEvents_group",
                table: "GuestInEvents",
                column: "group");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestInEvents_Groups_group",
                table: "GuestInEvents",
                column: "group",
                principalTable: "Groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestInEvents_Groups_group",
                table: "GuestInEvents");

            migrationBuilder.DropIndex(
                name: "IX_GuestInEvents_group",
                table: "GuestInEvents");

            migrationBuilder.DropColumn(
                name: "group",
                table: "GuestInEvents");

            migrationBuilder.AddColumn<int>(
                name: "category",
                table: "GuestInEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
