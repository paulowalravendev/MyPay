using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShopkeeper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shopkeepers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    email = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    hash = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    salt = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopkeepers", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_shopkeepers_cnpj",
                table: "shopkeepers",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_shopkeepers_email",
                table: "shopkeepers",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shopkeepers");
        }
    }
}
