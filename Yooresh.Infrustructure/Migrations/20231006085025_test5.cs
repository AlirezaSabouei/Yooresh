using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yooresh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ResourceBuilding",
                columns: new[] { "Id", "Name", "ProductionType", "RequirementId", "UpgradeDuration", "HourlyProductionFood", "HourlyProductionGold", "HourlyProductionLumber", "HourlyProductionMetal", "HourlyProductionStone", "UpgradeCostFood", "UpgradeCostGold", "UpgradeCostLumber", "UpgradeCostMetal", "UpgradeCostStone" },
                values: new object[,]
                {
                    { new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0"), "Farm1", "Food", null, new TimeSpan(0, 0, 1, 0, 0), 600, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { new Guid("0986a9a2-d235-4a62-8ae4-1eb1a31eab18"), "Farm2", "Food", new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0"), new TimeSpan(0, 0, 2, 0, 0), 1200, 0, 0, 0, 0, 10, 0, 10, 0, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("0986a9a2-d235-4a62-8ae4-1eb1a31eab18"));

            migrationBuilder.DeleteData(
                table: "ResourceBuilding",
                keyColumn: "Id",
                keyValue: new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0"));
        }
    }
}
