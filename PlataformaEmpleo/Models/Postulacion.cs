using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PlataformaEmpleo.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEmpleo.Models
{
    public class Postulacion
    {
        [Key]
        public int IdPostulacion { get; set; } // PK
        public DateTime FechaPostulacion { get; set; }
        public TipoPostulacion EstadoPostulacion { get; set; }


        //relación muchos a uno con Cliente
        public int IdCandidato { get; set; } // FK
        [ForeignKey("IdCandidato")]
        [ValidateNever]
        public Candidato Candidato { get; set; }


        //propiedad de navegacion para la tabla intermedia OfertasPostulaciones
        [ValidateNever]
        public virtual ICollection<OfertaPostulacion> OfertasPostulaciones { get; set; }
    }
}
