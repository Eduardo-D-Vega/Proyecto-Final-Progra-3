using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEmpleo.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        public string Nombre { get; set; }
    }
}
