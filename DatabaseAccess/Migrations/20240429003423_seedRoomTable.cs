using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatabaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "tbl_Rooms",
                newName: "UpdatedDate");

            migrationBuilder.InsertData(
                table: "tbl_Rooms",
                columns: new[] { "RoomId", "CreatedDate", "Description", "ImageUrl", "MaxOccupancy", "Price", "RoomName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(6963), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Single.jpg", 1, 85.0, "Single Room", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(7108), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Double.jpg", 2, 90.0, "Double Room", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(7115), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Deluxed.jpg", 3, 100.0, "Deluxed Room", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(7119), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Queens.jpg", 4, 120.0, "Queens Room", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(7124), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Kings.jpg", 5, 130.0, "Kings Room", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(7128), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Executive.jpg", 10, 100.0, "Executive Suite", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(7132), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Super Deluxed.jpg", 10, 110.0, "Super Deluxed", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(7136), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Diamond Room.jpg", 10, 87.0, "Diamond Room", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2024, 4, 29, 12, 34, 23, 84, DateTimeKind.Local).AddTicks(7140), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\\\img\\\\Rooms\\\\Emerald Room.jpg", 10, 98.0, "Emerald Deluxed", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 9);

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "tbl_Rooms",
                newName: "UpdateDate");
        }
    }
}
