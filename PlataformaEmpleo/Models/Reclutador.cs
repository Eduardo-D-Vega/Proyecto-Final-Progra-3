using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEmpleo.Models
{
    public class Reclutador
    {
        [Key]
        public int IdReclutador { get; set; } // PK
        public string NombreEmpresa { get; set; }
        public string CorreoEmpresa { get; set; }

        //Propiedad de navegacion para la relación uno a muchos con ofertas de empleo
        [ValidateNever]
        public ICollection<OfertaEmpleo> OfertasDeTrabajo { get; set; } = new List<OfertaEmpleo>();
    }
}
