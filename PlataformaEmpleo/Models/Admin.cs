using System.ComponentModel.DataAnnotations;

namespace PlataformaEmpleo.Models
{
    public class Admin
    {
        [Key]
        public int IdAdmin { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        public string Rol { get; set; } = "Admin";
    }
}

