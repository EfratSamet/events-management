using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    public partial class guestgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Groups_Groupid",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "Groupid",
                table: "Guests",
                newName: "groupId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_Groupid",
                table: "Guests",
                newName: "IX_Guests_groupId");

            migrationBuilder.AlterColumn<int>(
                name: "groupId",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Groups_groupId",
                table: "Guests",
                column: "groupId",
                principalTable: "Groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Groups_groupId",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "groupId",
                table: "Guests",
                newName: "Groupid");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_groupId",
                table: "Guests",
                newName: "IX_Guests_Groupid");

            migrationBuilder.AlterColumn<int>(
                name: "Groupid",
                table: "Guests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Groups_Groupid",
                table: "Guests",
                column: "Groupid",
                principalTable: "Groups",
                principalColumn: "id");
        }
    }
}
