using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    Advantages = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Disadvantages = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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
                name: "ResourceBuilding",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpgradeDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    UpgradeCostFood = table.Column<int>(type: "int", nullable: false),
                    UpgradeCostLumber = table.Column<int>(type: "int", nullable: false),
                    UpgradeCostStone = table.Column<int>(type: "int", nullable: false),
                    UpgradeCostGold = table.Column<int>(type: "int", nullable: false),
                    UpgradeCostMetal = table.Column<int>(type: "int", nullable: false),
                    RequirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HourlyProductionFood = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionLumber = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionStone = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionGold = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionMetal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceBuilding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceBuilding_ResourceBuilding_RequirementId",
                        column: x => x.RequirementId,
                        principalTable: "ResourceBuilding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Village",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceFood = table.Column<int>(type: "int", nullable: false),
                    ResourceLumber = table.Column<int>(type: "int", nullable: false),
                    ResourceStone = table.Column<int>(type: "int", nullable: false),
                    ResourceGold = table.Column<int>(type: "int", nullable: false),
                    ResourceMetal = table.Column<int>(type: "int", nullable: false),
                    AvailableBuilders = table.Column<int>(type: "int", nullable: false),
                    LastResourceChangeTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "VillageResourceBuilding",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceBuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillageResourceBuilding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VillageResourceBuilding_ResourceBuilding_ResourceBuildingId",
                        column: x => x.ResourceBuildingId,
                        principalTable: "ResourceBuilding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VillageResourceBuilding_Village_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Village",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VillageUpgradeQueue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpgradeType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillageUpgradeQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VillageUpgradeQueue_Village_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Village",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Faction",
                columns: new[] { "Id", "Advantages", "Disadvantages", "Name" },
                values: new object[,]
                {
                    { new Guid("012fc556-7788-42b7-82f7-f093d37ec517"), "Advantage1,Advantage2", "Disadvantage1,Disadvantage2", "Orc" },
                    { new Guid("1a060e07-fd38-4c1b-96e8-b8daee50421e"), "Advantage1,Advantage2", "Disadvantage1,Disadvantage2", "Elf" },
                    { new Guid("52559f4b-4052-4438-b5b9-9bac92dc75fb"), "Advantage1,Advantage2", "Disadvantage1,Disadvantage2", "Human" },
                    { new Guid("56d9e842-3c04-4741-9cda-4bfe38aa1f57"), "Advantage1,Advantage2", "Disadvantage1,Disadvantage2", "Undead" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceBuilding_RequirementId",
                table: "ResourceBuilding",
                column: "RequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_Village_FactionId",
                table: "Village",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Village_PlayerId",
                table: "Village",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VillageResourceBuilding_ResourceBuildingId",
                table: "VillageResourceBuilding",
                column: "ResourceBuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_VillageResourceBuilding_VillageId",
                table: "VillageResourceBuilding",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_VillageUpgradeQueue_VillageId",
                table: "VillageUpgradeQueue",
                column: "VillageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillageResourceBuilding");

            migrationBuilder.DropTable(
                name: "VillageUpgradeQueue");

            migrationBuilder.DropTable(
                name: "ResourceBuilding");

            migrationBuilder.DropTable(
                name: "Village");

            migrationBuilder.DropTable(
                name: "Faction");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
