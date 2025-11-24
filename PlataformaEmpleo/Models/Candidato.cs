using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEmpleo.Models
{
    public class Candidato
    {
        [Key]
        public int IdCandidato { get; set; } // PK
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [StringLength(12)]
        public string Telefono { get; set; }
        public string Ciudad { get; set; }

        //propiedad de navegacion uno a uno con CV
        [ValidateNever]  // → evita problemas al validar el modelo 
        public CV cv { get; set; }

        //Propiedad de navegacion para la relación MUCHOS a MUCHOS con Postulacion
        [ValidateNever]
        public ICollection<Postulacion> postulaciones { get; set; }


        [NotMapped] //→ indica que esta propiedad no se guarda en la BD
        public string NombreCompleto
        {
            get 
            {
                //se unen los campos nombre y apellido 
                return $"{Nombre} {Apellido}";
            }
        }

    }
}
