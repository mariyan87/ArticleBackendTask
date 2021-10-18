using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessImpl.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Individual");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Individual");

            migrationBuilder.InsertData(
                table: "Individual",
                columns: new[] { "Id", "CreatedOn", "Email", "ModifiedOn" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@SharpIT.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Individual",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Individual",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Individual",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
