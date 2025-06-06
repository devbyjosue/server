using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class UserRols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voucher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "User", new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Name", "RoleId", "UpdatedAt", "Voucher" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "Josue", 2L, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "v81137" },
                    { 2L, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "Ricardo", 1L, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "nosecual" },
                    { 3L, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", 2L, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "nosecual" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
