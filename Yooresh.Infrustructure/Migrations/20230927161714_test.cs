using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yooresh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Village",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceFood = table.Column<int>(type: "int", nullable: false),
                    ResourceLumber = table.Column<int>(type: "int", nullable: false),
                    ResourceStone = table.Column<int>(type: "int", nullable: false),
                    ResourceGold = table.Column<int>(type: "int", nullable: false),
                    ResourceMetal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Village", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Village_Faction_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Faction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Village_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Village_FactionId",
                table: "Village",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Village_PlayerId",
                table: "Village",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Village");

            migrationBuilder.DropTable(
                name: "Faction");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
