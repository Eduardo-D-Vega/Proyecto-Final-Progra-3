using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEmpleo.Models
{
    public class OfertaEmpleo
    {
        [Key]
        public int IdOferta { get; set; } // PK
        [Required(ErrorMessage = "Debe ingresar un titulo para la oferta de empleo")]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Requisitos { get; set; }
        [Required]
        public string Ubicacion { get; set; }
        [Required]
        public DateTime FechaPublicacion { get; set; }
        [Required]
        public DateTime FechaCierre { get; set; }
        [Required]
        public string TipoContrato { get; set; }
        [Required]
        public decimal Salario { get; set; }
        [Required]
        public string Horario { get; set; }

        //relacion uno a muchos con Reclutador
        [ForeignKey(("ReclutadorId"))]
        public int ReclutadorId { get; set; }
        [ValidateNever]
        public Reclutador Reclutador { get; set; }

        //relacion muchos a muchos con Postulacion
        [ValidateNever]
        public ICollection<Postulacion> postulacionesEmpleos { get; set; } = new List<Postulacion>();
    }
}
