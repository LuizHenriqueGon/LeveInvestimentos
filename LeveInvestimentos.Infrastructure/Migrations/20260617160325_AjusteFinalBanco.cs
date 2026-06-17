using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeveInvestimentos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteFinalBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoCaminho",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "TelefoneFixo",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FotoUsuario",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "MensagemDescritiva",
                table: "Tarefas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_SubordinadoId",
                table: "Tarefas",
                column: "SubordinadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_SubordinadoId",
                table: "Tarefas",
                column: "SubordinadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_SubordinadoId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_SubordinadoId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "FotoUsuario",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "TelefoneFixo",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "FotoCaminho",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MensagemDescritiva",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
