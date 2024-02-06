using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GianssWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InsertaObjetos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gians",
                columns: new[] { "Id", "Amenidad", "CreationDate", "Detalle", "ImagenUrl", "MetrosCuadrados", "Name", "Ocupantes", "Tarifa", "UptdateDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 2, 5, 17, 46, 36, 862, DateTimeKind.Local).AddTicks(35), "Detalle Test 1", "", 60, "Test1", 5, 600.0, new DateTime(2024, 2, 5, 17, 46, 36, 862, DateTimeKind.Local).AddTicks(47) },
                    { 2, "", new DateTime(2024, 2, 5, 17, 46, 36, 862, DateTimeKind.Local).AddTicks(49), "Detalle Test 2", "", 160, "Test2", 10, 800.0, new DateTime(2024, 2, 5, 17, 46, 36, 862, DateTimeKind.Local).AddTicks(50) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gians",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gians",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
