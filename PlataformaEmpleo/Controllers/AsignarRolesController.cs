using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding;
using PlataformaEmpleo.Models;

public class AsignarRolesController : Controller
{
    //roleManager se usa para gestionar los roles
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Usuario> _userManager;

    // constructor
    public AsignarRolesController(

        RoleManager<IdentityRole> roleManager, //gestiona los roles
        UserManager<Usuario> userManager) // gestiona los usuarios
    {
        //se asignan las dependencias inyectadas a los campos privados
        _roleManager = roleManager;
        _userManager = userManager;
    }

    //lista de roles
    public IActionResult Index()
    {
        // se obtienen todos los roles que existen en AspNet roles
        var Roles = _roleManager.Roles.ToList();

        return View(Roles); //va hacia views/asignarRoles/Index.cshtml
    }


    // metodo para mostrar los usuarios para asignarles un rol
    public async Task<IActionResult> Asignar(string RolId)
    {
        //busca el rol por su Id
        var rol = await _roleManager.FindByIdAsync(RolId);

        //si no existe el rol, muestra error 
        if (rol == null)
        {
            return NotFound();
        }

        //envia el nombre y Id del rol a la vista
        ViewBag.RoleName = rol.Name;
        ViewBag.RoleId = rol.Id;

        // obtiene todos los usuarios
        var usuarios = _userManager.Users.ToList();
        var modelo = new List<AsignarRol>();  //lista que se enviará a la vista para mostrar los usuarios y su estado de rol

        //recorre todos los usuarios y verifica si tienen un rol asignado
        foreach (var usuario in usuarios)
        {
            modelo.Add(new AsignarRol
            {
                UserId = usuario.Id,
                Email = usuario.Email,
                Nombre = usuario.Nombre, //se lee el nombre desde usuario y se envia hacia la vista

                //verifica si el usuario tiene el rol asignado
                EstadoRol = await _userManager.IsInRoleAsync(usuario, rol.Name)
            });
        }

        return View(modelo); //apunta a views/asignarRoles/Asignar.cshtml
    }

    //guarda los cambios de asignación
    [HttpPost]
    public async Task<IActionResult> Asignar(string roleId, List<AsignarRol> modelo)
    {
        //busca el rol
        var rol = await _roleManager.FindByIdAsync(roleId);

        if (rol == null)
        {
            return NotFound();
        }

        // se recorre la lista de usuarios envidada desde la vista
        foreach (var item in modelo)
        {
            var User = await _userManager.FindByIdAsync(item.UserId);

            //si el estado del rol es verdadero el usuario tiene rol asignado
            if (item.EstadoRol)
            {
                //si no tiene rol  se le asigna
                if (!await _userManager.IsInRoleAsync(User, rol.Name))
                    await _userManager.AddToRoleAsync(User, rol.Name);
            }
            else
            {
                //si el estado del rol es falso y el usuario tiene rol asignado se elimina el rol
                if (await _userManager.IsInRoleAsync(User, rol.Name))
                    await _userManager.RemoveFromRoleAsync(User, rol.Name);
            }
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Roles(string rolId, string usuarioId)
    {
        var rol = await _roleManager.FindByIdAsync(rolId); //busca el rol por su Id

        var usuario = await _userManager.FindByIdAsync(usuarioId); //busca el usuario por su Id

        if (rol == null)
        {
            return NotFound();
        }

        if (usuario == null)
        {
            return NotFound();
        }

        //
        if (await _userManager.IsInRoleAsync(usuario, rol.Name))
        {
            await _userManager.RemoveFromRoleAsync(usuario, rol.Name);
        }
        //
        else
        {
            await _userManager.AddToRoleAsync(usuario, rol.Name);
        }

        return RedirectToAction("Asignar", new { rolId = rolId });
    }

    //método para eliminar usuario s de la pagina
    [HttpPost] 
    public async Task<IActionResult> EliminarUsuarios(string usuarioId)
    {

        var usuario = await _userManager.FindByIdAsync(usuarioId);

        if (usuario.Id == _userManager.GetUserId(User))
        {
            ModelState.AddModelError("", "Error: No puede eliminar su propia cuenta");
            return RedirectToAction("Index");
        }

        if (usuario == null)
        {
            return NotFound();
        }

        await _userManager.DeleteAsync(usuario);

        return RedirectToAction("Index");
    }
}

