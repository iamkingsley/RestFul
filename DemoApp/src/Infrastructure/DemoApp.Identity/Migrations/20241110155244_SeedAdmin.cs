using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApp.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "MiddleNames", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImage", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8701d88a-9f03-4828-8208-f91a9e9dd16d", 0, "c185e8c7-e9df-4531-a2d4-2b942290a7dc", new DateTime(2024, 11, 10, 15, 52, 43, 539, DateTimeKind.Utc).AddTicks(7587), "iamkingsley@admin.com", true, null, true, null, null, "Bernard", "IAMKINGSLEY@ADMIN.COM", "IAMKINGSLEY", "AQAAAAIAAYagAAAAEEVkTfXF/m4GUUji2I6blTC0/Q0++vFLSsdl4iWSkB+ngWh3MeNSgcjV60u2DCquIQ==", null, false, null, "2e7620f1-c678-4c63-8172-3adea22aee49", "Codjoe", false, "iamkingsley" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8701d88a-9f03-4828-8208-f91a9e9dd16d");
        }
    }
}
