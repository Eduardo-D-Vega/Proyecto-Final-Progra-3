using Microsoft.EntityFrameworkCore;

namespace PlataformaEmpleo.Models
{
    [Keyless] //→ se usa Keyless para indicar que este modelo no es una tabla y no se debe mapear a la base de datos
    public class AsignarRol  //sirve para enviar datos a la vista
    {
        public string UserId { get; set; }   // id del usuario para saber a cual usuario se le aplica el rol
        public string Email { get; set; }  
        public bool EstadoRol { get; set; }   // para saber si el usuario tiene el rol asignado o no
        public string Nombre { get; set; }
    }
}
