using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class juncaorollback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JogosAdquiridos_Jogos_jogosIdJogo",
                table: "JogosAdquiridos");

            migrationBuilder.DropForeignKey(
                name: "FK_JogosAdquiridos_Usuarios_UsuarioIdUsuario",
                table: "JogosAdquiridos");

            migrationBuilder.RenameColumn(
                name: "jogosIdJogo",
                table: "JogosAdquiridos",
                newName: "JogosIdJogo");

            migrationBuilder.RenameIndex(
                name: "IX_JogosAdquiridos_jogosIdJogo",
                table: "JogosAdquiridos",
                newName: "IX_JogosAdquiridos_JogosIdJogo");

            migrationBuilder.AlterColumn<Guid>(
                name: "JogosIdJogo",
                table: "JogosAdquiridos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioIdUsuario",
                table: "JogosAdquiridos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_JogosAdquiridos_Jogos_JogosIdJogo",
                table: "JogosAdquiridos",
                column: "JogosIdJogo",
                principalTable: "Jogos",
                principalColumn: "IdJogo");

            migrationBuilder.AddForeignKey(
                name: "FK_JogosAdquiridos_Usuarios_UsuarioIdUsuario",
                table: "JogosAdquiridos",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JogosAdquiridos_Jogos_JogosIdJogo",
                table: "JogosAdquiridos");

            migrationBuilder.DropForeignKey(
                name: "FK_JogosAdquiridos_Usuarios_UsuarioIdUsuario",
                table: "JogosAdquiridos");

            migrationBuilder.RenameColumn(
                name: "JogosIdJogo",
                table: "JogosAdquiridos",
                newName: "jogosIdJogo");

            migrationBuilder.RenameIndex(
                name: "IX_JogosAdquiridos_JogosIdJogo",
                table: "JogosAdquiridos",
                newName: "IX_JogosAdquiridos_jogosIdJogo");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioIdUsuario",
                table: "JogosAdquiridos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "jogosIdJogo",
                table: "JogosAdquiridos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JogosAdquiridos_Jogos_jogosIdJogo",
                table: "JogosAdquiridos",
                column: "jogosIdJogo",
                principalTable: "Jogos",
                principalColumn: "IdJogo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JogosAdquiridos_Usuarios_UsuarioIdUsuario",
                table: "JogosAdquiridos",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
