using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class FixReclutadorField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Empresa",
                table: "OfertaEmpleo");

            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "OfertaEmpleo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Horario",
                table: "OfertaEmpleo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Empresa",
                table: "OfertaEmpleo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
