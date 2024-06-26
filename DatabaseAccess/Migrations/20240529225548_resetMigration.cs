﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatabaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class resetMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Amenity",
                columns: table => new
                {
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmenityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UpdatedDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Amenity", x => x.AmenityId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ResetPassword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ResetPassword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomPrice = table.Column<double>(type: "float", nullable: false),
                    MaxOccupancy = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_RoleClaims_tbl_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_RoomAmenity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_RoomAmenity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_RoomAmenity_tbl_Amenity_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "tbl_Amenity",
                        principalColumn: "AmenityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_RoomAmenity_tbl_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "tbl_Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_RoomNumber",
                columns: table => new
                {
                    RoomNo = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_RoomNumber", x => x.RoomNo);
                    table.ForeignKey(
                        name: "FK_tbl_RoomNumber_tbl_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "tbl_Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    RoomNo = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    TotalCost = table.Column<double>(type: "float", maxLength: 12, nullable: false),
                    BookingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckinDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckoutDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NoOfStay = table.Column<int>(type: "int", nullable: false),
                    IsPaymentSuccessfull = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StripeSessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualCheckinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCheckoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCancelledDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_tbl_Bookings_tbl_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "tbl_Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Bookings_tbl_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_UserClaims_tbl_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_tbl_UserLogins_tbl_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tbl_UserRoles_tbl_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_UserRoles_tbl_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_tbl_UserTokens_tbl_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tbl_Amenity",
                columns: new[] { "AmenityId", "AmenityName", "CreatedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Washing Machine", new DateOnly(2024, 5, 30), new DateOnly(1900, 1, 1) },
                    { 2, "Electric Fan", new DateOnly(2024, 5, 30), new DateOnly(1900, 1, 1) },
                    { 3, "TV", new DateOnly(2024, 5, 30), new DateOnly(1900, 1, 1) },
                    { 4, "Internet Wifi", new DateOnly(2024, 5, 30), new DateOnly(1900, 1, 1) },
                    { 5, "Microwave", new DateOnly(2024, 5, 30), new DateOnly(1900, 1, 1) }
                });

            migrationBuilder.InsertData(
                table: "tbl_Rooms",
                columns: new[] { "RoomId", "CreatedDate", "Description", "ImageUrl", "ImageUrlLocalPath", "MaxOccupancy", "RoomName", "RoomPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1739), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Single.jpg", null, 1, "Single Room", 85.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1856), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Double.jpg", null, 2, "Double Room", 90.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1864), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Deluxed.jpg", null, 3, "Deluxed Room", 100.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1868), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Queens.jpg", null, 4, "Queens Room", 120.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1872), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Kings.jpg", null, 5, "Kings Room", 130.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1875), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Executive.jpg", null, 10, "Executive Suite", 100.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1879), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Super Deluxed.jpg", null, 10, "Super Deluxed", 110.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1883), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Diamond Room.jpg", null, 10, "Diamond Room", 87.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2024, 5, 30, 10, 55, 48, 487, DateTimeKind.Local).AddTicks(1887), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.", "\\img\\Rooms\\Emerald Room.jpg", null, 10, "Emerald Deluxed", 98.0, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "tbl_RoomAmenity",
                columns: new[] { "Id", "AmenityId", "RoomId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 3, 2 },
                    { 7, 1, 2 },
                    { 8, 5, 3 },
                    { 9, 3, 4 },
                    { 10, 5, 5 },
                    { 11, 1, 7 },
                    { 12, 5, 7 },
                    { 13, 3, 8 },
                    { 14, 5, 9 }
                });

            migrationBuilder.InsertData(
                table: "tbl_RoomNumber",
                columns: new[] { "RoomNo", "CreatedDate", "Description", "RoomId", "UpdatedDate" },
                values: new object[,]
                {
                    { 101, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor.", 1, new DateOnly(1900, 1, 1) },
                    { 102, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 1, new DateOnly(1900, 1, 1) },
                    { 103, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 1, new DateOnly(1900, 1, 1) },
                    { 104, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 1, new DateOnly(1900, 1, 1) },
                    { 201, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 2, new DateOnly(1900, 1, 1) },
                    { 202, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 2, new DateOnly(1900, 1, 1) },
                    { 203, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 2, new DateOnly(1900, 1, 1) },
                    { 204, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 2, new DateOnly(1900, 1, 1) },
                    { 301, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 3, new DateOnly(1900, 1, 1) },
                    { 302, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 3, new DateOnly(1900, 1, 1) },
                    { 303, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 3, new DateOnly(1900, 1, 1) },
                    { 304, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 3, new DateOnly(1900, 1, 1) },
                    { 401, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 4, new DateOnly(1900, 1, 1) },
                    { 402, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 4, new DateOnly(1900, 1, 1) },
                    { 403, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 4, new DateOnly(1900, 1, 1) },
                    { 501, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 5, new DateOnly(1900, 1, 1) },
                    { 502, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 5, new DateOnly(1900, 1, 1) },
                    { 503, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 5, new DateOnly(1900, 1, 1) },
                    { 601, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 6, new DateOnly(1900, 1, 1) },
                    { 602, new DateOnly(2024, 5, 30), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed finibus sed purus consequat porta.Praesent vitae tincidunt dolor.", 6, new DateOnly(1900, 1, 1) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Bookings_RoomId",
                table: "tbl_Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Bookings_UserId",
                table: "tbl_Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RoleClaims_RoleId",
                table: "tbl_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "tbl_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RoomAmenity_AmenityId",
                table: "tbl_RoomAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RoomAmenity_RoomId",
                table: "tbl_RoomAmenity",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RoomNumber_RoomId",
                table: "tbl_RoomNumber",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserClaims_UserId",
                table: "tbl_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserLogins_UserId",
                table: "tbl_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserRoles_RoleId",
                table: "tbl_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "tbl_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "tbl_Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Bookings");

            migrationBuilder.DropTable(
                name: "tbl_ResetPassword");

            migrationBuilder.DropTable(
                name: "tbl_RoleClaims");

            migrationBuilder.DropTable(
                name: "tbl_RoomAmenity");

            migrationBuilder.DropTable(
                name: "tbl_RoomNumber");

            migrationBuilder.DropTable(
                name: "tbl_UserClaims");

            migrationBuilder.DropTable(
                name: "tbl_UserLogins");

            migrationBuilder.DropTable(
                name: "tbl_UserRoles");

            migrationBuilder.DropTable(
                name: "tbl_UserTokens");

            migrationBuilder.DropTable(
                name: "tbl_Amenity");

            migrationBuilder.DropTable(
                name: "tbl_Rooms");

            migrationBuilder.DropTable(
                name: "tbl_Roles");

            migrationBuilder.DropTable(
                name: "tbl_Users");
        }
    }
}
