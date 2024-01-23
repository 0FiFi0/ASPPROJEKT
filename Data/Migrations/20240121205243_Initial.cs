using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fd526199-e29a-48e8-b7ed-a22da1782139", "288c2e7b-b257-4979-972c-2867bf4e9c9b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c9b5ad90-43eb-4968-977d-e3dd5cfc60fc", "7741fcd6-7571-4b61-b1b1-dc9da225fe84" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9b5ad90-43eb-4968-977d-e3dd5cfc60fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd526199-e29a-48e8-b7ed-a22da1782139");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "288c2e7b-b257-4979-972c-2867bf4e9c9b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7741fcd6-7571-4b61-b1b1-dc9da225fe84");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ec86c39-dca2-457d-a7c6-ace69368820e", "3ec86c39-dca2-457d-a7c6-ace69368820e", "admin", "ADMIN" },
                    { "8dd87c75-8444-4a80-b936-4773ffb687d1", "8dd87c75-8444-4a80-b936-4773ffb687d1", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0434aa06-3f84-4646-bacb-89447f528556", 0, "5379b02e-0e00-4fbb-8dfc-37458a093f41", "admin@wsei.edu.pl", true, false, null, "ADMIN@WSEI.EDU.PL", "ADMIN", "AQAAAAIAAYagAAAAEBbslOYzO7hq4gKkmYh+Kxb+UTSaAYMksEIvhrXlKRwDgbeR0H8bJzw2TkqDirGRjQ==", null, false, "fba15038-1ded-4c2c-8013-04c3f28a1ece", false, "admin" },
                    { "86bdab54-9f4e-4cc5-b7a2-4cbfb652970a", 0, "8070041b-070f-4ebb-b585-3de27cee23bd", "user@wsei.edu.pl", true, false, null, "USER@WSEI.EDU.PL", "USER", "AQAAAAIAAYagAAAAEOY0zIY4Evd+L3FG+4XW1PbJQBPcK35cA9Lvn62A15XG/qBRU/zZrbE+ZTeFvcdKTA==", null, false, "cc9f13d1-747f-4f1a-90b2-d3300b348d02", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3ec86c39-dca2-457d-a7c6-ace69368820e", "0434aa06-3f84-4646-bacb-89447f528556" },
                    { "8dd87c75-8444-4a80-b936-4773ffb687d1", "86bdab54-9f4e-4cc5-b7a2-4cbfb652970a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3ec86c39-dca2-457d-a7c6-ace69368820e", "0434aa06-3f84-4646-bacb-89447f528556" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8dd87c75-8444-4a80-b936-4773ffb687d1", "86bdab54-9f4e-4cc5-b7a2-4cbfb652970a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ec86c39-dca2-457d-a7c6-ace69368820e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd87c75-8444-4a80-b936-4773ffb687d1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0434aa06-3f84-4646-bacb-89447f528556");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86bdab54-9f4e-4cc5-b7a2-4cbfb652970a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c9b5ad90-43eb-4968-977d-e3dd5cfc60fc", "c9b5ad90-43eb-4968-977d-e3dd5cfc60fc", "admin", "ADMIN" },
                    { "fd526199-e29a-48e8-b7ed-a22da1782139", "fd526199-e29a-48e8-b7ed-a22da1782139", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "288c2e7b-b257-4979-972c-2867bf4e9c9b", 0, "a6a49342-af02-4213-a8e6-44cfbfa2f027", "user@wsei.edu.pl", true, false, null, "USER@WSEI.EDU.PL", "USER", "AQAAAAIAAYagAAAAEFcO2Tf3XwqTqm42uDX+nxDtsWcKViez+nxlkZODqvbTJ3esZPj0Olxc75WN7T2zWQ==", null, false, "43963635-a203-41b2-816f-116a9630d9b4", false, "user" },
                    { "7741fcd6-7571-4b61-b1b1-dc9da225fe84", 0, "6d8ddcf3-00a9-4205-b9bb-1bbc5af9c019", "admin@wsei.edu.pl", true, false, null, "ADMIN@WSEI.EDU.PL", "ADMIN", "AQAAAAIAAYagAAAAEOA+nbvHmZIvIDQpX11ObkRHWojGj1LrbKmTVbotsrd3NZJqEsrwVCWrgtrbOUN/hg==", null, false, "a2c17dfc-289c-4500-8e2f-045a11dff43d", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "fd526199-e29a-48e8-b7ed-a22da1782139", "288c2e7b-b257-4979-972c-2867bf4e9c9b" },
                    { "c9b5ad90-43eb-4968-977d-e3dd5cfc60fc", "7741fcd6-7571-4b61-b1b1-dc9da225fe84" }
                });
        }
    }
}
