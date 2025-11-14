using System.ComponentModel.DataAnnotations;

namespace PlataformaEmpleo.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; } // PK
        public string nombreUsuario { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }

        //sin controlador
    }
}
