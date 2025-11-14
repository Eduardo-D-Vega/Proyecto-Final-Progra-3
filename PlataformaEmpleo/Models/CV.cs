using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEmpleo.Models
{
    public class CV
    {
        [Key]
        public int IdCV { get; set; } //PK
        public string FormacionAcademica { get; set; }
        public string ExperienciaLaboral { get; set; }
        public string Habilidades { get; set; }
        public string Idiomas { get; set; }
        public string RutaArchivo { get; set; }

        public int CandidatoId { get; set; } //FK
        [ForeignKey("CandidatoId")]
        [ValidateNever]
        public Candidato Candidato { get; set; }
    }
}
