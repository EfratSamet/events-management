using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    public partial class perfect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SubGuests",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    guestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGuests", x => x.id);
                    table.ForeignKey(
                        name: "FK_SubGuests_Guests_guestId",
                        column: x => x.guestId,
                        principalTable: "Guests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    organizerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    eventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seperation = table.Column<bool>(type: "bit", nullable: false),
                    invitation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.id);
                    table.ForeignKey(
                        name: "FK_Events_Organizers_organizerId",
                        column: x => x.organizerId,
                        principalTable: "Organizers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organizerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    guestId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.id);
                    table.ForeignKey(
                        name: "FK_Groups_Guests_guestId",
                        column: x => x.guestId,
                        principalTable: "Guests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Organizers_organizerId",
                        column: x => x.organizerId,
                        principalTable: "Organizers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuestInEvents",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    guestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    eventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ok = table.Column<bool>(type: "bit", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestInEvents", x => x.id);
                    table.ForeignKey(
                        name: "FK_GuestInEvents_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestInEvents_Guests_guestId",
                        column: x => x.guestId,
                        principalTable: "Guests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotosFromEvents",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    guestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    eventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    blessing = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosFromEvents", x => x.id);
                    table.ForeignKey(
                        name: "FK_PhotosFromEvents_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotosFromEvents_Guests_guestId",
                        column: x => x.guestId,
                        principalTable: "Guests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seatings",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    eventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    subGuestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    table = table.Column<int>(type: "int", nullable: false),
                    seat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seatings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Seatings_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seatings_SubGuests_subGuestId",
                        column: x => x.subGuestId,
                        principalTable: "SubGuests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_organizerId",
                table: "Events",
                column: "organizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_guestId",
                table: "Groups",
                column: "guestId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_organizerId",
                table: "Groups",
                column: "organizerId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestInEvents_eventId",
                table: "GuestInEvents",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestInEvents_guestId",
                table: "GuestInEvents",
                column: "guestId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosFromEvents_eventId",
                table: "PhotosFromEvents",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosFromEvents_guestId",
                table: "PhotosFromEvents",
                column: "guestId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_eventId",
                table: "Seatings",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_subGuestId",
                table: "Seatings",
                column: "subGuestId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGuests_guestId",
                table: "SubGuests",
                column: "guestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "GuestInEvents");

            migrationBuilder.DropTable(
                name: "PhotosFromEvents");

            migrationBuilder.DropTable(
                name: "Seatings");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "SubGuests");

            migrationBuilder.DropTable(
                name: "Organizers");

            migrationBuilder.DropTable(
                name: "Guests");
        }
    }
}
