using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEmpleo.Models
{
    public class OfertaPostulacion  //tabla intermedia
    {
        [Key]
        public int OfertasPostulacionesId { get; set; }

        //FK de Postulacion y Oferta
        public int IdPostulacion { get; set; }
        public int IdOferta { get; set; }

        //Propiedades de navegación entre oferta y postulacion
        [ForeignKey("IdPostulacion")]
        public Postulacion Postulaciones { get; set; }

        [ForeignKey("IdOferta")]
        public OfertaEmpleo OfertaEmpleo { get; set; }
    }
}
