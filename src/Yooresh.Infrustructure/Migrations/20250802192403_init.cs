using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yooresh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingType = table.Column<string>(type: "nvarchar(30)", maxLength: 20, nullable: false),
                    UpgradeCostFood = table.Column<int>(type: "int", nullable: false),
                    UpgradeCostLumber = table.Column<int>(type: "int", nullable: false),
                    UpgradeCostStone = table.Column<int>(type: "int", nullable: false),
                    UpgradeCostGold = table.Column<int>(type: "int", nullable: false),
                    UpgradeCostMetal = table.Column<int>(type: "int", nullable: false),
                    UpgradeDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Building_Building_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Building",
                        principalColumn: "Id");
                });

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
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(24)", maxLength: 20, nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ConfirmationCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(24)", maxLength: 20, nullable: false),
                    AvailableAmount = table.Column<int>(type: "int", nullable: false),
                    HarvestRatePerMinute = table.Column<int>(type: "int", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlyProductionFood = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionLumber = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionStone = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionGold = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionMetal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Farm_Building_Id",
                        column: x => x.Id,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoldMine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlyProductionFood = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionLumber = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionStone = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionGold = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionMetal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoldMine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoldMine_Building_Id",
                        column: x => x.Id,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LumberMill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlyProductionFood = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionLumber = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionStone = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionGold = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionMetal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LumberMill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LumberMill_Building_Id",
                        column: x => x.Id,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetalMine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlyProductionFood = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionLumber = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionStone = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionGold = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionMetal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetalMine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetalMine_Building_Id",
                        column: x => x.Id,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoneMine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlyProductionFood = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionLumber = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionStone = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionGold = table.Column<int>(type: "int", nullable: false),
                    HourlyProductionMetal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoneMine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoneMine_Building_Id",
                        column: x => x.Id,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tower",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefenseAgainstMeleeInfantry = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstRangeInfantry = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstCavalry = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstMage = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstFlyingCavalry = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstSiegeUnit = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    RepairCostFood = table.Column<int>(type: "int", nullable: false),
                    RepairCostLumber = table.Column<int>(type: "int", nullable: false),
                    RepairCostStone = table.Column<int>(type: "int", nullable: false),
                    RepairCostGold = table.Column<int>(type: "int", nullable: false),
                    RepairCostMetal = table.Column<int>(type: "int", nullable: false),
                    TroopCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tower_Building_Id",
                        column: x => x.Id,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wall",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefenseAgainstMeleeInfantry = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstRangeInfantry = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstCavalry = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstMage = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstFlyingCavalry = table.Column<int>(type: "int", nullable: false),
                    DefenseAgainstSiegeUnit = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    RepairCostFood = table.Column<int>(type: "int", nullable: false),
                    RepairCostLumber = table.Column<int>(type: "int", nullable: false),
                    RepairCostStone = table.Column<int>(type: "int", nullable: false),
                    RepairCostGold = table.Column<int>(type: "int", nullable: false),
                    RepairCostMetal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wall_Building_Id",
                        column: x => x.Id,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Village",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailableBuilders = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "VillageBuilding",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastHarvestTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillageBuilding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VillageBuilding_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VillageBuilding_Village_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Village",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VillageTowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillageTowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VillageTowers_Tower_TowerId",
                        column: x => x.TowerId,
                        principalTable: "Tower",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VillageTowers_Village_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Village",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VillageWalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillageWalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VillageWalls_Village_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Village",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VillageWalls_Wall_WallId",
                        column: x => x.WallId,
                        principalTable: "Wall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Troop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageTowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Troop_VillageTowers_VillageTowerId",
                        column: x => x.VillageTowerId,
                        principalTable: "VillageTowers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("71101885-0649-4714-a217-8db19374c3fc"), 12, 12, 12, 12, 12, "Tower", 24, null, new TimeSpan(9, 14, 24, 0, 0) },
                    { new Guid("9e4bb5b6-90da-434d-837d-8b0d0d88b9c4"), 12, 12, 12, 12, 12, "GoldMine", 24, null, new TimeSpan(9, 14, 24, 0, 0) },
                    { new Guid("ca2be18d-e1e0-4afa-9f97-1eda67a8a83b"), 12, 12, 12, 12, 12, "StoneMine", 24, null, new TimeSpan(9, 14, 24, 0, 0) },
                    { new Guid("d37e1071-c448-4ea4-a83c-5ef225281005"), 12, 12, 12, 12, 12, "Farm", 24, null, new TimeSpan(9, 14, 24, 0, 0) },
                    { new Guid("e4d4395f-aa76-48d4-aabe-b4468e1bf9b5"), 12, 12, 12, 12, 12, "MetalMine", 24, null, new TimeSpan(9, 14, 24, 0, 0) },
                    { new Guid("f2c3ea31-09ee-4c0c-a528-e4a6f33cebf6"), 12, 12, 12, 12, 12, "Lumbermill", 24, null, new TimeSpan(9, 14, 24, 0, 0) },
                    { new Guid("f9927613-7d7f-47b0-b390-83fec5fd1c31"), 12, 12, 12, 12, 12, "Wall", 24, null, new TimeSpan(9, 14, 24, 0, 0) }
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

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "ConfirmationCode", "Confirmed", "Email", "Name", "Password", "Role" },
                values: new object[] { new Guid("a58bef94-8437-4856-bd87-a7b861285773"), "123", true, "alireza.sabouei@gmail.com", "Alireza Sabouei", "Aa123456", "SimplePlayer" });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("3396bf28-f455-4f5b-8d38-0466c48d119a"), 3, 3, 3, 3, 3, "MetalMine", 23, new Guid("e4d4395f-aa76-48d4-aabe-b4468e1bf9b5"), new TimeSpan(8, 10, 47, 0, 0) },
                    { new Guid("5ac05b39-04bb-4186-bbea-5b1aa5abbeb6"), 3, 3, 3, 3, 3, "Farm", 23, new Guid("d37e1071-c448-4ea4-a83c-5ef225281005"), new TimeSpan(8, 10, 47, 0, 0) },
                    { new Guid("7ab049fc-227b-43a4-b539-1e97abf0993b"), 3, 3, 3, 3, 3, "StoneMine", 23, new Guid("ca2be18d-e1e0-4afa-9f97-1eda67a8a83b"), new TimeSpan(8, 10, 47, 0, 0) },
                    { new Guid("9c0b6a3b-a963-478b-98da-a250b8fd6c6f"), 3, 3, 3, 3, 3, "Wall", 23, new Guid("f9927613-7d7f-47b0-b390-83fec5fd1c31"), new TimeSpan(8, 10, 47, 0, 0) },
                    { new Guid("a1e29d5f-2c37-4509-8a74-3f425227b8d9"), 3, 3, 3, 3, 3, "GoldMine", 23, new Guid("9e4bb5b6-90da-434d-837d-8b0d0d88b9c4"), new TimeSpan(8, 10, 47, 0, 0) },
                    { new Guid("b40529ca-90ce-4388-a53d-a9cb2ff8816e"), 3, 3, 3, 3, 3, "Lumbermill", 23, new Guid("f2c3ea31-09ee-4c0c-a528-e4a6f33cebf6"), new TimeSpan(8, 10, 47, 0, 0) },
                    { new Guid("ec40e968-5781-4c30-9370-c403e28b83ba"), 3, 3, 3, 3, 3, "Tower", 23, new Guid("71101885-0649-4714-a217-8db19374c3fc"), new TimeSpan(8, 10, 47, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("d37e1071-c448-4ea4-a83c-5ef225281005"), 34560, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("9e4bb5b6-90da-434d-837d-8b0d0d88b9c4"), 34560, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("f2c3ea31-09ee-4c0c-a528-e4a6f33cebf6"), 34560, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("e4d4395f-aa76-48d4-aabe-b4468e1bf9b5"), 34560, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("ca2be18d-e1e0-4afa-9f97-1eda67a8a83b"), 34560, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("71101885-0649-4714-a217-8db19374c3fc"), 14400, 5760, 2880, 2880, 2880, 2880, 2880, 576, 34560, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("f9927613-7d7f-47b0-b390-83fec5fd1c31"), 14400, 2880, 2880, 2880, 2880, 2880, 576, 34560, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("26924f6f-efaa-4b42-80ee-3deb38558f22"), 2, 2, 2, 2, 2, "Wall", 22, new Guid("9c0b6a3b-a963-478b-98da-a250b8fd6c6f"), new TimeSpan(7, 9, 28, 0, 0) },
                    { new Guid("3eb817be-8a90-40b9-9de2-6637c3ea9611"), 2, 2, 2, 2, 2, "Tower", 22, new Guid("ec40e968-5781-4c30-9370-c403e28b83ba"), new TimeSpan(7, 9, 28, 0, 0) },
                    { new Guid("864ae7f8-8262-415e-81c3-829a5659b040"), 2, 2, 2, 2, 2, "Farm", 22, new Guid("5ac05b39-04bb-4186-bbea-5b1aa5abbeb6"), new TimeSpan(7, 9, 28, 0, 0) },
                    { new Guid("937feb33-a2a3-4d12-846c-0e3bab45b277"), 2, 2, 2, 2, 2, "Lumbermill", 22, new Guid("b40529ca-90ce-4388-a53d-a9cb2ff8816e"), new TimeSpan(7, 9, 28, 0, 0) },
                    { new Guid("94248d08-5c2d-4a77-9f17-402d1e8103f5"), 2, 2, 2, 2, 2, "MetalMine", 22, new Guid("3396bf28-f455-4f5b-8d38-0466c48d119a"), new TimeSpan(7, 9, 28, 0, 0) },
                    { new Guid("aa702882-bdec-4e0d-b1c9-8851104c42cb"), 2, 2, 2, 2, 2, "GoldMine", 22, new Guid("a1e29d5f-2c37-4509-8a74-3f425227b8d9"), new TimeSpan(7, 9, 28, 0, 0) },
                    { new Guid("e7de480f-fdc8-46f2-8832-c5e02a895ce6"), 2, 2, 2, 2, 2, "StoneMine", 22, new Guid("7ab049fc-227b-43a4-b539-1e97abf0993b"), new TimeSpan(7, 9, 28, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("5ac05b39-04bb-4186-bbea-5b1aa5abbeb6"), 31740, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a1e29d5f-2c37-4509-8a74-3f425227b8d9"), 31740, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("b40529ca-90ce-4388-a53d-a9cb2ff8816e"), 31740, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("3396bf28-f455-4f5b-8d38-0466c48d119a"), 31740, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("7ab049fc-227b-43a4-b539-1e97abf0993b"), 31740, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("ec40e968-5781-4c30-9370-c403e28b83ba"), 13225, 5290, 2645, 2645, 2645, 2645, 2645, 529, 31740, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("9c0b6a3b-a963-478b-98da-a250b8fd6c6f"), 13225, 2645, 2645, 2645, 2645, 2645, 529, 31740, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("0474bad5-9cf8-4729-be0d-505819cb2590"), 1, 1, 1, 1, 1, "StoneMine", 21, new Guid("e7de480f-fdc8-46f2-8832-c5e02a895ce6"), new TimeSpan(6, 10, 21, 0, 0) },
                    { new Guid("5b52be58-aa7d-404c-a45b-a334915457c5"), 1, 1, 1, 1, 1, "MetalMine", 21, new Guid("94248d08-5c2d-4a77-9f17-402d1e8103f5"), new TimeSpan(6, 10, 21, 0, 0) },
                    { new Guid("8e1032b2-e9bd-489f-80ad-d6da1cf1112d"), 1, 1, 1, 1, 1, "Wall", 21, new Guid("26924f6f-efaa-4b42-80ee-3deb38558f22"), new TimeSpan(6, 10, 21, 0, 0) },
                    { new Guid("9343cc5d-d692-428a-bb31-d400d089957b"), 1, 1, 1, 1, 1, "Lumbermill", 21, new Guid("937feb33-a2a3-4d12-846c-0e3bab45b277"), new TimeSpan(6, 10, 21, 0, 0) },
                    { new Guid("b6a59d14-ec22-4440-a81a-097716e691c4"), 1, 1, 1, 1, 1, "GoldMine", 21, new Guid("aa702882-bdec-4e0d-b1c9-8851104c42cb"), new TimeSpan(6, 10, 21, 0, 0) },
                    { new Guid("d796baf9-a221-4eed-928e-bef70413cb8b"), 1, 1, 1, 1, 1, "Tower", 21, new Guid("3eb817be-8a90-40b9-9de2-6637c3ea9611"), new TimeSpan(6, 10, 21, 0, 0) },
                    { new Guid("e2986d7a-c2c0-4203-8b27-a72495b48732"), 1, 1, 1, 1, 1, "Farm", 21, new Guid("864ae7f8-8262-415e-81c3-829a5659b040"), new TimeSpan(6, 10, 21, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("864ae7f8-8262-415e-81c3-829a5659b040"), 29040, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("aa702882-bdec-4e0d-b1c9-8851104c42cb"), 29040, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("937feb33-a2a3-4d12-846c-0e3bab45b277"), 29040, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("94248d08-5c2d-4a77-9f17-402d1e8103f5"), 29040, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("e7de480f-fdc8-46f2-8832-c5e02a895ce6"), 29040, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("3eb817be-8a90-40b9-9de2-6637c3ea9611"), 12100, 4840, 2420, 2420, 2420, 2420, 2420, 484, 29040, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("26924f6f-efaa-4b42-80ee-3deb38558f22"), 12100, 2420, 2420, 2420, 2420, 2420, 484, 29040, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("0a99fe94-cb60-4c53-be91-109ecd2c2518"), 0, 0, 0, 0, 0, "Lumbermill", 20, new Guid("9343cc5d-d692-428a-bb31-d400d089957b"), new TimeSpan(5, 13, 20, 0, 0) },
                    { new Guid("3b5fbd6c-5646-43ae-a824-0c907aee804c"), 0, 0, 0, 0, 0, "Farm", 20, new Guid("e2986d7a-c2c0-4203-8b27-a72495b48732"), new TimeSpan(5, 13, 20, 0, 0) },
                    { new Guid("70bd92be-3fa1-42a6-9d00-efb5cfd40598"), 0, 0, 0, 0, 0, "MetalMine", 20, new Guid("5b52be58-aa7d-404c-a45b-a334915457c5"), new TimeSpan(5, 13, 20, 0, 0) },
                    { new Guid("77612487-e9cf-43b5-bdf1-a6b2757f0b33"), 0, 0, 0, 0, 0, "GoldMine", 20, new Guid("b6a59d14-ec22-4440-a81a-097716e691c4"), new TimeSpan(5, 13, 20, 0, 0) },
                    { new Guid("809f3aa5-5426-4bcc-9f89-3f52ac0dcb90"), 0, 0, 0, 0, 0, "Wall", 20, new Guid("8e1032b2-e9bd-489f-80ad-d6da1cf1112d"), new TimeSpan(5, 13, 20, 0, 0) },
                    { new Guid("8737a844-6be6-4f9d-8b4d-35afa8511a86"), 0, 0, 0, 0, 0, "StoneMine", 20, new Guid("0474bad5-9cf8-4729-be0d-505819cb2590"), new TimeSpan(5, 13, 20, 0, 0) },
                    { new Guid("b916ea59-d230-43b5-bb0f-38ac43ca3e4b"), 0, 0, 0, 0, 0, "Tower", 20, new Guid("d796baf9-a221-4eed-928e-bef70413cb8b"), new TimeSpan(5, 13, 20, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("e2986d7a-c2c0-4203-8b27-a72495b48732"), 26460, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("b6a59d14-ec22-4440-a81a-097716e691c4"), 26460, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("9343cc5d-d692-428a-bb31-d400d089957b"), 26460, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("5b52be58-aa7d-404c-a45b-a334915457c5"), 26460, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("0474bad5-9cf8-4729-be0d-505819cb2590"), 26460, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("d796baf9-a221-4eed-928e-bef70413cb8b"), 11025, 4410, 2205, 2205, 2205, 2205, 2205, 441, 26460, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("8e1032b2-e9bd-489f-80ad-d6da1cf1112d"), 11025, 2205, 2205, 2205, 2205, 2205, 441, 26460, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("07fdde50-b1e6-45fa-abd2-e752b566f6b5"), 7, 7, 7, 7, 7, "Wall", 19, new Guid("809f3aa5-5426-4bcc-9f89-3f52ac0dcb90"), new TimeSpan(4, 18, 19, 0, 0) },
                    { new Guid("305ac3cb-2b6f-4bf3-a24e-64c3fe3bf13d"), 7, 7, 7, 7, 7, "Lumbermill", 19, new Guid("0a99fe94-cb60-4c53-be91-109ecd2c2518"), new TimeSpan(4, 18, 19, 0, 0) },
                    { new Guid("3ba0ec84-b2be-413e-97b7-0470030c9148"), 7, 7, 7, 7, 7, "MetalMine", 19, new Guid("70bd92be-3fa1-42a6-9d00-efb5cfd40598"), new TimeSpan(4, 18, 19, 0, 0) },
                    { new Guid("53e73e6c-73df-4ff7-ab9c-f6bf9de5699c"), 7, 7, 7, 7, 7, "GoldMine", 19, new Guid("77612487-e9cf-43b5-bdf1-a6b2757f0b33"), new TimeSpan(4, 18, 19, 0, 0) },
                    { new Guid("9e41a575-b042-4c91-b5ae-a1dc4e074501"), 7, 7, 7, 7, 7, "StoneMine", 19, new Guid("8737a844-6be6-4f9d-8b4d-35afa8511a86"), new TimeSpan(4, 18, 19, 0, 0) },
                    { new Guid("d54181e8-c026-4c67-89c4-f630b8976451"), 7, 7, 7, 7, 7, "Tower", 19, new Guid("b916ea59-d230-43b5-bb0f-38ac43ca3e4b"), new TimeSpan(4, 18, 19, 0, 0) },
                    { new Guid("fc140b50-aaf7-4535-9de7-bb22a0d6e4d2"), 7, 7, 7, 7, 7, "Farm", 19, new Guid("3b5fbd6c-5646-43ae-a824-0c907aee804c"), new TimeSpan(4, 18, 19, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("3b5fbd6c-5646-43ae-a824-0c907aee804c"), 24000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("77612487-e9cf-43b5-bdf1-a6b2757f0b33"), 24000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("0a99fe94-cb60-4c53-be91-109ecd2c2518"), 24000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("70bd92be-3fa1-42a6-9d00-efb5cfd40598"), 24000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("8737a844-6be6-4f9d-8b4d-35afa8511a86"), 24000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("b916ea59-d230-43b5-bb0f-38ac43ca3e4b"), 10000, 4000, 2000, 2000, 2000, 2000, 2000, 400, 24000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("809f3aa5-5426-4bcc-9f89-3f52ac0dcb90"), 10000, 2000, 2000, 2000, 2000, 2000, 400, 24000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("21512b79-b532-4eb9-8c37-927bda5d1804"), 6, 6, 6, 6, 6, "GoldMine", 18, new Guid("53e73e6c-73df-4ff7-ab9c-f6bf9de5699c"), new TimeSpan(4, 1, 12, 0, 0) },
                    { new Guid("618eb24b-2370-44e4-aeaa-6addb7c1e0bb"), 6, 6, 6, 6, 6, "Wall", 18, new Guid("07fdde50-b1e6-45fa-abd2-e752b566f6b5"), new TimeSpan(4, 1, 12, 0, 0) },
                    { new Guid("6c21abe2-51f5-4d87-a528-923e49047317"), 6, 6, 6, 6, 6, "Lumbermill", 18, new Guid("305ac3cb-2b6f-4bf3-a24e-64c3fe3bf13d"), new TimeSpan(4, 1, 12, 0, 0) },
                    { new Guid("7ba85d05-d1b2-478e-bafa-ab1a698b079f"), 6, 6, 6, 6, 6, "StoneMine", 18, new Guid("9e41a575-b042-4c91-b5ae-a1dc4e074501"), new TimeSpan(4, 1, 12, 0, 0) },
                    { new Guid("93ab61fb-52b4-49bb-80cc-16bba25d062d"), 6, 6, 6, 6, 6, "MetalMine", 18, new Guid("3ba0ec84-b2be-413e-97b7-0470030c9148"), new TimeSpan(4, 1, 12, 0, 0) },
                    { new Guid("a5479db9-93e7-442d-8292-5bf69c02a1a5"), 6, 6, 6, 6, 6, "Farm", 18, new Guid("fc140b50-aaf7-4535-9de7-bb22a0d6e4d2"), new TimeSpan(4, 1, 12, 0, 0) },
                    { new Guid("af9e76f6-36d1-4fa4-9842-98d8d43ca36f"), 6, 6, 6, 6, 6, "Tower", 18, new Guid("d54181e8-c026-4c67-89c4-f630b8976451"), new TimeSpan(4, 1, 12, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("fc140b50-aaf7-4535-9de7-bb22a0d6e4d2"), 21660, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("53e73e6c-73df-4ff7-ab9c-f6bf9de5699c"), 21660, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("305ac3cb-2b6f-4bf3-a24e-64c3fe3bf13d"), 21660, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("3ba0ec84-b2be-413e-97b7-0470030c9148"), 21660, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("9e41a575-b042-4c91-b5ae-a1dc4e074501"), 21660, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("d54181e8-c026-4c67-89c4-f630b8976451"), 9025, 3610, 1805, 1805, 1805, 1805, 1805, 361, 21660, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("07fdde50-b1e6-45fa-abd2-e752b566f6b5"), 9025, 1805, 1805, 1805, 1805, 1805, 361, 21660, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("084ee031-3131-4289-9fbf-42216cd17fcd"), 5, 5, 5, 5, 5, "Wall", 17, new Guid("618eb24b-2370-44e4-aeaa-6addb7c1e0bb"), new TimeSpan(3, 9, 53, 0, 0) },
                    { new Guid("25fd995d-96fd-41d6-b6be-900c82c391f8"), 5, 5, 5, 5, 5, "StoneMine", 17, new Guid("7ba85d05-d1b2-478e-bafa-ab1a698b079f"), new TimeSpan(3, 9, 53, 0, 0) },
                    { new Guid("5e03bfc4-245c-4fae-8fb8-3529b1544140"), 5, 5, 5, 5, 5, "Lumbermill", 17, new Guid("6c21abe2-51f5-4d87-a528-923e49047317"), new TimeSpan(3, 9, 53, 0, 0) },
                    { new Guid("6e7d7738-cd20-4443-a957-6bdbdc6e1108"), 5, 5, 5, 5, 5, "Farm", 17, new Guid("a5479db9-93e7-442d-8292-5bf69c02a1a5"), new TimeSpan(3, 9, 53, 0, 0) },
                    { new Guid("94411122-cd71-4109-bcdd-2b7cfe670cf6"), 5, 5, 5, 5, 5, "GoldMine", 17, new Guid("21512b79-b532-4eb9-8c37-927bda5d1804"), new TimeSpan(3, 9, 53, 0, 0) },
                    { new Guid("dbe552f8-63ec-4b8a-bdcf-0e6cbaf78ee3"), 5, 5, 5, 5, 5, "Tower", 17, new Guid("af9e76f6-36d1-4fa4-9842-98d8d43ca36f"), new TimeSpan(3, 9, 53, 0, 0) },
                    { new Guid("ebff94c5-461d-4fd2-9eff-f4602105a1df"), 5, 5, 5, 5, 5, "MetalMine", 17, new Guid("93ab61fb-52b4-49bb-80cc-16bba25d062d"), new TimeSpan(3, 9, 53, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a5479db9-93e7-442d-8292-5bf69c02a1a5"), 19440, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("21512b79-b532-4eb9-8c37-927bda5d1804"), 19440, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("6c21abe2-51f5-4d87-a528-923e49047317"), 19440, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("93ab61fb-52b4-49bb-80cc-16bba25d062d"), 19440, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("7ba85d05-d1b2-478e-bafa-ab1a698b079f"), 19440, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("af9e76f6-36d1-4fa4-9842-98d8d43ca36f"), 8100, 3240, 1620, 1620, 1620, 1620, 1620, 324, 19440, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("618eb24b-2370-44e4-aeaa-6addb7c1e0bb"), 8100, 1620, 1620, 1620, 1620, 1620, 324, 19440, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("169fc59e-fde7-43da-8926-c24977f1f520"), 4, 4, 4, 4, 4, "StoneMine", 16, new Guid("25fd995d-96fd-41d6-b6be-900c82c391f8"), new TimeSpan(2, 20, 16, 0, 0) },
                    { new Guid("1e7c6a79-99c8-4b78-84f8-9f65e75444ba"), 4, 4, 4, 4, 4, "GoldMine", 16, new Guid("94411122-cd71-4109-bcdd-2b7cfe670cf6"), new TimeSpan(2, 20, 16, 0, 0) },
                    { new Guid("38063ae8-17c8-49af-8066-eb68d628ab51"), 4, 4, 4, 4, 4, "Tower", 16, new Guid("dbe552f8-63ec-4b8a-bdcf-0e6cbaf78ee3"), new TimeSpan(2, 20, 16, 0, 0) },
                    { new Guid("89b68ea8-56f0-43b4-891a-f9fdc14ff5a1"), 4, 4, 4, 4, 4, "Wall", 16, new Guid("084ee031-3131-4289-9fbf-42216cd17fcd"), new TimeSpan(2, 20, 16, 0, 0) },
                    { new Guid("9f5a3744-9070-4a83-9fa6-821527bf3f9d"), 4, 4, 4, 4, 4, "MetalMine", 16, new Guid("ebff94c5-461d-4fd2-9eff-f4602105a1df"), new TimeSpan(2, 20, 16, 0, 0) },
                    { new Guid("a3ec3ad8-aeab-4d98-890f-b5bb49c2882a"), 4, 4, 4, 4, 4, "Lumbermill", 16, new Guid("5e03bfc4-245c-4fae-8fb8-3529b1544140"), new TimeSpan(2, 20, 16, 0, 0) },
                    { new Guid("d4ffd8e5-5ed4-49d3-a494-a4a0b69b68d5"), 4, 4, 4, 4, 4, "Farm", 16, new Guid("6e7d7738-cd20-4443-a957-6bdbdc6e1108"), new TimeSpan(2, 20, 16, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("6e7d7738-cd20-4443-a957-6bdbdc6e1108"), 17340, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("94411122-cd71-4109-bcdd-2b7cfe670cf6"), 17340, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("5e03bfc4-245c-4fae-8fb8-3529b1544140"), 17340, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("ebff94c5-461d-4fd2-9eff-f4602105a1df"), 17340, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("25fd995d-96fd-41d6-b6be-900c82c391f8"), 17340, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("dbe552f8-63ec-4b8a-bdcf-0e6cbaf78ee3"), 7225, 2890, 1445, 1445, 1445, 1445, 1445, 289, 17340, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("084ee031-3131-4289-9fbf-42216cd17fcd"), 7225, 1445, 1445, 1445, 1445, 1445, 289, 17340, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("1ec79ca5-f0cf-4996-8d5c-7045259e0ba5"), 27, 27, 27, 27, 27, "Wall", 15, new Guid("89b68ea8-56f0-43b4-891a-f9fdc14ff5a1"), new TimeSpan(2, 8, 15, 0, 0) },
                    { new Guid("6a6d6df8-b869-42fb-bec9-b7b411fd2d4d"), 27, 27, 27, 27, 27, "Lumbermill", 15, new Guid("a3ec3ad8-aeab-4d98-890f-b5bb49c2882a"), new TimeSpan(2, 8, 15, 0, 0) },
                    { new Guid("7a642e7e-c875-49ab-a5e4-48f13a038f04"), 27, 27, 27, 27, 27, "MetalMine", 15, new Guid("9f5a3744-9070-4a83-9fa6-821527bf3f9d"), new TimeSpan(2, 8, 15, 0, 0) },
                    { new Guid("9a44a6f0-8f79-4eff-9e9a-97ffb262804a"), 27, 27, 27, 27, 27, "GoldMine", 15, new Guid("1e7c6a79-99c8-4b78-84f8-9f65e75444ba"), new TimeSpan(2, 8, 15, 0, 0) },
                    { new Guid("add69fc0-caff-42f8-833f-ceea80f533a5"), 27, 27, 27, 27, 27, "StoneMine", 15, new Guid("169fc59e-fde7-43da-8926-c24977f1f520"), new TimeSpan(2, 8, 15, 0, 0) },
                    { new Guid("dc6a35dd-4731-47b0-9090-b09777a76c15"), 27, 27, 27, 27, 27, "Farm", 15, new Guid("d4ffd8e5-5ed4-49d3-a494-a4a0b69b68d5"), new TimeSpan(2, 8, 15, 0, 0) },
                    { new Guid("ebcbfd7b-53d6-4771-9917-622b71f43418"), 27, 27, 27, 27, 27, "Tower", 15, new Guid("38063ae8-17c8-49af-8066-eb68d628ab51"), new TimeSpan(2, 8, 15, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("d4ffd8e5-5ed4-49d3-a494-a4a0b69b68d5"), 15360, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("1e7c6a79-99c8-4b78-84f8-9f65e75444ba"), 15360, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a3ec3ad8-aeab-4d98-890f-b5bb49c2882a"), 15360, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("9f5a3744-9070-4a83-9fa6-821527bf3f9d"), 15360, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("169fc59e-fde7-43da-8926-c24977f1f520"), 15360, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("38063ae8-17c8-49af-8066-eb68d628ab51"), 6400, 2560, 1280, 1280, 1280, 1280, 1280, 256, 15360, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("89b68ea8-56f0-43b4-891a-f9fdc14ff5a1"), 6400, 1280, 1280, 1280, 1280, 1280, 256, 15360, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("0a913428-2e16-4945-97ae-5715f2ce030e"), 26, 26, 26, 26, 26, "StoneMine", 14, new Guid("add69fc0-caff-42f8-833f-ceea80f533a5"), new TimeSpan(1, 21, 44, 0, 0) },
                    { new Guid("379e5990-df49-4fe7-b4cd-156bb3f0716c"), 26, 26, 26, 26, 26, "Tower", 14, new Guid("ebcbfd7b-53d6-4771-9917-622b71f43418"), new TimeSpan(1, 21, 44, 0, 0) },
                    { new Guid("406aa7aa-ad73-4c14-b144-6294607ecb6d"), 26, 26, 26, 26, 26, "Farm", 14, new Guid("dc6a35dd-4731-47b0-9090-b09777a76c15"), new TimeSpan(1, 21, 44, 0, 0) },
                    { new Guid("46bbe45a-3bef-4121-9220-b991bf378189"), 26, 26, 26, 26, 26, "Wall", 14, new Guid("1ec79ca5-f0cf-4996-8d5c-7045259e0ba5"), new TimeSpan(1, 21, 44, 0, 0) },
                    { new Guid("6629022e-fdaa-4026-a2d2-66c33297ca39"), 26, 26, 26, 26, 26, "MetalMine", 14, new Guid("7a642e7e-c875-49ab-a5e4-48f13a038f04"), new TimeSpan(1, 21, 44, 0, 0) },
                    { new Guid("8429bb7c-1673-4593-95e9-546838064bea"), 26, 26, 26, 26, 26, "GoldMine", 14, new Guid("9a44a6f0-8f79-4eff-9e9a-97ffb262804a"), new TimeSpan(1, 21, 44, 0, 0) },
                    { new Guid("f6d7e2f6-6f74-46d6-9ed7-ce31c944d198"), 26, 26, 26, 26, 26, "Lumbermill", 14, new Guid("6a6d6df8-b869-42fb-bec9-b7b411fd2d4d"), new TimeSpan(1, 21, 44, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("dc6a35dd-4731-47b0-9090-b09777a76c15"), 13500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("9a44a6f0-8f79-4eff-9e9a-97ffb262804a"), 13500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("6a6d6df8-b869-42fb-bec9-b7b411fd2d4d"), 13500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("7a642e7e-c875-49ab-a5e4-48f13a038f04"), 13500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("add69fc0-caff-42f8-833f-ceea80f533a5"), 13500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("ebcbfd7b-53d6-4771-9917-622b71f43418"), 5625, 2250, 1125, 1125, 1125, 1125, 1125, 225, 13500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("1ec79ca5-f0cf-4996-8d5c-7045259e0ba5"), 5625, 1125, 1125, 1125, 1125, 1125, 225, 13500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("033ac0d1-fd86-4ea3-a160-49d05c896e3d"), 25, 25, 25, 25, 25, "StoneMine", 13, new Guid("0a913428-2e16-4945-97ae-5715f2ce030e"), new TimeSpan(1, 12, 37, 0, 0) },
                    { new Guid("30a90be5-edef-4707-957b-606a985c0acc"), 25, 25, 25, 25, 25, "Farm", 13, new Guid("406aa7aa-ad73-4c14-b144-6294607ecb6d"), new TimeSpan(1, 12, 37, 0, 0) },
                    { new Guid("32181e33-3c75-40d3-ae3c-720c52fad786"), 25, 25, 25, 25, 25, "MetalMine", 13, new Guid("6629022e-fdaa-4026-a2d2-66c33297ca39"), new TimeSpan(1, 12, 37, 0, 0) },
                    { new Guid("a8b961d2-fe4f-4b8c-9578-8c0bb9b32a7d"), 25, 25, 25, 25, 25, "Wall", 13, new Guid("46bbe45a-3bef-4121-9220-b991bf378189"), new TimeSpan(1, 12, 37, 0, 0) },
                    { new Guid("c10c6337-a5e5-4f65-9b78-a008b3696744"), 25, 25, 25, 25, 25, "Lumbermill", 13, new Guid("f6d7e2f6-6f74-46d6-9ed7-ce31c944d198"), new TimeSpan(1, 12, 37, 0, 0) },
                    { new Guid("c1297f98-a911-4c2c-9eae-ab0892b1afe4"), 25, 25, 25, 25, 25, "GoldMine", 13, new Guid("8429bb7c-1673-4593-95e9-546838064bea"), new TimeSpan(1, 12, 37, 0, 0) },
                    { new Guid("f8e06239-8d0e-4907-a90c-e40c5f6b5c34"), 25, 25, 25, 25, 25, "Tower", 13, new Guid("379e5990-df49-4fe7-b4cd-156bb3f0716c"), new TimeSpan(1, 12, 37, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("406aa7aa-ad73-4c14-b144-6294607ecb6d"), 11760, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("8429bb7c-1673-4593-95e9-546838064bea"), 11760, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("f6d7e2f6-6f74-46d6-9ed7-ce31c944d198"), 11760, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("6629022e-fdaa-4026-a2d2-66c33297ca39"), 11760, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("0a913428-2e16-4945-97ae-5715f2ce030e"), 11760, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("379e5990-df49-4fe7-b4cd-156bb3f0716c"), 4900, 1960, 980, 980, 980, 980, 980, 196, 11760, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("46bbe45a-3bef-4121-9220-b991bf378189"), 4900, 980, 980, 980, 980, 980, 196, 11760, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("0259c4b3-bc36-4187-a793-2cecd88d0f75"), 24, 24, 24, 24, 24, "Wall", 12, new Guid("a8b961d2-fe4f-4b8c-9578-8c0bb9b32a7d"), new TimeSpan(1, 4, 48, 0, 0) },
                    { new Guid("02f43c31-448e-4154-9625-5cd00f29ce8b"), 24, 24, 24, 24, 24, "MetalMine", 12, new Guid("32181e33-3c75-40d3-ae3c-720c52fad786"), new TimeSpan(1, 4, 48, 0, 0) },
                    { new Guid("54ca1260-d4ec-4a95-951d-b158c110e535"), 24, 24, 24, 24, 24, "Farm", 12, new Guid("30a90be5-edef-4707-957b-606a985c0acc"), new TimeSpan(1, 4, 48, 0, 0) },
                    { new Guid("68351e6e-2831-47ae-aceb-c220002eb56c"), 24, 24, 24, 24, 24, "Lumbermill", 12, new Guid("c10c6337-a5e5-4f65-9b78-a008b3696744"), new TimeSpan(1, 4, 48, 0, 0) },
                    { new Guid("ba4359b9-965c-4e67-9ed3-d6df19dc3a6e"), 24, 24, 24, 24, 24, "StoneMine", 12, new Guid("033ac0d1-fd86-4ea3-a160-49d05c896e3d"), new TimeSpan(1, 4, 48, 0, 0) },
                    { new Guid("cdfa96e2-eb80-4a71-9b07-d587d1141583"), 24, 24, 24, 24, 24, "GoldMine", 12, new Guid("c1297f98-a911-4c2c-9eae-ab0892b1afe4"), new TimeSpan(1, 4, 48, 0, 0) },
                    { new Guid("e287c59c-25cc-4af5-b5fe-15bea97f6b29"), 24, 24, 24, 24, 24, "Tower", 12, new Guid("f8e06239-8d0e-4907-a90c-e40c5f6b5c34"), new TimeSpan(1, 4, 48, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("30a90be5-edef-4707-957b-606a985c0acc"), 10140, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("c1297f98-a911-4c2c-9eae-ab0892b1afe4"), 10140, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("c10c6337-a5e5-4f65-9b78-a008b3696744"), 10140, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("32181e33-3c75-40d3-ae3c-720c52fad786"), 10140, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("033ac0d1-fd86-4ea3-a160-49d05c896e3d"), 10140, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("f8e06239-8d0e-4907-a90c-e40c5f6b5c34"), 4225, 1690, 845, 845, 845, 845, 845, 169, 10140, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("a8b961d2-fe4f-4b8c-9578-8c0bb9b32a7d"), 4225, 845, 845, 845, 845, 845, 169, 10140, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("00ec9baf-a4a3-43ee-8d2b-468dfdf367c9"), 31, 31, 31, 31, 31, "Tower", 11, new Guid("e287c59c-25cc-4af5-b5fe-15bea97f6b29"), new TimeSpan(0, 22, 11, 0, 0) },
                    { new Guid("52d3767a-f084-4e38-aa30-9dc6a52575c3"), 31, 31, 31, 31, 31, "MetalMine", 11, new Guid("02f43c31-448e-4154-9625-5cd00f29ce8b"), new TimeSpan(0, 22, 11, 0, 0) },
                    { new Guid("57ad6e92-278b-4834-925a-4487869bd3d5"), 31, 31, 31, 31, 31, "Lumbermill", 11, new Guid("68351e6e-2831-47ae-aceb-c220002eb56c"), new TimeSpan(0, 22, 11, 0, 0) },
                    { new Guid("9c0d6e3d-1744-45b8-bc3e-cf6e15121ef9"), 31, 31, 31, 31, 31, "Wall", 11, new Guid("0259c4b3-bc36-4187-a793-2cecd88d0f75"), new TimeSpan(0, 22, 11, 0, 0) },
                    { new Guid("abfabe86-4d4a-4217-b056-c1828f506fd8"), 31, 31, 31, 31, 31, "GoldMine", 11, new Guid("cdfa96e2-eb80-4a71-9b07-d587d1141583"), new TimeSpan(0, 22, 11, 0, 0) },
                    { new Guid("ae191e6e-bd74-43b9-8c75-e32c948ea1ff"), 31, 31, 31, 31, 31, "Farm", 11, new Guid("54ca1260-d4ec-4a95-951d-b158c110e535"), new TimeSpan(0, 22, 11, 0, 0) },
                    { new Guid("c0d9a43a-14c7-49f2-b11f-92cc83411f17"), 31, 31, 31, 31, 31, "StoneMine", 11, new Guid("ba4359b9-965c-4e67-9ed3-d6df19dc3a6e"), new TimeSpan(0, 22, 11, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("54ca1260-d4ec-4a95-951d-b158c110e535"), 8640, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("cdfa96e2-eb80-4a71-9b07-d587d1141583"), 8640, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("68351e6e-2831-47ae-aceb-c220002eb56c"), 8640, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("02f43c31-448e-4154-9625-5cd00f29ce8b"), 8640, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("ba4359b9-965c-4e67-9ed3-d6df19dc3a6e"), 8640, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("e287c59c-25cc-4af5-b5fe-15bea97f6b29"), 3600, 1440, 720, 720, 720, 720, 720, 144, 8640, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("0259c4b3-bc36-4187-a793-2cecd88d0f75"), 3600, 720, 720, 720, 720, 720, 144, 8640, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("47bcd37b-195e-4fa7-bcf5-7f2ec6f2b74c"), 30, 30, 30, 30, 30, "Tower", 10, new Guid("00ec9baf-a4a3-43ee-8d2b-468dfdf367c9"), new TimeSpan(0, 16, 40, 0, 0) },
                    { new Guid("544905d6-289a-4973-8aca-acd5625aa72e"), 30, 30, 30, 30, 30, "Lumbermill", 10, new Guid("57ad6e92-278b-4834-925a-4487869bd3d5"), new TimeSpan(0, 16, 40, 0, 0) },
                    { new Guid("69a7c55b-ff93-43ff-991b-28716bbb8eb2"), 30, 30, 30, 30, 30, "GoldMine", 10, new Guid("abfabe86-4d4a-4217-b056-c1828f506fd8"), new TimeSpan(0, 16, 40, 0, 0) },
                    { new Guid("9140f7bb-5bf9-4147-8cd6-1e8a467e3ab3"), 30, 30, 30, 30, 30, "Wall", 10, new Guid("9c0d6e3d-1744-45b8-bc3e-cf6e15121ef9"), new TimeSpan(0, 16, 40, 0, 0) },
                    { new Guid("9a9d3b9a-1f3c-44cc-ac70-4086fd151469"), 30, 30, 30, 30, 30, "StoneMine", 10, new Guid("c0d9a43a-14c7-49f2-b11f-92cc83411f17"), new TimeSpan(0, 16, 40, 0, 0) },
                    { new Guid("a6cf1133-7581-47ee-b3da-c9c724708aac"), 30, 30, 30, 30, 30, "Farm", 10, new Guid("ae191e6e-bd74-43b9-8c75-e32c948ea1ff"), new TimeSpan(0, 16, 40, 0, 0) },
                    { new Guid("f345765d-b596-46a1-bbce-2224e0d6fa98"), 30, 30, 30, 30, 30, "MetalMine", 10, new Guid("52d3767a-f084-4e38-aa30-9dc6a52575c3"), new TimeSpan(0, 16, 40, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("ae191e6e-bd74-43b9-8c75-e32c948ea1ff"), 7260, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("abfabe86-4d4a-4217-b056-c1828f506fd8"), 7260, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("57ad6e92-278b-4834-925a-4487869bd3d5"), 7260, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("52d3767a-f084-4e38-aa30-9dc6a52575c3"), 7260, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("c0d9a43a-14c7-49f2-b11f-92cc83411f17"), 7260, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("00ec9baf-a4a3-43ee-8d2b-468dfdf367c9"), 3025, 1210, 605, 605, 605, 605, 605, 121, 7260, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("9c0d6e3d-1744-45b8-bc3e-cf6e15121ef9"), 3025, 605, 605, 605, 605, 605, 121, 7260, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("102744c5-27a6-4d23-a538-1a6f742b953b"), 29, 29, 29, 29, 29, "Wall", 9, new Guid("9140f7bb-5bf9-4147-8cd6-1e8a467e3ab3"), new TimeSpan(0, 12, 9, 0, 0) },
                    { new Guid("7ad72cd3-2821-4ced-b6bb-81e1809bc009"), 29, 29, 29, 29, 29, "MetalMine", 9, new Guid("f345765d-b596-46a1-bbce-2224e0d6fa98"), new TimeSpan(0, 12, 9, 0, 0) },
                    { new Guid("a0d0a6d4-cec8-4a3f-84b9-ff198e0a918f"), 29, 29, 29, 29, 29, "GoldMine", 9, new Guid("69a7c55b-ff93-43ff-991b-28716bbb8eb2"), new TimeSpan(0, 12, 9, 0, 0) },
                    { new Guid("b500aebc-c279-4f11-b6cd-e8cc78645e3a"), 29, 29, 29, 29, 29, "Farm", 9, new Guid("a6cf1133-7581-47ee-b3da-c9c724708aac"), new TimeSpan(0, 12, 9, 0, 0) },
                    { new Guid("df44ccbc-2831-4847-8d5c-741b7db4dcfc"), 29, 29, 29, 29, 29, "Tower", 9, new Guid("47bcd37b-195e-4fa7-bcf5-7f2ec6f2b74c"), new TimeSpan(0, 12, 9, 0, 0) },
                    { new Guid("eb9e83e4-fa9b-45de-a9b3-76a8729a7347"), 29, 29, 29, 29, 29, "Lumbermill", 9, new Guid("544905d6-289a-4973-8aca-acd5625aa72e"), new TimeSpan(0, 12, 9, 0, 0) },
                    { new Guid("f352c9fc-baaf-4a75-8030-32dffc9c9512"), 29, 29, 29, 29, 29, "StoneMine", 9, new Guid("9a9d3b9a-1f3c-44cc-ac70-4086fd151469"), new TimeSpan(0, 12, 9, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a6cf1133-7581-47ee-b3da-c9c724708aac"), 6000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("69a7c55b-ff93-43ff-991b-28716bbb8eb2"), 6000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("544905d6-289a-4973-8aca-acd5625aa72e"), 6000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("f345765d-b596-46a1-bbce-2224e0d6fa98"), 6000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("9a9d3b9a-1f3c-44cc-ac70-4086fd151469"), 6000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("47bcd37b-195e-4fa7-bcf5-7f2ec6f2b74c"), 2500, 1000, 500, 500, 500, 500, 500, 100, 6000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("9140f7bb-5bf9-4147-8cd6-1e8a467e3ab3"), 2500, 500, 500, 500, 500, 500, 100, 6000, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("1d67631c-a3d8-4548-a9f2-5405040c8e39"), 28, 28, 28, 28, 28, "StoneMine", 8, new Guid("f352c9fc-baaf-4a75-8030-32dffc9c9512"), new TimeSpan(0, 8, 32, 0, 0) },
                    { new Guid("2ecb2335-eb9f-4e7d-9c7e-9cc0044bddd2"), 28, 28, 28, 28, 28, "MetalMine", 8, new Guid("7ad72cd3-2821-4ced-b6bb-81e1809bc009"), new TimeSpan(0, 8, 32, 0, 0) },
                    { new Guid("3357a6dc-60a1-46e4-a046-01339f63ddb0"), 28, 28, 28, 28, 28, "Farm", 8, new Guid("b500aebc-c279-4f11-b6cd-e8cc78645e3a"), new TimeSpan(0, 8, 32, 0, 0) },
                    { new Guid("3bdd802b-4d97-4558-8790-8033c7c37708"), 28, 28, 28, 28, 28, "Wall", 8, new Guid("102744c5-27a6-4d23-a538-1a6f742b953b"), new TimeSpan(0, 8, 32, 0, 0) },
                    { new Guid("46d59fef-f121-42c4-9067-5e6c94cfd438"), 28, 28, 28, 28, 28, "Tower", 8, new Guid("df44ccbc-2831-4847-8d5c-741b7db4dcfc"), new TimeSpan(0, 8, 32, 0, 0) },
                    { new Guid("6d76dca7-19b1-4fb4-bfed-74d157959838"), 28, 28, 28, 28, 28, "GoldMine", 8, new Guid("a0d0a6d4-cec8-4a3f-84b9-ff198e0a918f"), new TimeSpan(0, 8, 32, 0, 0) },
                    { new Guid("b64a4404-4c25-405a-a447-e64ca8d98c50"), 28, 28, 28, 28, 28, "Lumbermill", 8, new Guid("eb9e83e4-fa9b-45de-a9b3-76a8729a7347"), new TimeSpan(0, 8, 32, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("b500aebc-c279-4f11-b6cd-e8cc78645e3a"), 4860, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a0d0a6d4-cec8-4a3f-84b9-ff198e0a918f"), 4860, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("eb9e83e4-fa9b-45de-a9b3-76a8729a7347"), 4860, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("7ad72cd3-2821-4ced-b6bb-81e1809bc009"), 4860, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("f352c9fc-baaf-4a75-8030-32dffc9c9512"), 4860, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("df44ccbc-2831-4847-8d5c-741b7db4dcfc"), 2025, 810, 405, 405, 405, 405, 405, 81, 4860, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("102744c5-27a6-4d23-a538-1a6f742b953b"), 2025, 405, 405, 405, 405, 405, 81, 4860, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("3f5d975e-196a-48c9-9a4d-78a8c121008a"), 19, 19, 19, 19, 19, "Lumbermill", 7, new Guid("b64a4404-4c25-405a-a447-e64ca8d98c50"), new TimeSpan(0, 5, 43, 0, 0) },
                    { new Guid("45512184-78ad-4d36-bd94-ab01bef6db7d"), 19, 19, 19, 19, 19, "Farm", 7, new Guid("3357a6dc-60a1-46e4-a046-01339f63ddb0"), new TimeSpan(0, 5, 43, 0, 0) },
                    { new Guid("7187e6b4-bccd-4eb6-8014-afd89d83d307"), 19, 19, 19, 19, 19, "MetalMine", 7, new Guid("2ecb2335-eb9f-4e7d-9c7e-9cc0044bddd2"), new TimeSpan(0, 5, 43, 0, 0) },
                    { new Guid("79b78f71-c992-44c1-a51a-9c009c816f8a"), 19, 19, 19, 19, 19, "Tower", 7, new Guid("46d59fef-f121-42c4-9067-5e6c94cfd438"), new TimeSpan(0, 5, 43, 0, 0) },
                    { new Guid("8ccadf79-bea7-4224-83d9-6336e54431ab"), 19, 19, 19, 19, 19, "Wall", 7, new Guid("3bdd802b-4d97-4558-8790-8033c7c37708"), new TimeSpan(0, 5, 43, 0, 0) },
                    { new Guid("a44e3e2f-4f00-40d2-8be6-49637631e737"), 19, 19, 19, 19, 19, "StoneMine", 7, new Guid("1d67631c-a3d8-4548-a9f2-5405040c8e39"), new TimeSpan(0, 5, 43, 0, 0) },
                    { new Guid("aec0c807-e58c-4a5f-9c35-87fe6a4f537c"), 19, 19, 19, 19, 19, "GoldMine", 7, new Guid("6d76dca7-19b1-4fb4-bfed-74d157959838"), new TimeSpan(0, 5, 43, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("3357a6dc-60a1-46e4-a046-01339f63ddb0"), 3840, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("6d76dca7-19b1-4fb4-bfed-74d157959838"), 3840, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("b64a4404-4c25-405a-a447-e64ca8d98c50"), 3840, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("2ecb2335-eb9f-4e7d-9c7e-9cc0044bddd2"), 3840, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("1d67631c-a3d8-4548-a9f2-5405040c8e39"), 3840, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("46d59fef-f121-42c4-9067-5e6c94cfd438"), 1600, 640, 320, 320, 320, 320, 320, 64, 3840, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("3bdd802b-4d97-4558-8790-8033c7c37708"), 1600, 320, 320, 320, 320, 320, 64, 3840, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("357b2c1a-4765-484e-ad35-82650482fbb6"), 18, 18, 18, 18, 18, "GoldMine", 6, new Guid("aec0c807-e58c-4a5f-9c35-87fe6a4f537c"), new TimeSpan(0, 3, 36, 0, 0) },
                    { new Guid("6360c3bc-5b68-4305-8339-43abf9a561f5"), 18, 18, 18, 18, 18, "MetalMine", 6, new Guid("7187e6b4-bccd-4eb6-8014-afd89d83d307"), new TimeSpan(0, 3, 36, 0, 0) },
                    { new Guid("75d03d1f-59bd-4648-b739-944214869388"), 18, 18, 18, 18, 18, "Tower", 6, new Guid("79b78f71-c992-44c1-a51a-9c009c816f8a"), new TimeSpan(0, 3, 36, 0, 0) },
                    { new Guid("ae7ced42-d8db-43e4-8b68-819f2166fb7e"), 18, 18, 18, 18, 18, "Farm", 6, new Guid("45512184-78ad-4d36-bd94-ab01bef6db7d"), new TimeSpan(0, 3, 36, 0, 0) },
                    { new Guid("b541c942-7c2d-452b-b6a8-5bb4dad009f2"), 18, 18, 18, 18, 18, "Lumbermill", 6, new Guid("3f5d975e-196a-48c9-9a4d-78a8c121008a"), new TimeSpan(0, 3, 36, 0, 0) },
                    { new Guid("cd105217-2857-4ec8-9502-de6456c88a03"), 18, 18, 18, 18, 18, "StoneMine", 6, new Guid("a44e3e2f-4f00-40d2-8be6-49637631e737"), new TimeSpan(0, 3, 36, 0, 0) },
                    { new Guid("f34484d8-89ef-47a6-b004-3da8f1e7e44d"), 18, 18, 18, 18, 18, "Wall", 6, new Guid("8ccadf79-bea7-4224-83d9-6336e54431ab"), new TimeSpan(0, 3, 36, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("45512184-78ad-4d36-bd94-ab01bef6db7d"), 2940, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("aec0c807-e58c-4a5f-9c35-87fe6a4f537c"), 2940, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("3f5d975e-196a-48c9-9a4d-78a8c121008a"), 2940, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("7187e6b4-bccd-4eb6-8014-afd89d83d307"), 2940, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a44e3e2f-4f00-40d2-8be6-49637631e737"), 2940, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("79b78f71-c992-44c1-a51a-9c009c816f8a"), 1225, 490, 245, 245, 245, 245, 245, 49, 2940, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("8ccadf79-bea7-4224-83d9-6336e54431ab"), 1225, 245, 245, 245, 245, 245, 49, 2940, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("1c55b0ec-7610-4810-b5dc-c0c1daaafb6d"), 17, 17, 17, 17, 17, "Tower", 5, new Guid("75d03d1f-59bd-4648-b739-944214869388"), new TimeSpan(0, 2, 5, 0, 0) },
                    { new Guid("46d0d679-aa8a-441d-9508-8cd4344f26a8"), 17, 17, 17, 17, 17, "Farm", 5, new Guid("ae7ced42-d8db-43e4-8b68-819f2166fb7e"), new TimeSpan(0, 2, 5, 0, 0) },
                    { new Guid("648c25e6-c591-4006-abe0-e20581d7d553"), 17, 17, 17, 17, 17, "MetalMine", 5, new Guid("6360c3bc-5b68-4305-8339-43abf9a561f5"), new TimeSpan(0, 2, 5, 0, 0) },
                    { new Guid("709c0106-d06c-411a-9eec-352f42b3990c"), 17, 17, 17, 17, 17, "StoneMine", 5, new Guid("cd105217-2857-4ec8-9502-de6456c88a03"), new TimeSpan(0, 2, 5, 0, 0) },
                    { new Guid("9207c336-02f6-4e63-8a48-bbba0d3dac0a"), 17, 17, 17, 17, 17, "Lumbermill", 5, new Guid("b541c942-7c2d-452b-b6a8-5bb4dad009f2"), new TimeSpan(0, 2, 5, 0, 0) },
                    { new Guid("a8a48170-db0b-4046-a7ac-fee0c99e7cfb"), 17, 17, 17, 17, 17, "GoldMine", 5, new Guid("357b2c1a-4765-484e-ad35-82650482fbb6"), new TimeSpan(0, 2, 5, 0, 0) },
                    { new Guid("e12f9f9b-f633-4c01-b012-3a5ea226cd0b"), 17, 17, 17, 17, 17, "Wall", 5, new Guid("f34484d8-89ef-47a6-b004-3da8f1e7e44d"), new TimeSpan(0, 2, 5, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("ae7ced42-d8db-43e4-8b68-819f2166fb7e"), 2160, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("357b2c1a-4765-484e-ad35-82650482fbb6"), 2160, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("b541c942-7c2d-452b-b6a8-5bb4dad009f2"), 2160, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("6360c3bc-5b68-4305-8339-43abf9a561f5"), 2160, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("cd105217-2857-4ec8-9502-de6456c88a03"), 2160, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("75d03d1f-59bd-4648-b739-944214869388"), 900, 360, 180, 180, 180, 180, 180, 36, 2160, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("f34484d8-89ef-47a6-b004-3da8f1e7e44d"), 900, 180, 180, 180, 180, 180, 36, 2160, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("00c44ffa-0a08-4a24-aa27-b49726b0b6b9"), 16, 16, 16, 16, 16, "MetalMine", 4, new Guid("648c25e6-c591-4006-abe0-e20581d7d553"), new TimeSpan(0, 1, 4, 0, 0) },
                    { new Guid("166a72d9-2fff-4a58-845a-9ab4f0bcd06b"), 16, 16, 16, 16, 16, "Wall", 4, new Guid("e12f9f9b-f633-4c01-b012-3a5ea226cd0b"), new TimeSpan(0, 1, 4, 0, 0) },
                    { new Guid("4e2d47e5-cc3b-418e-a234-8f77a8986fc3"), 16, 16, 16, 16, 16, "Farm", 4, new Guid("46d0d679-aa8a-441d-9508-8cd4344f26a8"), new TimeSpan(0, 1, 4, 0, 0) },
                    { new Guid("6d883cc5-8cea-4a89-aec9-ef3248d14b01"), 16, 16, 16, 16, 16, "Tower", 4, new Guid("1c55b0ec-7610-4810-b5dc-c0c1daaafb6d"), new TimeSpan(0, 1, 4, 0, 0) },
                    { new Guid("7e67e97f-6496-4df3-9af2-9e8e6ffb9280"), 16, 16, 16, 16, 16, "Lumbermill", 4, new Guid("9207c336-02f6-4e63-8a48-bbba0d3dac0a"), new TimeSpan(0, 1, 4, 0, 0) },
                    { new Guid("f6e3833b-724f-4ce0-85ed-acbab08f428b"), 16, 16, 16, 16, 16, "GoldMine", 4, new Guid("a8a48170-db0b-4046-a7ac-fee0c99e7cfb"), new TimeSpan(0, 1, 4, 0, 0) },
                    { new Guid("ff6317c4-7ff1-48b9-8328-bbe36b3fb8d0"), 16, 16, 16, 16, 16, "StoneMine", 4, new Guid("709c0106-d06c-411a-9eec-352f42b3990c"), new TimeSpan(0, 1, 4, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("46d0d679-aa8a-441d-9508-8cd4344f26a8"), 1500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a8a48170-db0b-4046-a7ac-fee0c99e7cfb"), 1500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("9207c336-02f6-4e63-8a48-bbba0d3dac0a"), 1500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("648c25e6-c591-4006-abe0-e20581d7d553"), 1500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("709c0106-d06c-411a-9eec-352f42b3990c"), 1500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("1c55b0ec-7610-4810-b5dc-c0c1daaafb6d"), 625, 250, 125, 125, 125, 125, 125, 25, 1500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("e12f9f9b-f633-4c01-b012-3a5ea226cd0b"), 625, 125, 125, 125, 125, 125, 25, 1500, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("08958648-1b1e-45ca-bd42-aa7ff32778b9"), 23, 23, 23, 23, 23, "Wall", 3, new Guid("166a72d9-2fff-4a58-845a-9ab4f0bcd06b"), new TimeSpan(0, 0, 27, 0, 0) },
                    { new Guid("191f8d84-4a55-404b-b790-33e4dcd24077"), 23, 23, 23, 23, 23, "Lumbermill", 3, new Guid("7e67e97f-6496-4df3-9af2-9e8e6ffb9280"), new TimeSpan(0, 0, 27, 0, 0) },
                    { new Guid("658d6d98-16ad-4102-91fa-d24c58def4d1"), 23, 23, 23, 23, 23, "Tower", 3, new Guid("6d883cc5-8cea-4a89-aec9-ef3248d14b01"), new TimeSpan(0, 0, 27, 0, 0) },
                    { new Guid("a1ea1fc6-c08b-44a8-827f-f58b5ac8ef3d"), 23, 23, 23, 23, 23, "MetalMine", 3, new Guid("00c44ffa-0a08-4a24-aa27-b49726b0b6b9"), new TimeSpan(0, 0, 27, 0, 0) },
                    { new Guid("a6000121-008d-43e8-bd19-d2e63d24b81c"), 23, 23, 23, 23, 23, "StoneMine", 3, new Guid("ff6317c4-7ff1-48b9-8328-bbe36b3fb8d0"), new TimeSpan(0, 0, 27, 0, 0) },
                    { new Guid("e5e359aa-9dec-4979-bc0c-f2064da24997"), 23, 23, 23, 23, 23, "Farm", 3, new Guid("4e2d47e5-cc3b-418e-a234-8f77a8986fc3"), new TimeSpan(0, 0, 27, 0, 0) },
                    { new Guid("eac9bb21-2f91-4406-b5bd-a2cb54b3c5c0"), 23, 23, 23, 23, 23, "GoldMine", 3, new Guid("f6e3833b-724f-4ce0-85ed-acbab08f428b"), new TimeSpan(0, 0, 27, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("4e2d47e5-cc3b-418e-a234-8f77a8986fc3"), 960, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("f6e3833b-724f-4ce0-85ed-acbab08f428b"), 960, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("7e67e97f-6496-4df3-9af2-9e8e6ffb9280"), 960, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("00c44ffa-0a08-4a24-aa27-b49726b0b6b9"), 960, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("ff6317c4-7ff1-48b9-8328-bbe36b3fb8d0"), 960, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("6d883cc5-8cea-4a89-aec9-ef3248d14b01"), 400, 160, 80, 80, 80, 80, 80, 16, 960, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("166a72d9-2fff-4a58-845a-9ab4f0bcd06b"), 400, 80, 80, 80, 80, 80, 16, 960, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("2684a393-a8d3-4daf-8a5e-d7723dccd383"), 22, 22, 22, 22, 22, "Wall", 2, new Guid("08958648-1b1e-45ca-bd42-aa7ff32778b9"), new TimeSpan(0, 0, 8, 0, 0) },
                    { new Guid("5732eeec-f58f-4c46-8a9c-aee2e9b2ac7d"), 22, 22, 22, 22, 22, "StoneMine", 2, new Guid("a6000121-008d-43e8-bd19-d2e63d24b81c"), new TimeSpan(0, 0, 8, 0, 0) },
                    { new Guid("6f7bbf8a-4b95-4f41-aa92-1dd31e57014c"), 22, 22, 22, 22, 22, "Tower", 2, new Guid("658d6d98-16ad-4102-91fa-d24c58def4d1"), new TimeSpan(0, 0, 8, 0, 0) },
                    { new Guid("99c663b4-b322-4493-b241-94fd9c45147c"), 22, 22, 22, 22, 22, "Lumbermill", 2, new Guid("191f8d84-4a55-404b-b790-33e4dcd24077"), new TimeSpan(0, 0, 8, 0, 0) },
                    { new Guid("a409d292-7cf4-48ed-9d9e-96f22b1acde5"), 22, 22, 22, 22, 22, "MetalMine", 2, new Guid("a1ea1fc6-c08b-44a8-827f-f58b5ac8ef3d"), new TimeSpan(0, 0, 8, 0, 0) },
                    { new Guid("c1030dc7-314e-4292-8307-5d87e4ae7c55"), 22, 22, 22, 22, 22, "GoldMine", 2, new Guid("eac9bb21-2f91-4406-b5bd-a2cb54b3c5c0"), new TimeSpan(0, 0, 8, 0, 0) },
                    { new Guid("c337d2b6-d34a-49f4-8db1-1cfebefef4dc"), 22, 22, 22, 22, 22, "Farm", 2, new Guid("e5e359aa-9dec-4979-bc0c-f2064da24997"), new TimeSpan(0, 0, 8, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("e5e359aa-9dec-4979-bc0c-f2064da24997"), 540, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("eac9bb21-2f91-4406-b5bd-a2cb54b3c5c0"), 540, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("191f8d84-4a55-404b-b790-33e4dcd24077"), 540, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a1ea1fc6-c08b-44a8-827f-f58b5ac8ef3d"), 540, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a6000121-008d-43e8-bd19-d2e63d24b81c"), 540, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("658d6d98-16ad-4102-91fa-d24c58def4d1"), 225, 90, 45, 45, 45, 45, 45, 9, 540, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("08958648-1b1e-45ca-bd42-aa7ff32778b9"), 225, 45, 45, 45, 45, 45, 9, 540, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("0c09daa5-0a1a-42a5-9e4d-005a10610711"), 0, 0, 0, 0, 0, "MetalMine", 1, new Guid("a409d292-7cf4-48ed-9d9e-96f22b1acde5"), new TimeSpan(0, 0, 1, 0, 0) },
                    { new Guid("1ac03dcf-e453-43e6-bc7b-c01a5ec82760"), 0, 0, 0, 0, 0, "Lumbermill", 1, new Guid("99c663b4-b322-4493-b241-94fd9c45147c"), new TimeSpan(0, 0, 1, 0, 0) },
                    { new Guid("1cbe2263-2d1b-4c3a-8610-2327ae11d9d4"), 0, 0, 0, 0, 0, "GoldMine", 1, new Guid("c1030dc7-314e-4292-8307-5d87e4ae7c55"), new TimeSpan(0, 0, 1, 0, 0) },
                    { new Guid("4b540e35-f77f-4684-989c-e10c42881956"), 0, 0, 0, 0, 0, "StoneMine", 1, new Guid("5732eeec-f58f-4c46-8a9c-aee2e9b2ac7d"), new TimeSpan(0, 0, 1, 0, 0) },
                    { new Guid("52c0d87a-7fb3-48f7-88d5-1f9e5a06ac7d"), 0, 0, 0, 0, 0, "Wall", 1, new Guid("2684a393-a8d3-4daf-8a5e-d7723dccd383"), new TimeSpan(0, 0, 1, 0, 0) },
                    { new Guid("76d01316-e693-4a29-8164-1b3904371ac7"), 0, 0, 0, 0, 0, "Farm", 1, new Guid("c337d2b6-d34a-49f4-8db1-1cfebefef4dc"), new TimeSpan(0, 0, 1, 0, 0) },
                    { new Guid("88e35b53-867f-4f49-917e-baf1e20fc341"), 0, 0, 0, 0, 0, "Tower", 1, new Guid("6f7bbf8a-4b95-4f41-aa92-1dd31e57014c"), new TimeSpan(0, 0, 1, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("c337d2b6-d34a-49f4-8db1-1cfebefef4dc"), 240, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("c1030dc7-314e-4292-8307-5d87e4ae7c55"), 240, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("99c663b4-b322-4493-b241-94fd9c45147c"), 240, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("a409d292-7cf4-48ed-9d9e-96f22b1acde5"), 240, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("5732eeec-f58f-4c46-8a9c-aee2e9b2ac7d"), 240, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("6f7bbf8a-4b95-4f41-aa92-1dd31e57014c"), 100, 40, 20, 20, 20, 20, 20, 4, 240, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("2684a393-a8d3-4daf-8a5e-d7723dccd383"), 100, 20, 20, 20, 20, 20, 4, 240, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone", "BuildingType", "Level", "TargetId", "UpgradeDuration" },
                values: new object[,]
                {
                    { new Guid("1207c282-da8e-4ba9-8a7e-1f4f403a3fa9"), 20, 20, 20, 20, 20, "GoldMine", 0, new Guid("1cbe2263-2d1b-4c3a-8610-2327ae11d9d4"), new TimeSpan(0, 0, 0, 0, 0) },
                    { new Guid("4586f813-4966-490a-803f-7e09128f34f0"), 20, 20, 20, 20, 20, "StoneMine", 0, new Guid("4b540e35-f77f-4684-989c-e10c42881956"), new TimeSpan(0, 0, 0, 0, 0) },
                    { new Guid("c122e004-7196-4f8f-8038-50a96097467b"), 20, 20, 20, 20, 20, "Lumbermill", 0, new Guid("1ac03dcf-e453-43e6-bc7b-c01a5ec82760"), new TimeSpan(0, 0, 0, 0, 0) },
                    { new Guid("ce06429a-7b41-4437-b2f4-0c6a15a04225"), 20, 20, 20, 20, 20, "Farm", 0, new Guid("76d01316-e693-4a29-8164-1b3904371ac7"), new TimeSpan(0, 0, 0, 0, 0) },
                    { new Guid("e1dc0baf-6417-4734-bad7-ca471f5f3cdb"), 20, 20, 20, 20, 20, "Tower", 0, new Guid("88e35b53-867f-4f49-917e-baf1e20fc341"), new TimeSpan(0, 0, 0, 0, 0) },
                    { new Guid("e56e4164-0149-4913-8ded-9e8b6b67b218"), 20, 20, 20, 20, 20, "MetalMine", 0, new Guid("0c09daa5-0a1a-42a5-9e4d-005a10610711"), new TimeSpan(0, 0, 0, 0, 0) },
                    { new Guid("ea4df1fe-852e-4508-abd9-e39e7dffc713"), 20, 20, 20, 20, 20, "Wall", 0, new Guid("52c0d87a-7fb3-48f7-88d5-1f9e5a06ac7d"), new TimeSpan(0, 0, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("76d01316-e693-4a29-8164-1b3904371ac7"), 60, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("1cbe2263-2d1b-4c3a-8610-2327ae11d9d4"), 60, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("1ac03dcf-e453-43e6-bc7b-c01a5ec82760"), 60, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("0c09daa5-0a1a-42a5-9e4d-005a10610711"), 60, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("4b540e35-f77f-4684-989c-e10c42881956"), 60, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("88e35b53-867f-4f49-917e-baf1e20fc341"), 25, 10, 5, 5, 5, 5, 5, 1, 60, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("52c0d87a-7fb3-48f7-88d5-1f9e5a06ac7d"), 25, 5, 5, 5, 5, 5, 1, 60, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Farm",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("ce06429a-7b41-4437-b2f4-0c6a15a04225"), 0, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "GoldMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("1207c282-da8e-4ba9-8a7e-1f4f403a3fa9"), 0, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "LumberMill",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("c122e004-7196-4f8f-8038-50a96097467b"), 0, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "MetalMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("e56e4164-0149-4913-8ded-9e8b6b67b218"), 0, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "StoneMine",
                columns: new[] { "Id", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone" },
                values: new object[] { new Guid("4586f813-4966-490a-803f-7e09128f34f0"), 0, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tower",
                columns: new[] { "Id", "Health", "TroopCapacity", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("e1dc0baf-6417-4734-bad7-ca471f5f3cdb"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Wall",
                columns: new[] { "Id", "Health", "DefenseAgainstCavalry", "DefenseAgainstFlyingCavalry", "DefenseAgainstMage", "DefenseAgainstMeleeInfantry", "DefenseAgainstRangeInfantry", "DefenseAgainstSiegeUnit", "RepairCostFood", "RepairCostGold", "RepairCostLumber", "RepairCostMetal", "RepairCostStone" },
                values: new object[] { new Guid("ea4df1fe-852e-4508-abd9-e39e7dffc713"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Building_TargetId",
                table: "Building",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_PlayerId",
                table: "RefreshTokens",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Troop_VillageTowerId",
                table: "Troop",
                column: "VillageTowerId");

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
                name: "IX_VillageBuilding_BuildingId",
                table: "VillageBuilding",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_VillageBuilding_VillageId",
                table: "VillageBuilding",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_VillageTowers_TowerId",
                table: "VillageTowers",
                column: "TowerId");

            migrationBuilder.CreateIndex(
                name: "IX_VillageTowers_VillageId",
                table: "VillageTowers",
                column: "VillageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VillageWalls_VillageId",
                table: "VillageWalls",
                column: "VillageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VillageWalls_WallId",
                table: "VillageWalls",
                column: "WallId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Farm");

            migrationBuilder.DropTable(
                name: "GoldMine");

            migrationBuilder.DropTable(
                name: "LumberMill");

            migrationBuilder.DropTable(
                name: "MetalMine");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "StoneMine");

            migrationBuilder.DropTable(
                name: "Troop");

            migrationBuilder.DropTable(
                name: "VillageBuilding");

            migrationBuilder.DropTable(
                name: "VillageWalls");

            migrationBuilder.DropTable(
                name: "VillageTowers");

            migrationBuilder.DropTable(
                name: "Wall");

            migrationBuilder.DropTable(
                name: "Tower");

            migrationBuilder.DropTable(
                name: "Village");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Faction");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
