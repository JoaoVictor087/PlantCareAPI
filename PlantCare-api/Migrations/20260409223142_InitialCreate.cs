using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APP_USUARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_USUARIOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "APP_PLANTAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Especie = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ImgLink = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Umidade = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Temperatura = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_PLANTAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APP_PLANTAS_APP_USUARIOS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "APP_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APP_PLANTAS_UsuarioId",
                table: "APP_PLANTAS",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APP_PLANTAS");

            migrationBuilder.DropTable(
                name: "APP_USUARIOS");
        }
    }
}
