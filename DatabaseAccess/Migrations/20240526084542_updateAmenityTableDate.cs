using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateAmenityTableDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "tbl_Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "tbl_RoomNumber",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UpdatedDate",
                table: "tbl_Amenity",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedDate",
                table: "tbl_Amenity",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7284));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7397));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7404));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7413));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7417));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 26, 20, 45, 42, 473, DateTimeKind.Local).AddTicks(7429));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "tbl_Rooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "tbl_RoomNumber",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UpdatedDate",
                table: "tbl_Amenity",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedDate",
                table: "tbl_Amenity",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "tbl_Amenity",
                keyColumn: "AmenityId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1012));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1142));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1154));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1159));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1163));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1168));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1173));

            migrationBuilder.UpdateData(
                table: "tbl_Rooms",
                keyColumn: "RoomId",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 9, 57, 36, 407, DateTimeKind.Local).AddTicks(1178));
        }
    }
}
