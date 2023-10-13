using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yooresh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceBuilding_ResourceBuilding_RequirementId",
                table: "ResourceBuilding");

            migrationBuilder.DropTable(
                name: "VillageResourceBuilding");

            migrationBuilder.DropTable(
                name: "VillageUpgradeQueue");

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("0986a9a2-d235-4a62-8ae4-1eb1a31eab18"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0"));

            migrationBuilder.DropColumn(
                name: "LastResourceChangeTime",
                table: "Village");

            migrationBuilder.RenameColumn(
                name: "RequirementId",
                table: "ResourceBuilding",
                newName: "TargetId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceBuilding_RequirementId",
                table: "ResourceBuilding",
                newName: "IX_ResourceBuilding_TargetId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastResourceGatherDate",
                table: "ResourceBuilding",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "ResourceBuilding",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "NeedBuilderForUpgrade",
                table: "ResourceBuilding",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpgradeName",
                table: "ResourceBuilding",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ResourceBuildingVillage",
                columns: table => new
                {
                    ResourceBuildingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceBuildingVillage", x => new { x.ResourceBuildingsId, x.VillageId });
                    table.ForeignKey(
                        name: "FK_ResourceBuildingVillage_ResourceBuilding_ResourceBuildingsId",
                        column: x => x.ResourceBuildingsId,
                        principalTable: "ResourceBuilding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceBuildingVillage_Village_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Village",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ResourceBuilding",
                columns: new[] { "Id", "LastResourceGatherDate", "Level", "Name", "NeedBuilderForUpgrade", "ProductionType", "TargetId", "UpgradeDuration", "UpgradeName", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone" },
                values: new object[,]
                {
                    { new Guid("46b0991f-6ec6-4c0a-9b58-2b7415417ce1"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(7751), new TimeSpan(0, 0, 0, 0, 0)), 25, "Farm 25", true, "Food", null, new TimeSpan(10, 20, 25, 0, 0), "Upgrade To Farm 26", 37500, 0, 0, 0, 0, 13, 13, 13, 13, 13 },
                    { new Guid("b8823bbc-cb6d-491b-97d9-deb69e820e27"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(9302), new TimeSpan(0, 0, 0, 0, 0)), 25, "Metal Mine 25", true, "Metal", null, new TimeSpan(10, 20, 25, 0, 0), "Upgrade To Metal Mine 26", 0, 0, 0, 37500, 0, 13, 13, 13, 13, 13 },
                    { new Guid("c568a21a-44a3-44ae-8358-998e7f02f284"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(5140), new TimeSpan(0, 0, 0, 0, 0)), 25, "Stone Mine 25", true, "Stone", null, new TimeSpan(10, 20, 25, 0, 0), "Upgrade To Stone Mine 26", 0, 0, 0, 0, 37500, 13, 13, 13, 13, 13 },
                    { new Guid("ea9c65e0-a993-42b6-a3e4-c8c48af6ba9a"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(1628), new TimeSpan(0, 0, 0, 0, 0)), 25, "Lumber Mill 25", true, "Lumber", null, new TimeSpan(10, 20, 25, 0, 0), "Upgrade To Lumber Mill 26", 0, 0, 37500, 0, 0, 13, 13, 13, 13, 13 },
                    { new Guid("7b59c255-21ec-49f8-888b-7794f4fd4c43"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(5338), new TimeSpan(0, 0, 0, 0, 0)), 24, "Stone Mine 24", true, "Stone", new Guid("c568a21a-44a3-44ae-8358-998e7f02f284"), new TimeSpan(9, 14, 24, 0, 0), "Upgrade To Stone Mine 25", 0, 0, 0, 0, 34560, 12, 12, 12, 12, 12 },
                    { new Guid("8c48de00-6605-4a45-8fff-61ad9c4e394b"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(9440), new TimeSpan(0, 0, 0, 0, 0)), 24, "Metal Mine 24", true, "Metal", new Guid("b8823bbc-cb6d-491b-97d9-deb69e820e27"), new TimeSpan(9, 14, 24, 0, 0), "Upgrade To Metal Mine 25", 0, 0, 0, 34560, 0, 12, 12, 12, 12, 12 },
                    { new Guid("c20746db-4fdc-460d-9f1b-2834b5b30255"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(1800), new TimeSpan(0, 0, 0, 0, 0)), 24, "Lumber Mill 24", true, "Lumber", new Guid("ea9c65e0-a993-42b6-a3e4-c8c48af6ba9a"), new TimeSpan(9, 14, 24, 0, 0), "Upgrade To Lumber Mill 25", 0, 0, 34560, 0, 0, 12, 12, 12, 12, 12 },
                    { new Guid("e11007de-e663-4593-bf9d-0f012b814197"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(8051), new TimeSpan(0, 0, 0, 0, 0)), 24, "Farm 24", true, "Food", new Guid("46b0991f-6ec6-4c0a-9b58-2b7415417ce1"), new TimeSpan(9, 14, 24, 0, 0), "Upgrade To Farm 25", 34560, 0, 0, 0, 0, 12, 12, 12, 12, 12 },
                    { new Guid("0d1a8d42-0ba2-45a8-81c9-32ef854ec27d"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(9554), new TimeSpan(0, 0, 0, 0, 0)), 23, "Metal Mine 23", true, "Metal", new Guid("8c48de00-6605-4a45-8fff-61ad9c4e394b"), new TimeSpan(8, 10, 47, 0, 0), "Upgrade To Metal Mine 24", 0, 0, 0, 31740, 0, 3, 3, 3, 3, 3 },
                    { new Guid("507e7512-5ab4-4a80-b77e-1968d96f2e83"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(5460), new TimeSpan(0, 0, 0, 0, 0)), 23, "Stone Mine 23", true, "Stone", new Guid("7b59c255-21ec-49f8-888b-7794f4fd4c43"), new TimeSpan(8, 10, 47, 0, 0), "Upgrade To Stone Mine 24", 0, 0, 0, 0, 31740, 3, 3, 3, 3, 3 },
                    { new Guid("84c110c9-431f-47fe-8335-01db77dcb6a3"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(1921), new TimeSpan(0, 0, 0, 0, 0)), 23, "Lumber Mill 23", true, "Lumber", new Guid("c20746db-4fdc-460d-9f1b-2834b5b30255"), new TimeSpan(8, 10, 47, 0, 0), "Upgrade To Lumber Mill 24", 0, 0, 31740, 0, 0, 3, 3, 3, 3, 3 },
                    { new Guid("c44727f1-75a6-437d-afb1-065c88b7cebc"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(8201), new TimeSpan(0, 0, 0, 0, 0)), 23, "Farm 23", true, "Food", new Guid("e11007de-e663-4593-bf9d-0f012b814197"), new TimeSpan(8, 10, 47, 0, 0), "Upgrade To Farm 24", 31740, 0, 0, 0, 0, 3, 3, 3, 3, 3 },
                    { new Guid("0a4f89ff-913d-4296-bdfa-51657f6a4b39"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(5574), new TimeSpan(0, 0, 0, 0, 0)), 22, "Stone Mine 22", true, "Stone", new Guid("507e7512-5ab4-4a80-b77e-1968d96f2e83"), new TimeSpan(7, 9, 28, 0, 0), "Upgrade To Stone Mine 23", 0, 0, 0, 0, 29040, 2, 2, 2, 2, 2 },
                    { new Guid("51c7a8e2-67f4-4b93-8361-b5287a028fbf"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(2033), new TimeSpan(0, 0, 0, 0, 0)), 22, "Lumber Mill 22", true, "Lumber", new Guid("84c110c9-431f-47fe-8335-01db77dcb6a3"), new TimeSpan(7, 9, 28, 0, 0), "Upgrade To Lumber Mill 23", 0, 0, 29040, 0, 0, 2, 2, 2, 2, 2 },
                    { new Guid("b85a4fa5-1658-4fa7-86e7-91f74581267f"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(8326), new TimeSpan(0, 0, 0, 0, 0)), 22, "Farm 22", true, "Food", new Guid("c44727f1-75a6-437d-afb1-065c88b7cebc"), new TimeSpan(7, 9, 28, 0, 0), "Upgrade To Farm 23", 29040, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
                    { new Guid("f528f585-12de-40b6-9136-4abc73770141"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(9709), new TimeSpan(0, 0, 0, 0, 0)), 22, "Metal Mine 22", true, "Metal", new Guid("0d1a8d42-0ba2-45a8-81c9-32ef854ec27d"), new TimeSpan(7, 9, 28, 0, 0), "Upgrade To Metal Mine 23", 0, 0, 0, 29040, 0, 2, 2, 2, 2, 2 },
                    { new Guid("14f3eafb-c909-404e-8b24-faaa28695be8"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(8496), new TimeSpan(0, 0, 0, 0, 0)), 21, "Farm 21", true, "Food", new Guid("b85a4fa5-1658-4fa7-86e7-91f74581267f"), new TimeSpan(6, 10, 21, 0, 0), "Upgrade To Farm 22", 26460, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
                    { new Guid("3458ee76-6180-4bd3-8d7e-c88d4bea6bd1"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(2195), new TimeSpan(0, 0, 0, 0, 0)), 21, "Lumber Mill 21", true, "Lumber", new Guid("51c7a8e2-67f4-4b93-8361-b5287a028fbf"), new TimeSpan(6, 10, 21, 0, 0), "Upgrade To Lumber Mill 22", 0, 0, 26460, 0, 0, 1, 1, 1, 1, 1 },
                    { new Guid("7e03e1fd-22bd-44ea-abf5-02c76d475e1f"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(5739), new TimeSpan(0, 0, 0, 0, 0)), 21, "Stone Mine 21", true, "Stone", new Guid("0a4f89ff-913d-4296-bdfa-51657f6a4b39"), new TimeSpan(6, 10, 21, 0, 0), "Upgrade To Stone Mine 22", 0, 0, 0, 0, 26460, 1, 1, 1, 1, 1 },
                    { new Guid("f83f057b-15d7-4460-8513-831983715285"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(9822), new TimeSpan(0, 0, 0, 0, 0)), 21, "Metal Mine 21", true, "Metal", new Guid("f528f585-12de-40b6-9136-4abc73770141"), new TimeSpan(6, 10, 21, 0, 0), "Upgrade To Metal Mine 22", 0, 0, 0, 26460, 0, 1, 1, 1, 1, 1 },
                    { new Guid("26cbc17b-ac88-4e8e-bcd1-e4b9b78db291"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(8623), new TimeSpan(0, 0, 0, 0, 0)), 20, "Farm 20", true, "Food", new Guid("14f3eafb-c909-404e-8b24-faaa28695be8"), new TimeSpan(5, 13, 20, 0, 0), "Upgrade To Farm 21", 24000, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { new Guid("9b62c092-6fa3-43de-bc0d-26c4ea7ca321"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(2310), new TimeSpan(0, 0, 0, 0, 0)), 20, "Lumber Mill 20", true, "Lumber", new Guid("3458ee76-6180-4bd3-8d7e-c88d4bea6bd1"), new TimeSpan(5, 13, 20, 0, 0), "Upgrade To Lumber Mill 21", 0, 0, 24000, 0, 0, 0, 0, 0, 0, 0 },
                    { new Guid("a9bc8a12-1100-4eac-ab28-949ebcea8c5f"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(5856), new TimeSpan(0, 0, 0, 0, 0)), 20, "Stone Mine 20", true, "Stone", new Guid("7e03e1fd-22bd-44ea-abf5-02c76d475e1f"), new TimeSpan(5, 13, 20, 0, 0), "Upgrade To Stone Mine 21", 0, 0, 0, 0, 24000, 0, 0, 0, 0, 0 },
                    { new Guid("c230013c-96ef-493e-8637-37937e27ac43"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(9935), new TimeSpan(0, 0, 0, 0, 0)), 20, "Metal Mine 20", true, "Metal", new Guid("f83f057b-15d7-4460-8513-831983715285"), new TimeSpan(5, 13, 20, 0, 0), "Upgrade To Metal Mine 21", 0, 0, 0, 24000, 0, 0, 0, 0, 0, 0 },
                    { new Guid("272468d1-7f9c-425d-bf2b-6eea0f74b764"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(2423), new TimeSpan(0, 0, 0, 0, 0)), 19, "Lumber Mill 19", true, "Lumber", new Guid("9b62c092-6fa3-43de-bc0d-26c4ea7ca321"), new TimeSpan(4, 18, 19, 0, 0), "Upgrade To Lumber Mill 20", 0, 0, 21660, 0, 0, 7, 7, 7, 7, 7 },
                    { new Guid("77b216e0-51f6-41bb-af01-9852992d41f8"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(8741), new TimeSpan(0, 0, 0, 0, 0)), 19, "Farm 19", true, "Food", new Guid("26cbc17b-ac88-4e8e-bcd1-e4b9b78db291"), new TimeSpan(4, 18, 19, 0, 0), "Upgrade To Farm 20", 21660, 0, 0, 0, 0, 7, 7, 7, 7, 7 },
                    { new Guid("c24eb876-784c-4fd7-8b5a-cb973bc80e56"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(89), new TimeSpan(0, 0, 0, 0, 0)), 19, "Metal Mine 19", true, "Metal", new Guid("c230013c-96ef-493e-8637-37937e27ac43"), new TimeSpan(4, 18, 19, 0, 0), "Upgrade To Metal Mine 20", 0, 0, 0, 21660, 0, 7, 7, 7, 7, 7 },
                    { new Guid("c47843f0-0bd7-4932-b0f2-6ee36b16c9bf"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(6006), new TimeSpan(0, 0, 0, 0, 0)), 19, "Stone Mine 19", true, "Stone", new Guid("a9bc8a12-1100-4eac-ab28-949ebcea8c5f"), new TimeSpan(4, 18, 19, 0, 0), "Upgrade To Stone Mine 20", 0, 0, 0, 0, 21660, 7, 7, 7, 7, 7 },
                    { new Guid("2587bd2d-8985-4ec2-b1bf-837aa26931bf"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(203), new TimeSpan(0, 0, 0, 0, 0)), 18, "Metal Mine 18", true, "Metal", new Guid("c24eb876-784c-4fd7-8b5a-cb973bc80e56"), new TimeSpan(4, 1, 12, 0, 0), "Upgrade To Metal Mine 19", 0, 0, 0, 19440, 0, 6, 6, 6, 6, 6 },
                    { new Guid("32a370b5-0499-4d6c-82b5-bc7210b8710f"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(8919), new TimeSpan(0, 0, 0, 0, 0)), 18, "Farm 18", true, "Food", new Guid("77b216e0-51f6-41bb-af01-9852992d41f8"), new TimeSpan(4, 1, 12, 0, 0), "Upgrade To Farm 19", 19440, 0, 0, 0, 0, 6, 6, 6, 6, 6 },
                    { new Guid("5bd6757d-7568-4359-a358-f7fd0fe5deae"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(6129), new TimeSpan(0, 0, 0, 0, 0)), 18, "Stone Mine 18", true, "Stone", new Guid("c47843f0-0bd7-4932-b0f2-6ee36b16c9bf"), new TimeSpan(4, 1, 12, 0, 0), "Upgrade To Stone Mine 19", 0, 0, 0, 0, 19440, 6, 6, 6, 6, 6 },
                    { new Guid("db8ef114-a68f-4d63-be26-6dbd423842cb"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(2624), new TimeSpan(0, 0, 0, 0, 0)), 18, "Lumber Mill 18", true, "Lumber", new Guid("272468d1-7f9c-425d-bf2b-6eea0f74b764"), new TimeSpan(4, 1, 12, 0, 0), "Upgrade To Lumber Mill 19", 0, 0, 19440, 0, 0, 6, 6, 6, 6, 6 },
                    { new Guid("1958e5d3-7e4a-4b04-91b3-91bd5d39707f"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(355), new TimeSpan(0, 0, 0, 0, 0)), 17, "Metal Mine 17", true, "Metal", new Guid("2587bd2d-8985-4ec2-b1bf-837aa26931bf"), new TimeSpan(3, 9, 53, 0, 0), "Upgrade To Metal Mine 18", 0, 0, 0, 17340, 0, 5, 5, 5, 5, 5 },
                    { new Guid("7de46a85-7062-4c1f-b7f3-fd6288f5aa20"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(6243), new TimeSpan(0, 0, 0, 0, 0)), 17, "Stone Mine 17", true, "Stone", new Guid("5bd6757d-7568-4359-a358-f7fd0fe5deae"), new TimeSpan(3, 9, 53, 0, 0), "Upgrade To Stone Mine 18", 0, 0, 0, 0, 17340, 5, 5, 5, 5, 5 },
                    { new Guid("a1d5cada-9190-4674-adbc-f01743d860a9"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(9034), new TimeSpan(0, 0, 0, 0, 0)), 17, "Farm 17", true, "Food", new Guid("32a370b5-0499-4d6c-82b5-bc7210b8710f"), new TimeSpan(3, 9, 53, 0, 0), "Upgrade To Farm 18", 17340, 0, 0, 0, 0, 5, 5, 5, 5, 5 },
                    { new Guid("f21660d6-96cc-44cb-88a8-06d30d64ca8b"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(2741), new TimeSpan(0, 0, 0, 0, 0)), 17, "Lumber Mill 17", true, "Lumber", new Guid("db8ef114-a68f-4d63-be26-6dbd423842cb"), new TimeSpan(3, 9, 53, 0, 0), "Upgrade To Lumber Mill 18", 0, 0, 17340, 0, 0, 5, 5, 5, 5, 5 },
                    { new Guid("1331f568-29c3-4e36-8983-4e7756903675"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(9205), new TimeSpan(0, 0, 0, 0, 0)), 16, "Farm 16", true, "Food", new Guid("a1d5cada-9190-4674-adbc-f01743d860a9"), new TimeSpan(2, 20, 16, 0, 0), "Upgrade To Farm 17", 15360, 0, 0, 0, 0, 4, 4, 4, 4, 4 },
                    { new Guid("1b42ba5b-98e3-4cbf-a760-16d7f6e65005"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(6396), new TimeSpan(0, 0, 0, 0, 0)), 16, "Stone Mine 16", true, "Stone", new Guid("7de46a85-7062-4c1f-b7f3-fd6288f5aa20"), new TimeSpan(2, 20, 16, 0, 0), "Upgrade To Stone Mine 17", 0, 0, 0, 0, 15360, 4, 4, 4, 4, 4 },
                    { new Guid("429966e6-ac9a-41ee-924a-f8a9dfd7fe6d"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(2923), new TimeSpan(0, 0, 0, 0, 0)), 16, "Lumber Mill 16", true, "Lumber", new Guid("f21660d6-96cc-44cb-88a8-06d30d64ca8b"), new TimeSpan(2, 20, 16, 0, 0), "Upgrade To Lumber Mill 17", 0, 0, 15360, 0, 0, 4, 4, 4, 4, 4 },
                    { new Guid("5780a993-3972-4efe-90fd-b6d3701bfb5e"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(476), new TimeSpan(0, 0, 0, 0, 0)), 16, "Metal Mine 16", true, "Metal", new Guid("1958e5d3-7e4a-4b04-91b3-91bd5d39707f"), new TimeSpan(2, 20, 16, 0, 0), "Upgrade To Metal Mine 17", 0, 0, 0, 15360, 0, 4, 4, 4, 4, 4 },
                    { new Guid("453eb613-ad3c-45f1-8ac3-6e427a3d9a70"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(589), new TimeSpan(0, 0, 0, 0, 0)), 15, "Metal Mine 15", true, "Metal", new Guid("5780a993-3972-4efe-90fd-b6d3701bfb5e"), new TimeSpan(2, 8, 15, 0, 0), "Upgrade To Metal Mine 16", 0, 0, 0, 13500, 0, 27, 27, 27, 27, 27 },
                    { new Guid("85da87f4-93f1-428f-b7d4-3a1e38ea32f4"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(6509), new TimeSpan(0, 0, 0, 0, 0)), 15, "Stone Mine 15", true, "Stone", new Guid("1b42ba5b-98e3-4cbf-a760-16d7f6e65005"), new TimeSpan(2, 8, 15, 0, 0), "Upgrade To Stone Mine 16", 0, 0, 0, 0, 13500, 27, 27, 27, 27, 27 },
                    { new Guid("c4ae1397-fe25-4822-80ba-89c19d10b6fc"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(3038), new TimeSpan(0, 0, 0, 0, 0)), 15, "Lumber Mill 15", true, "Lumber", new Guid("429966e6-ac9a-41ee-924a-f8a9dfd7fe6d"), new TimeSpan(2, 8, 15, 0, 0), "Upgrade To Lumber Mill 16", 0, 0, 13500, 0, 0, 27, 27, 27, 27, 27 },
                    { new Guid("f6a357e1-e138-4a67-9709-8d250e57f9f6"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(9329), new TimeSpan(0, 0, 0, 0, 0)), 15, "Farm 15", true, "Food", new Guid("1331f568-29c3-4e36-8983-4e7756903675"), new TimeSpan(2, 8, 15, 0, 0), "Upgrade To Farm 16", 13500, 0, 0, 0, 0, 27, 27, 27, 27, 27 },
                    { new Guid("774d281c-5062-4134-bc1e-37e26be662a2"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(3187), new TimeSpan(0, 0, 0, 0, 0)), 14, "Lumber Mill 14", true, "Lumber", new Guid("c4ae1397-fe25-4822-80ba-89c19d10b6fc"), new TimeSpan(1, 21, 44, 0, 0), "Upgrade To Lumber Mill 15", 0, 0, 11760, 0, 0, 26, 26, 26, 26, 26 },
                    { new Guid("adea883e-1f2e-42b2-bb3e-64775759f4ec"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(9444), new TimeSpan(0, 0, 0, 0, 0)), 14, "Farm 14", true, "Food", new Guid("f6a357e1-e138-4a67-9709-8d250e57f9f6"), new TimeSpan(1, 21, 44, 0, 0), "Upgrade To Farm 15", 11760, 0, 0, 0, 0, 26, 26, 26, 26, 26 },
                    { new Guid("b314d811-caee-46b3-b9b1-0f6f51c62931"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(6618), new TimeSpan(0, 0, 0, 0, 0)), 14, "Stone Mine 14", true, "Stone", new Guid("85da87f4-93f1-428f-b7d4-3a1e38ea32f4"), new TimeSpan(1, 21, 44, 0, 0), "Upgrade To Stone Mine 15", 0, 0, 0, 0, 11760, 26, 26, 26, 26, 26 },
                    { new Guid("eeaecd92-bb70-4090-81a4-71faa8d420bb"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(768), new TimeSpan(0, 0, 0, 0, 0)), 14, "Metal Mine 14", true, "Metal", new Guid("453eb613-ad3c-45f1-8ac3-6e427a3d9a70"), new TimeSpan(1, 21, 44, 0, 0), "Upgrade To Metal Mine 15", 0, 0, 0, 11760, 0, 26, 26, 26, 26, 26 },
                    { new Guid("3dc74ebc-5cc3-43e4-8f30-2bcd5ce098c5"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(9661), new TimeSpan(0, 0, 0, 0, 0)), 13, "Farm 13", true, "Food", new Guid("adea883e-1f2e-42b2-bb3e-64775759f4ec"), new TimeSpan(1, 12, 37, 0, 0), "Upgrade To Farm 14", 10140, 0, 0, 0, 0, 25, 25, 25, 25, 25 },
                    { new Guid("7c4affca-63c6-4168-ba95-5cc68ef7b22e"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(883), new TimeSpan(0, 0, 0, 0, 0)), 13, "Metal Mine 13", true, "Metal", new Guid("eeaecd92-bb70-4090-81a4-71faa8d420bb"), new TimeSpan(1, 12, 37, 0, 0), "Upgrade To Metal Mine 14", 0, 0, 0, 10140, 0, 25, 25, 25, 25, 25 },
                    { new Guid("c357e6bf-91d5-4014-ae8d-4c3c43a75088"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(6805), new TimeSpan(0, 0, 0, 0, 0)), 13, "Stone Mine 13", true, "Stone", new Guid("b314d811-caee-46b3-b9b1-0f6f51c62931"), new TimeSpan(1, 12, 37, 0, 0), "Upgrade To Stone Mine 14", 0, 0, 0, 0, 10140, 25, 25, 25, 25, 25 },
                    { new Guid("ded0a5a8-f3d3-492a-bce9-ff0608acdef4"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(3309), new TimeSpan(0, 0, 0, 0, 0)), 13, "Lumber Mill 13", true, "Lumber", new Guid("774d281c-5062-4134-bc1e-37e26be662a2"), new TimeSpan(1, 12, 37, 0, 0), "Upgrade To Lumber Mill 14", 0, 0, 10140, 0, 0, 25, 25, 25, 25, 25 },
                    { new Guid("2febede5-a459-4c27-9b43-954d5e55ff37"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(3421), new TimeSpan(0, 0, 0, 0, 0)), 12, "Lumber Mill 12", true, "Lumber", new Guid("ded0a5a8-f3d3-492a-bce9-ff0608acdef4"), new TimeSpan(1, 4, 48, 0, 0), "Upgrade To Lumber Mill 13", 0, 0, 8640, 0, 0, 24, 24, 24, 24, 24 },
                    { new Guid("72a178be-ad9e-446b-b6ee-2f7f301311e8"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(996), new TimeSpan(0, 0, 0, 0, 0)), 12, "Metal Mine 12", true, "Metal", new Guid("7c4affca-63c6-4168-ba95-5cc68ef7b22e"), new TimeSpan(1, 4, 48, 0, 0), "Upgrade To Metal Mine 13", 0, 0, 0, 8640, 0, 24, 24, 24, 24, 24 },
                    { new Guid("bfc8eef2-bcec-488f-b734-59ce853ae8d6"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(6961), new TimeSpan(0, 0, 0, 0, 0)), 12, "Stone Mine 12", true, "Stone", new Guid("c357e6bf-91d5-4014-ae8d-4c3c43a75088"), new TimeSpan(1, 4, 48, 0, 0), "Upgrade To Stone Mine 13", 0, 0, 0, 0, 8640, 24, 24, 24, 24, 24 },
                    { new Guid("dd132be9-eda6-4707-ab97-f52698a8a0f2"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(9786), new TimeSpan(0, 0, 0, 0, 0)), 12, "Farm 12", true, "Food", new Guid("3dc74ebc-5cc3-43e4-8f30-2bcd5ce098c5"), new TimeSpan(1, 4, 48, 0, 0), "Upgrade To Farm 13", 8640, 0, 0, 0, 0, 24, 24, 24, 24, 24 },
                    { new Guid("004f7ad7-8b26-4e4c-b81d-02840e05b5d8"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(7085), new TimeSpan(0, 0, 0, 0, 0)), 11, "Stone Mine 11", true, "Stone", new Guid("bfc8eef2-bcec-488f-b734-59ce853ae8d6"), new TimeSpan(0, 22, 11, 0, 0), "Upgrade To Stone Mine 12", 0, 0, 0, 0, 7260, 31, 31, 31, 31, 31 },
                    { new Guid("2f312a84-74e3-4acd-8867-4055ef516627"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(3575), new TimeSpan(0, 0, 0, 0, 0)), 11, "Lumber Mill 11", true, "Lumber", new Guid("2febede5-a459-4c27-9b43-954d5e55ff37"), new TimeSpan(0, 22, 11, 0, 0), "Upgrade To Lumber Mill 12", 0, 0, 7260, 0, 0, 31, 31, 31, 31, 31 },
                    { new Guid("4f476234-33b0-42e3-a502-710a44e6382f"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(1185), new TimeSpan(0, 0, 0, 0, 0)), 11, "Metal Mine 11", true, "Metal", new Guid("72a178be-ad9e-446b-b6ee-2f7f301311e8"), new TimeSpan(0, 22, 11, 0, 0), "Upgrade To Metal Mine 12", 0, 0, 0, 7260, 0, 31, 31, 31, 31, 31 },
                    { new Guid("ea59a893-94ce-41c9-91ea-2c17868c91d5"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 909, DateTimeKind.Unspecified).AddTicks(9961), new TimeSpan(0, 0, 0, 0, 0)), 11, "Farm 11", true, "Food", new Guid("dd132be9-eda6-4707-ab97-f52698a8a0f2"), new TimeSpan(0, 22, 11, 0, 0), "Upgrade To Farm 12", 7260, 0, 0, 0, 0, 31, 31, 31, 31, 31 },
                    { new Guid("18d44e8c-d605-4169-92d8-e1381d9cee77"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(7195), new TimeSpan(0, 0, 0, 0, 0)), 10, "Stone Mine 10", true, "Stone", new Guid("004f7ad7-8b26-4e4c-b81d-02840e05b5d8"), new TimeSpan(0, 16, 40, 0, 0), "Upgrade To Stone Mine 11", 0, 0, 0, 0, 6000, 30, 30, 30, 30, 30 },
                    { new Guid("62f2bab0-33b0-4330-a296-44fc1f4ac2c3"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(89), new TimeSpan(0, 0, 0, 0, 0)), 10, "Farm 10", true, "Food", new Guid("ea59a893-94ce-41c9-91ea-2c17868c91d5"), new TimeSpan(0, 16, 40, 0, 0), "Upgrade To Farm 11", 6000, 0, 0, 0, 0, 30, 30, 30, 30, 30 },
                    { new Guid("cb4f1b89-3e8c-4cc0-bbeb-c2a881c4bcd1"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(1297), new TimeSpan(0, 0, 0, 0, 0)), 10, "Metal Mine 10", true, "Metal", new Guid("4f476234-33b0-42e3-a502-710a44e6382f"), new TimeSpan(0, 16, 40, 0, 0), "Upgrade To Metal Mine 11", 0, 0, 0, 6000, 0, 30, 30, 30, 30, 30 },
                    { new Guid("f2b50905-363e-42d3-b5f2-29b567387ece"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(3687), new TimeSpan(0, 0, 0, 0, 0)), 10, "Lumber Mill 10", true, "Lumber", new Guid("2f312a84-74e3-4acd-8867-4055ef516627"), new TimeSpan(0, 16, 40, 0, 0), "Upgrade To Lumber Mill 11", 0, 0, 6000, 0, 0, 30, 30, 30, 30, 30 },
                    { new Guid("602b0b0c-5160-4dd6-9632-d8086ba02e7c"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(1451), new TimeSpan(0, 0, 0, 0, 0)), 9, "Metal Mine 9", true, "Metal", new Guid("cb4f1b89-3e8c-4cc0-bbeb-c2a881c4bcd1"), new TimeSpan(0, 12, 9, 0, 0), "Upgrade To Metal Mine 10", 0, 0, 0, 4860, 0, 29, 29, 29, 29, 29 },
                    { new Guid("85854685-b8b6-4106-ad1e-721f68d08324"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(203), new TimeSpan(0, 0, 0, 0, 0)), 9, "Farm 9", true, "Food", new Guid("62f2bab0-33b0-4330-a296-44fc1f4ac2c3"), new TimeSpan(0, 12, 9, 0, 0), "Upgrade To Farm 10", 4860, 0, 0, 0, 0, 29, 29, 29, 29, 29 },
                    { new Guid("9a15a05c-0608-4bba-a214-5b67e5546f22"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(3802), new TimeSpan(0, 0, 0, 0, 0)), 9, "Lumber Mill 9", true, "Lumber", new Guid("f2b50905-363e-42d3-b5f2-29b567387ece"), new TimeSpan(0, 12, 9, 0, 0), "Upgrade To Lumber Mill 10", 0, 0, 4860, 0, 0, 29, 29, 29, 29, 29 },
                    { new Guid("9a6b2744-6e4d-443f-aa67-66c1a1db716e"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(7335), new TimeSpan(0, 0, 0, 0, 0)), 9, "Stone Mine 9", true, "Stone", new Guid("18d44e8c-d605-4169-92d8-e1381d9cee77"), new TimeSpan(0, 12, 9, 0, 0), "Upgrade To Stone Mine 10", 0, 0, 0, 0, 4860, 29, 29, 29, 29, 29 },
                    { new Guid("0fc69a9e-60b7-4bc2-9769-3d91a42994a0"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(7869), new TimeSpan(0, 0, 0, 0, 0)), 8, "Stone Mine 8", true, "Stone", new Guid("9a6b2744-6e4d-443f-aa67-66c1a1db716e"), new TimeSpan(0, 8, 32, 0, 0), "Upgrade To Stone Mine 9", 0, 0, 0, 0, 3840, 28, 28, 28, 28, 28 },
                    { new Guid("2379208c-a2ef-4b91-8730-44dfe62b088f"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(3959), new TimeSpan(0, 0, 0, 0, 0)), 8, "Lumber Mill 8", true, "Lumber", new Guid("9a15a05c-0608-4bba-a214-5b67e5546f22"), new TimeSpan(0, 8, 32, 0, 0), "Upgrade To Lumber Mill 9", 0, 0, 3840, 0, 0, 28, 28, 28, 28, 28 },
                    { new Guid("3e998580-fdef-4b14-b30c-5aa099131dfe"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(378), new TimeSpan(0, 0, 0, 0, 0)), 8, "Farm 8", true, "Food", new Guid("85854685-b8b6-4106-ad1e-721f68d08324"), new TimeSpan(0, 8, 32, 0, 0), "Upgrade To Farm 9", 3840, 0, 0, 0, 0, 28, 28, 28, 28, 28 },
                    { new Guid("6d3ebca3-0b96-4224-ad27-68878949bd35"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(1575), new TimeSpan(0, 0, 0, 0, 0)), 8, "Metal Mine 8", true, "Metal", new Guid("602b0b0c-5160-4dd6-9632-d8086ba02e7c"), new TimeSpan(0, 8, 32, 0, 0), "Upgrade To Metal Mine 9", 0, 0, 0, 3840, 0, 28, 28, 28, 28, 28 },
                    { new Guid("45fb6717-c258-4149-bf07-6a5699ec8e3a"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(4071), new TimeSpan(0, 0, 0, 0, 0)), 7, "Lumber Mill 7", true, "Lumber", new Guid("2379208c-a2ef-4b91-8730-44dfe62b088f"), new TimeSpan(0, 5, 43, 0, 0), "Upgrade To Lumber Mill 8", 0, 0, 2940, 0, 0, 19, 19, 19, 19, 19 },
                    { new Guid("5c1f1726-b594-47c4-9696-23ba3a813bcd"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(496), new TimeSpan(0, 0, 0, 0, 0)), 7, "Farm 7", true, "Food", new Guid("3e998580-fdef-4b14-b30c-5aa099131dfe"), new TimeSpan(0, 5, 43, 0, 0), "Upgrade To Farm 8", 2940, 0, 0, 0, 0, 19, 19, 19, 19, 19 },
                    { new Guid("71c59c5c-b872-4bf1-9053-f6bee02398bc"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(8105), new TimeSpan(0, 0, 0, 0, 0)), 7, "Stone Mine 7", true, "Stone", new Guid("0fc69a9e-60b7-4bc2-9769-3d91a42994a0"), new TimeSpan(0, 5, 43, 0, 0), "Upgrade To Stone Mine 8", 0, 0, 0, 0, 2940, 19, 19, 19, 19, 19 },
                    { new Guid("e7f2c839-88dc-4231-9c17-6c1930ad5cb9"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(1687), new TimeSpan(0, 0, 0, 0, 0)), 7, "Metal Mine 7", true, "Metal", new Guid("6d3ebca3-0b96-4224-ad27-68878949bd35"), new TimeSpan(0, 5, 43, 0, 0), "Upgrade To Metal Mine 8", 0, 0, 0, 2940, 0, 19, 19, 19, 19, 19 },
                    { new Guid("043f0d46-335e-40b1-beec-496b13465c21"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(8316), new TimeSpan(0, 0, 0, 0, 0)), 6, "Stone Mine 6", true, "Stone", new Guid("71c59c5c-b872-4bf1-9053-f6bee02398bc"), new TimeSpan(0, 3, 36, 0, 0), "Upgrade To Stone Mine 7", 0, 0, 0, 0, 2160, 18, 18, 18, 18, 18 },
                    { new Guid("1e63a93c-8682-47fa-90c7-2f16b64ae204"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(612), new TimeSpan(0, 0, 0, 0, 0)), 6, "Farm 6", true, "Food", new Guid("5c1f1726-b594-47c4-9696-23ba3a813bcd"), new TimeSpan(0, 3, 36, 0, 0), "Upgrade To Farm 7", 2160, 0, 0, 0, 0, 18, 18, 18, 18, 18 },
                    { new Guid("52b3beed-538f-443e-8168-c8421c505b77"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(1841), new TimeSpan(0, 0, 0, 0, 0)), 6, "Metal Mine 6", true, "Metal", new Guid("e7f2c839-88dc-4231-9c17-6c1930ad5cb9"), new TimeSpan(0, 3, 36, 0, 0), "Upgrade To Metal Mine 7", 0, 0, 0, 2160, 0, 18, 18, 18, 18, 18 },
                    { new Guid("8d2e894b-1fd2-40e6-8398-5f48446e9f57"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(4236), new TimeSpan(0, 0, 0, 0, 0)), 6, "Lumber Mill 6", true, "Lumber", new Guid("45fb6717-c258-4149-bf07-6a5699ec8e3a"), new TimeSpan(0, 3, 36, 0, 0), "Upgrade To Lumber Mill 7", 0, 0, 2160, 0, 0, 18, 18, 18, 18, 18 },
                    { new Guid("34b3002f-b65f-4012-b570-f87cc5f57a6b"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(786), new TimeSpan(0, 0, 0, 0, 0)), 5, "Farm 5", true, "Food", new Guid("1e63a93c-8682-47fa-90c7-2f16b64ae204"), new TimeSpan(0, 2, 5, 0, 0), "Upgrade To Farm 6", 1500, 0, 0, 0, 0, 17, 17, 17, 17, 17 },
                    { new Guid("5fcd5538-bd46-41dd-a662-78236f3c17b9"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(1953), new TimeSpan(0, 0, 0, 0, 0)), 5, "Metal Mine 5", true, "Metal", new Guid("52b3beed-538f-443e-8168-c8421c505b77"), new TimeSpan(0, 2, 5, 0, 0), "Upgrade To Metal Mine 6", 0, 0, 0, 1500, 0, 17, 17, 17, 17, 17 },
                    { new Guid("83afb734-510c-47a7-ab00-9046627ecdc5"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(8438), new TimeSpan(0, 0, 0, 0, 0)), 5, "Stone Mine 5", true, "Stone", new Guid("043f0d46-335e-40b1-beec-496b13465c21"), new TimeSpan(0, 2, 5, 0, 0), "Upgrade To Stone Mine 6", 0, 0, 0, 0, 1500, 17, 17, 17, 17, 17 },
                    { new Guid("cc6e2efd-c9b6-4878-b125-b233f35458a7"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(4357), new TimeSpan(0, 0, 0, 0, 0)), 5, "Lumber Mill 5", true, "Lumber", new Guid("8d2e894b-1fd2-40e6-8398-5f48446e9f57"), new TimeSpan(0, 2, 5, 0, 0), "Upgrade To Lumber Mill 6", 0, 0, 1500, 0, 0, 17, 17, 17, 17, 17 },
                    { new Guid("13261eb9-159e-4d95-9e62-5079456563b2"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(903), new TimeSpan(0, 0, 0, 0, 0)), 4, "Farm 4", true, "Food", new Guid("34b3002f-b65f-4012-b570-f87cc5f57a6b"), new TimeSpan(0, 1, 4, 0, 0), "Upgrade To Farm 5", 960, 0, 0, 0, 0, 16, 16, 16, 16, 16 },
                    { new Guid("887be77e-d4fe-44dd-a5f9-0bc8f65e8f43"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(8598), new TimeSpan(0, 0, 0, 0, 0)), 4, "Stone Mine 4", true, "Stone", new Guid("83afb734-510c-47a7-ab00-9046627ecdc5"), new TimeSpan(0, 1, 4, 0, 0), "Upgrade To Stone Mine 5", 0, 0, 0, 0, 960, 16, 16, 16, 16, 16 },
                    { new Guid("8dd84077-d3bc-460e-a16e-d7feea99fee3"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(4465), new TimeSpan(0, 0, 0, 0, 0)), 4, "Lumber Mill 4", true, "Lumber", new Guid("cc6e2efd-c9b6-4878-b125-b233f35458a7"), new TimeSpan(0, 1, 4, 0, 0), "Upgrade To Lumber Mill 5", 0, 0, 960, 0, 0, 16, 16, 16, 16, 16 },
                    { new Guid("d015fbdc-1245-4e30-a56a-466c8ade4b34"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(2063), new TimeSpan(0, 0, 0, 0, 0)), 4, "Metal Mine 4", true, "Metal", new Guid("5fcd5538-bd46-41dd-a662-78236f3c17b9"), new TimeSpan(0, 1, 4, 0, 0), "Upgrade To Metal Mine 5", 0, 0, 0, 960, 0, 16, 16, 16, 16, 16 },
                    { new Guid("1876ac7b-723b-4f22-8ae9-1fcb5a03fada"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(4617), new TimeSpan(0, 0, 0, 0, 0)), 3, "Lumber Mill 3", true, "Lumber", new Guid("8dd84077-d3bc-460e-a16e-d7feea99fee3"), new TimeSpan(0, 0, 27, 0, 0), "Upgrade To Lumber Mill 4", 0, 0, 540, 0, 0, 23, 23, 23, 23, 23 },
                    { new Guid("ce55a7e0-e30f-4bd1-9bbb-31a6fb704c74"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(1071), new TimeSpan(0, 0, 0, 0, 0)), 3, "Farm 3", true, "Food", new Guid("13261eb9-159e-4d95-9e62-5079456563b2"), new TimeSpan(0, 0, 27, 0, 0), "Upgrade To Farm 4", 540, 0, 0, 0, 0, 23, 23, 23, 23, 23 },
                    { new Guid("db57966c-7bad-41d3-b591-29814f04446e"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(2216), new TimeSpan(0, 0, 0, 0, 0)), 3, "Metal Mine 3", true, "Metal", new Guid("d015fbdc-1245-4e30-a56a-466c8ade4b34"), new TimeSpan(0, 0, 27, 0, 0), "Upgrade To Metal Mine 4", 0, 0, 0, 540, 0, 23, 23, 23, 23, 23 },
                    { new Guid("f51dbb55-0f6f-4472-a841-364becdf3cb1"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(8723), new TimeSpan(0, 0, 0, 0, 0)), 3, "Stone Mine 3", true, "Stone", new Guid("887be77e-d4fe-44dd-a5f9-0bc8f65e8f43"), new TimeSpan(0, 0, 27, 0, 0), "Upgrade To Stone Mine 4", 0, 0, 0, 0, 540, 23, 23, 23, 23, 23 },
                    { new Guid("1843e86a-e742-4d8a-b10d-24293e028f39"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(8837), new TimeSpan(0, 0, 0, 0, 0)), 2, "Stone Mine 2", true, "Stone", new Guid("f51dbb55-0f6f-4472-a841-364becdf3cb1"), new TimeSpan(0, 0, 8, 0, 0), "Upgrade To Stone Mine 3", 0, 0, 0, 0, 240, 22, 22, 22, 22, 22 },
                    { new Guid("a609bf36-2ef3-43f0-9579-e9d80221fd60"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(4728), new TimeSpan(0, 0, 0, 0, 0)), 2, "Lumber Mill 2", true, "Lumber", new Guid("1876ac7b-723b-4f22-8ae9-1fcb5a03fada"), new TimeSpan(0, 0, 8, 0, 0), "Upgrade To Lumber Mill 3", 0, 0, 240, 0, 0, 22, 22, 22, 22, 22 },
                    { new Guid("ae055e86-9d20-4dd5-a49e-6d8ebe284a95"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(1197), new TimeSpan(0, 0, 0, 0, 0)), 2, "Farm 2", true, "Food", new Guid("ce55a7e0-e30f-4bd1-9bbb-31a6fb704c74"), new TimeSpan(0, 0, 8, 0, 0), "Upgrade To Farm 3", 240, 0, 0, 0, 0, 22, 22, 22, 22, 22 },
                    { new Guid("bc6e57fd-f9e9-4186-af98-dc86c0308bc6"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(2326), new TimeSpan(0, 0, 0, 0, 0)), 2, "Metal Mine 2", true, "Metal", new Guid("db57966c-7bad-41d3-b591-29814f04446e"), new TimeSpan(0, 0, 8, 0, 0), "Upgrade To Metal Mine 3", 0, 0, 0, 240, 0, 22, 22, 22, 22, 22 },
                    { new Guid("014febf3-a532-4102-99d7-475037a8e0d4"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(1312), new TimeSpan(0, 0, 0, 0, 0)), 1, "Farm 1", true, "Food", new Guid("ae055e86-9d20-4dd5-a49e-6d8ebe284a95"), new TimeSpan(0, 0, 1, 0, 0), "Upgrade To Farm 2", 60, 0, 0, 0, 0, 21, 21, 21, 21, 21 },
                    { new Guid("69b33dd3-763c-4a82-9189-ac9ca0f254e7"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(2483), new TimeSpan(0, 0, 0, 0, 0)), 1, "Metal Mine 1", true, "Metal", new Guid("bc6e57fd-f9e9-4186-af98-dc86c0308bc6"), new TimeSpan(0, 0, 1, 0, 0), "Upgrade To Metal Mine 2", 0, 0, 0, 60, 0, 21, 21, 21, 21, 21 },
                    { new Guid("b57c3178-7278-4164-8370-ebb8c0c304d1"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 0, 0, 0, 0)), 1, "Stone Mine 1", true, "Stone", new Guid("1843e86a-e742-4d8a-b10d-24293e028f39"), new TimeSpan(0, 0, 1, 0, 0), "Upgrade To Stone Mine 2", 0, 0, 0, 0, 60, 21, 21, 21, 21, 21 },
                    { new Guid("cb6bc83c-bb3d-47f9-af3b-67d9ad6eedba"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(4839), new TimeSpan(0, 0, 0, 0, 0)), 1, "Lumber Mill 1", true, "Lumber", new Guid("a609bf36-2ef3-43f0-9579-e9d80221fd60"), new TimeSpan(0, 0, 1, 0, 0), "Upgrade To Lumber Mill 2", 0, 0, 60, 0, 0, 21, 21, 21, 21, 21 },
                    { new Guid("133b8a32-d3db-48a0-a7c1-e6f019197b30"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(1479), new TimeSpan(0, 0, 0, 0, 0)), 0, "ِDamaged Farm", true, "Food", new Guid("014febf3-a532-4102-99d7-475037a8e0d4"), new TimeSpan(0, 0, 0, 0, 0), "Repair The Farm", 0, 0, 0, 0, 0, 20, 20, 20, 20, 20 },
                    { new Guid("73e851fb-9368-4bbb-b5bf-76f0d3a8832a"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 911, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 0, 0, 0, 0)), 0, "ِDamaged Metal Mine", true, "Metal", new Guid("69b33dd3-763c-4a82-9189-ac9ca0f254e7"), new TimeSpan(0, 0, 0, 0, 0), "Repair The Metal Mine", 0, 0, 0, 0, 0, 20, 20, 20, 20, 20 },
                    { new Guid("946009bc-f652-4541-888f-4b2560052b88"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(9111), new TimeSpan(0, 0, 0, 0, 0)), 0, "ِDamaged Stone Mine", true, "Stone", new Guid("b57c3178-7278-4164-8370-ebb8c0c304d1"), new TimeSpan(0, 0, 0, 0, 0), "Repair The Stone Mine", 0, 0, 0, 0, 0, 20, 20, 20, 20, 20 },
                    { new Guid("b5ddbf31-7cec-4683-ab58-e5f7d87cd1a3"), new DateTimeOffset(new DateTime(2023, 10, 26, 21, 29, 55, 910, DateTimeKind.Unspecified).AddTicks(4997), new TimeSpan(0, 0, 0, 0, 0)), 0, "ِDamaged Lumber Mill", true, "Lumber", new Guid("cb6bc83c-bb3d-47f9-af3b-67d9ad6eedba"), new TimeSpan(0, 0, 0, 0, 0), "Repair The Lumber Mill", 0, 0, 0, 0, 0, 20, 20, 20, 20, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceBuildingVillage_VillageId",
                table: "ResourceBuildingVillage",
                column: "VillageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceBuilding_ResourceBuilding_TargetId",
                table: "ResourceBuilding",
                column: "TargetId",
                principalTable: "ResourceBuilding",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceBuilding_ResourceBuilding_TargetId",
                table: "ResourceBuilding");

            migrationBuilder.DropTable(
                name: "ResourceBuildingVillage");

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("133b8a32-d3db-48a0-a7c1-e6f019197b30"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("73e851fb-9368-4bbb-b5bf-76f0d3a8832a"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("946009bc-f652-4541-888f-4b2560052b88"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("b5ddbf31-7cec-4683-ab58-e5f7d87cd1a3"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("014febf3-a532-4102-99d7-475037a8e0d4"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("69b33dd3-763c-4a82-9189-ac9ca0f254e7"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("b57c3178-7278-4164-8370-ebb8c0c304d1"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("cb6bc83c-bb3d-47f9-af3b-67d9ad6eedba"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("1843e86a-e742-4d8a-b10d-24293e028f39"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("a609bf36-2ef3-43f0-9579-e9d80221fd60"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("ae055e86-9d20-4dd5-a49e-6d8ebe284a95"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("bc6e57fd-f9e9-4186-af98-dc86c0308bc6"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("1876ac7b-723b-4f22-8ae9-1fcb5a03fada"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("ce55a7e0-e30f-4bd1-9bbb-31a6fb704c74"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("db57966c-7bad-41d3-b591-29814f04446e"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("f51dbb55-0f6f-4472-a841-364becdf3cb1"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("13261eb9-159e-4d95-9e62-5079456563b2"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("887be77e-d4fe-44dd-a5f9-0bc8f65e8f43"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("8dd84077-d3bc-460e-a16e-d7feea99fee3"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("d015fbdc-1245-4e30-a56a-466c8ade4b34"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("34b3002f-b65f-4012-b570-f87cc5f57a6b"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("5fcd5538-bd46-41dd-a662-78236f3c17b9"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("83afb734-510c-47a7-ab00-9046627ecdc5"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("cc6e2efd-c9b6-4878-b125-b233f35458a7"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("043f0d46-335e-40b1-beec-496b13465c21"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("1e63a93c-8682-47fa-90c7-2f16b64ae204"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("52b3beed-538f-443e-8168-c8421c505b77"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("8d2e894b-1fd2-40e6-8398-5f48446e9f57"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("45fb6717-c258-4149-bf07-6a5699ec8e3a"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("5c1f1726-b594-47c4-9696-23ba3a813bcd"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("71c59c5c-b872-4bf1-9053-f6bee02398bc"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("e7f2c839-88dc-4231-9c17-6c1930ad5cb9"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("0fc69a9e-60b7-4bc2-9769-3d91a42994a0"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("2379208c-a2ef-4b91-8730-44dfe62b088f"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("3e998580-fdef-4b14-b30c-5aa099131dfe"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("6d3ebca3-0b96-4224-ad27-68878949bd35"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("602b0b0c-5160-4dd6-9632-d8086ba02e7c"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("85854685-b8b6-4106-ad1e-721f68d08324"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("9a15a05c-0608-4bba-a214-5b67e5546f22"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("9a6b2744-6e4d-443f-aa67-66c1a1db716e"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("18d44e8c-d605-4169-92d8-e1381d9cee77"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("62f2bab0-33b0-4330-a296-44fc1f4ac2c3"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("cb4f1b89-3e8c-4cc0-bbeb-c2a881c4bcd1"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("f2b50905-363e-42d3-b5f2-29b567387ece"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("004f7ad7-8b26-4e4c-b81d-02840e05b5d8"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("2f312a84-74e3-4acd-8867-4055ef516627"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("4f476234-33b0-42e3-a502-710a44e6382f"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("ea59a893-94ce-41c9-91ea-2c17868c91d5"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("2febede5-a459-4c27-9b43-954d5e55ff37"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("72a178be-ad9e-446b-b6ee-2f7f301311e8"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("bfc8eef2-bcec-488f-b734-59ce853ae8d6"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("dd132be9-eda6-4707-ab97-f52698a8a0f2"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("3dc74ebc-5cc3-43e4-8f30-2bcd5ce098c5"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("7c4affca-63c6-4168-ba95-5cc68ef7b22e"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("c357e6bf-91d5-4014-ae8d-4c3c43a75088"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("ded0a5a8-f3d3-492a-bce9-ff0608acdef4"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("774d281c-5062-4134-bc1e-37e26be662a2"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("adea883e-1f2e-42b2-bb3e-64775759f4ec"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("b314d811-caee-46b3-b9b1-0f6f51c62931"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("eeaecd92-bb70-4090-81a4-71faa8d420bb"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("453eb613-ad3c-45f1-8ac3-6e427a3d9a70"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("85da87f4-93f1-428f-b7d4-3a1e38ea32f4"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("c4ae1397-fe25-4822-80ba-89c19d10b6fc"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("f6a357e1-e138-4a67-9709-8d250e57f9f6"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("1331f568-29c3-4e36-8983-4e7756903675"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("1b42ba5b-98e3-4cbf-a760-16d7f6e65005"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("429966e6-ac9a-41ee-924a-f8a9dfd7fe6d"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("5780a993-3972-4efe-90fd-b6d3701bfb5e"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("1958e5d3-7e4a-4b04-91b3-91bd5d39707f"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("7de46a85-7062-4c1f-b7f3-fd6288f5aa20"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("a1d5cada-9190-4674-adbc-f01743d860a9"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("f21660d6-96cc-44cb-88a8-06d30d64ca8b"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("2587bd2d-8985-4ec2-b1bf-837aa26931bf"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("32a370b5-0499-4d6c-82b5-bc7210b8710f"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("5bd6757d-7568-4359-a358-f7fd0fe5deae"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("db8ef114-a68f-4d63-be26-6dbd423842cb"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("272468d1-7f9c-425d-bf2b-6eea0f74b764"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("77b216e0-51f6-41bb-af01-9852992d41f8"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("c24eb876-784c-4fd7-8b5a-cb973bc80e56"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("c47843f0-0bd7-4932-b0f2-6ee36b16c9bf"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("26cbc17b-ac88-4e8e-bcd1-e4b9b78db291"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("9b62c092-6fa3-43de-bc0d-26c4ea7ca321"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("a9bc8a12-1100-4eac-ab28-949ebcea8c5f"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("c230013c-96ef-493e-8637-37937e27ac43"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("14f3eafb-c909-404e-8b24-faaa28695be8"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("3458ee76-6180-4bd3-8d7e-c88d4bea6bd1"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("7e03e1fd-22bd-44ea-abf5-02c76d475e1f"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("f83f057b-15d7-4460-8513-831983715285"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("0a4f89ff-913d-4296-bdfa-51657f6a4b39"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("51c7a8e2-67f4-4b93-8361-b5287a028fbf"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("b85a4fa5-1658-4fa7-86e7-91f74581267f"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("f528f585-12de-40b6-9136-4abc73770141"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("0d1a8d42-0ba2-45a8-81c9-32ef854ec27d"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("507e7512-5ab4-4a80-b77e-1968d96f2e83"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("84c110c9-431f-47fe-8335-01db77dcb6a3"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("c44727f1-75a6-437d-afb1-065c88b7cebc"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("7b59c255-21ec-49f8-888b-7794f4fd4c43"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("8c48de00-6605-4a45-8fff-61ad9c4e394b"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("c20746db-4fdc-460d-9f1b-2834b5b30255"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("e11007de-e663-4593-bf9d-0f012b814197"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("46b0991f-6ec6-4c0a-9b58-2b7415417ce1"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("b8823bbc-cb6d-491b-97d9-deb69e820e27"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("c568a21a-44a3-44ae-8358-998e7f02f284"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("ea9c65e0-a993-42b6-a3e4-c8c48af6ba9a"));

            migrationBuilder.DropColumn(
                name: "LastResourceGatherDate",
                table: "ResourceBuilding");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "ResourceBuilding");

            migrationBuilder.DropColumn(
                name: "NeedBuilderForUpgrade",
                table: "ResourceBuilding");

            migrationBuilder.DropColumn(
                name: "UpgradeName",
                table: "ResourceBuilding");

            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "ResourceBuilding",
                newName: "RequirementId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceBuilding_TargetId",
                table: "ResourceBuilding",
                newName: "IX_ResourceBuilding_RequirementId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastResourceChangeTime",
                table: "Village",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "VillageResourceBuilding",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceBuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    Completed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                table: "ResourceBuilding",
                columns: new[] { "Id", "Name", "ProductionType", "RequirementId", "UpgradeDuration", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone" },
                values: new object[,]
                {
                    { new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0"), "Farm1", "Food", null, new TimeSpan(0, 0, 1, 0, 0), 600, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { new Guid("0986a9a2-d235-4a62-8ae4-1eb1a31eab18"), "Farm2", "Food", new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0"), new TimeSpan(0, 0, 2, 0, 0), 1200, 0, 0, 0, 0, 10, 0, 10, 0, 0 }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceBuilding_ResourceBuilding_RequirementId",
                table: "ResourceBuilding",
                column: "RequirementId",
                principalTable: "ResourceBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
