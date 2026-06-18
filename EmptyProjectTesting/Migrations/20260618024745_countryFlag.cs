using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmptyProjectTesting.Migrations
{
    /// <inheritdoc />
    public partial class countryFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_CountryFlag_CountryCode",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryFlag",
                table: "CountryFlag");

            migrationBuilder.RenameTable(
                name: "CountryFlag",
                newName: "countryFlag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_countryFlag",
                table: "countryFlag",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_countryFlag_CountryCode",
                table: "Students",
                column: "CountryCode",
                principalTable: "countryFlag",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_countryFlag_CountryCode",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_countryFlag",
                table: "countryFlag");

            migrationBuilder.RenameTable(
                name: "countryFlag",
                newName: "CountryFlag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryFlag",
                table: "CountryFlag",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CountryFlag_CountryCode",
                table: "Students",
                column: "CountryCode",
                principalTable: "CountryFlag",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
