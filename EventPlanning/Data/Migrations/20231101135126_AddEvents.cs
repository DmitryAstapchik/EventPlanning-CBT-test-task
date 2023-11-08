using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanning.Data.Migrations
{
    public partial class AddEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsDetails");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxRegisteredUsers = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventProperty_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventUserDetails",
                columns: table => new
                {
                    RegisteredEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisteredUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUserDetails", x => new { x.RegisteredEventsId, x.RegisteredUsersId });
                    table.ForeignKey(
                        name: "FK_EventUserDetails_Events_RegisteredEventsId",
                        column: x => x.RegisteredEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUserDetails_UsersDetails_RegisteredUsersId",
                        column: x => x.RegisteredUsersId,
                        principalTable: "UsersDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventProperty_EventId",
                table: "EventProperty",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUserDetails_RegisteredUsersId",
                table: "EventUserDetails",
                column: "RegisteredUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventProperty");

            migrationBuilder.DropTable(
                name: "EventUserDetails");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "UsersDetails");

            migrationBuilder.CreateTable(
                name: "AccountsDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsDetails", x => x.Id);
                });
        }
    }
}
