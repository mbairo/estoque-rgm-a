using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace estoqueRgm.Migrations
{
    public partial class AddedDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dataHora",
                table: "Produtos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ControleEstoque",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    categoria = table.Column<string>(type: "TEXT", nullable: true),
                    modelo = table.Column<string>(type: "TEXT", nullable: true),
                    quantidadeTotal = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleEstoque", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Filiais",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nomeFilial = table.Column<string>(type: "TEXT", nullable: true),
                    categoria = table.Column<string>(type: "TEXT", nullable: true),
                    modelo = table.Column<string>(type: "TEXT", nullable: true),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filiais", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleEstoque");

            migrationBuilder.DropTable(
                name: "Filiais");

            migrationBuilder.DropColumn(
                name: "dataHora",
                table: "Produtos");
        }
    }
}
