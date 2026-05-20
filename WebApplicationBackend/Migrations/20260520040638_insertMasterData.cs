using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplicationBackend.Migrations
{
    /// <inheritdoc />
    public partial class insertMasterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Age", "Email", "Gender", "JoinDate", "MemberName", "Phone" },
                values: new object[,]
                {
                    { 1, 54, "rahul@inc.com", "Male", new DateTime(2004, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rahul Gandhi", "9876543210" },
                    { 2, 82, "kharge@inc.com", "Male", new DateTime(1969, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mallikarjun Kharge", "9876543211" },
                    { 3, 53, "priyanka@inc.com", "Female", new DateTime(1999, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Priyanka Gandhi", "9876543212" },
                    { 4, 70, "tharoor@inc.com", "Male", new DateTime(2009, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shashi Tharoor", "9876543213" },
                    { 5, 48, "pilot@inc.com", "Male", new DateTime(2004, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sachin Pilot", "9876543214" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
