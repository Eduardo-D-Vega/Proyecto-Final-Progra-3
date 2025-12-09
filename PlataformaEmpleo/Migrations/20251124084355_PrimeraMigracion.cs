using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidato",
                columns: table => new
                {
                    IdCandidato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidato", x => x.IdCandidato);
                });

            migrationBuilder.CreateTable(
                name: "Reclutador",
                columns: table => new
                {
                    IdReclutador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reclutador", x => x.IdReclutador);
                });

            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    IdCV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilProfesional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormacionAcademica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienciaLaboral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Habilidades = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idiomas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certificaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidatoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV", x => x.IdCV);
                    table.ForeignKey(
                        name: "FK_CV_Candidato_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidato",
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postulacion",
                columns: table => new
                {
                    IdPostulacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaPostulacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoPostulacion = table.Column<int>(type: "int", nullable: false),
                    IdCandidato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postulacion", x => x.IdPostulacion);
                    table.ForeignKey(
                        name: "FK_Postulacion_Candidato_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidato",
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfertaEmpleo",
                columns: table => new
                {
                    IdOferta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requisitos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoContrato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReclutadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertaEmpleo", x => x.IdOferta);
                    table.ForeignKey(
                        name: "FK_OfertaEmpleo_Reclutador_ReclutadorId",
                        column: x => x.ReclutadorId,
                        principalTable: "Reclutador",
                        principalColumn: "IdReclutador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfertaPostulacion",
                columns: table => new
                {
                    OfertasPostulacionesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPostulacion = table.Column<int>(type: "int", nullable: false),
                    IdOferta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertaPostulacion", x => x.OfertasPostulacionesId);
                    table.ForeignKey(
                        name: "FK_OfertaPostulacion_OfertaEmpleo_IdOferta",
                        column: x => x.IdOferta,
                        principalTable: "OfertaEmpleo",
                        principalColumn: "IdOferta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfertaPostulacion_Postulacion_IdPostulacion",
                        column: x => x.IdPostulacion,
                        principalTable: "Postulacion",
                        principalColumn: "IdPostulacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CV_CandidatoId",
                table: "CV",
                column: "CandidatoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfertaEmpleo_ReclutadorId",
                table: "OfertaEmpleo",
                column: "ReclutadorId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaPostulacion_IdOferta",
                table: "OfertaPostulacion",
                column: "IdOferta");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaPostulacion_IdPostulacion",
                table: "OfertaPostulacion",
                column: "IdPostulacion");

            migrationBuilder.CreateIndex(
                name: "IX_Postulacion_IdCandidato",
                table: "Postulacion",
                column: "IdCandidato");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "OfertaPostulacion");

            migrationBuilder.DropTable(
                name: "OfertaEmpleo");

            migrationBuilder.DropTable(
                name: "Postulacion");

            migrationBuilder.DropTable(
                name: "Reclutador");

            migrationBuilder.DropTable(
                name: "Candidato");
        }
    }
}
