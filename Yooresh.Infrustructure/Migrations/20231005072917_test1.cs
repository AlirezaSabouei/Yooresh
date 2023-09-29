using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yooresh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "Confirmed", "Email", "Name", "Password", "Role" },
                values: new object[] { new Guid("a58bef94-8437-4856-bd87-a7b861285773"), true, "alireza.sabouei@gmail.com", "Alireza Sabouei", "Aa123456", "SimplePlayer" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: new Guid("a58bef94-8437-4856-bd87-a7b861285773"));
        }
    }
}
