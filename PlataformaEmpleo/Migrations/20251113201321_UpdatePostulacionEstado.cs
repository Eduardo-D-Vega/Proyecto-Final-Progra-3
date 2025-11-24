using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostulacionEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EstadoPostulacion",
                table: "Postulacion",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EstadoPostulacion",
                table: "Postulacion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
