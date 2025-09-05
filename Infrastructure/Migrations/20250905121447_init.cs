using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "musteri_tanim_table",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UNVAN = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musteri_tanim_table", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "musteri_fatura_table",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MUSTERI_ID = table.Column<int>(type: "integer", nullable: false),
                    FATURA_TARIHI = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FATURA_TUTARI = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ODEME_TARIHI = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musteri_fatura_table", x => x.ID);
                    table.ForeignKey(
                        name: "FK_musteri_fatura_table_musteri_tanim_table_MUSTERI_ID",
                        column: x => x.MUSTERI_ID,
                        principalTable: "musteri_tanim_table",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_musteri_fatura_table_MUSTERI_ID",
                table: "musteri_fatura_table",
                column: "MUSTERI_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "musteri_fatura_table");

            migrationBuilder.DropTable(
                name: "musteri_tanim_table");
        }
    }
}
