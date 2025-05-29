using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class juncao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JogosAdquiridos",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdJogo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioIdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    jogosIdJogo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogosAdquiridos", x => new { x.IdUsuario, x.IdJogo });
                    table.ForeignKey(
                        name: "FK_JogosAdquiridos_Jogos_jogosIdJogo",
                        column: x => x.jogosIdJogo,
                        principalTable: "Jogos",
                        principalColumn: "IdJogo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogosAdquiridos_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JogosAdquiridos_jogosIdJogo",
                table: "JogosAdquiridos",
                column: "jogosIdJogo");

            migrationBuilder.CreateIndex(
                name: "IX_JogosAdquiridos_UsuarioIdUsuario",
                table: "JogosAdquiridos",
                column: "UsuarioIdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JogosAdquiridos");
        }
    }
}
