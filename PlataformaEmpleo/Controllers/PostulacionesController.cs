using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlataformaEmpleo.Data;
using PlataformaEmpleo.Models;
using PlataformaEmpleo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaEmpleo.Controllers
{
    public class PostulacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostulacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Postulaciones
        [Authorize(Roles = "Usuario, Administrador, Reclutador")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Postulacion.Include(p => p.Candidato);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Postulaciones/Details/5
        [Authorize(Roles = "Administrador, Reclutador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postulacion = await _context.Postulacion
                .Include(p => p.Candidato)
                .FirstOrDefaultAsync(m => m.IdPostulacion == id);
            if (postulacion == null)
            {
                return NotFound();
            }

            return View(postulacion);
        }

        // GET: Postulaciones/Create
        [Authorize(Roles = "Usuario, Administrador")]
        public IActionResult Create()
        { 
            //se crea un selectList de la clase enum TipoPostulacion
            var estadoPostulaciones = Enum.GetValues(typeof(TipoPostulacion))
                .Cast<TipoPostulacion>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(), //numero que se guarda en la base de datos
                    Text = e.ToString() //nombre que se muestra en el select
                }).ToList();

            ViewData["EstadoPostulacion"] = estadoPostulaciones;

            ViewData["IdCandidato"] = new SelectList(_context.Candidato, "IdCandidato", "NombreCompleto");
            //
            ViewBag.IdOferta = new SelectList(_context.OfertaEmpleo, "IdOferta", "Titulo");
            ViewBag.EstadoPostulacion = new SelectList(Enum.GetValues(typeof(TipoPostulacion)));
            return View();
        }

        // POST: Postulaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario, Administrador")]
        public async Task<IActionResult> Create([Bind("IdPostulacion,FechaPostulacion,EstadoPostulacion,IdCandidato")] Postulacion postulacion)
        {
            try
            {
                //se guardar la postulación
                _context.Add(postulacion);
                await _context.SaveChangesAsync();
                 
                //se lee el IdOferta que envia el usuario en el formulario
                var idOfertaString = Request.Form["IdOferta"].FirstOrDefault();

                //Si ese valor existe y es un numero válido significa que el usuario selecciono una oferta
                if (!string.IsNullOrEmpty(idOfertaString) && int.TryParse(idOfertaString, out int idOferta))
                {
                    //se crea el registro en la tabla intermedia
                    var ofertaPost = new OfertaPostulacion
                    {
                        IdPostulacion = postulacion.IdPostulacion,
                        IdOferta = idOferta
                    };

                    _context.Add(ofertaPost);
                    await _context.SaveChangesAsync();
                }
                //si el usuario no selecciona una oferta
                else
                {
                    //si la postulación se guarda ahora se borra
                    _context.Postulacion.Remove(postulacion);
                    await _context.SaveChangesAsync();
                    
                    ModelState.AddModelError("", "Seleccione una oferta para poder postularse");
                    
                    
                    ViewData["IdCandidato"] = new SelectList(_context.Candidato, "IdCandidato", "Nombre", postulacion.IdCandidato);
                    ViewData["IdOferta"] = new SelectList(_context.OfertaEmpleo, "IdOferta", "Titulo");
                    return View(postulacion);
                }
                return RedirectToAction(nameof(Index)); 
            }
            catch
            {
                throw;
            }

            //si algo falla se viene aqui y vuelve a cargar los selects list
            ViewData["IdCandidato"] = new SelectList(_context.Candidato, "IdCandidato", "Nombre", postulacion.IdCandidato);
            ViewData["IdOferta"] = new SelectList(_context.OfertaEmpleo, "IdOferta", "Titulo");
            return View(postulacion);
        }

        // GET: Postulaciones/Edit/5
        [Authorize(Roles = "Usuario, Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //se carga la postulacion a editar
            var postulacion = await _context.Postulacion.FindAsync(id);
            if (postulacion == null)
            {
                return NotFound();
            }

            //se crea un selectList de la clase enum TipoPostulacion
            var estadoPostulaciones = Enum.GetValues(typeof(TipoPostulacion))
                .Cast<TipoPostulacion>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(), //numero que se guarda en la base de datos
                    Text = e.ToString(), 
                    Selected = (e == postulacion.EstadoPostulacion) //selecciona el valor actual
                }).ToList();

            ViewData["EstadoPostulacion"] = estadoPostulaciones;
            
            ViewData["IdCandidato"] = new SelectList(_context.Candidato, "IdCandidato", "NombreCompleto", postulacion.IdCandidato);
            return View(postulacion);
        }

        // POST: Postulaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario, Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("IdPostulacion,FechaPostulacion,EstadoPostulacion,IdCandidato")] Postulacion postulacion)
        {
            if (id != postulacion.IdPostulacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postulacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostulacionExists(postulacion.IdPostulacion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidato"] = new SelectList(_context.Candidato, "IdCandidato", "IdCandidato", postulacion.IdCandidato);
            return View(postulacion);
        }

        // GET: Postulaciones/Delete/5
        [Authorize(Roles = "Usuario, Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postulacion = await _context.Postulacion
                .Include(p => p.Candidato)
                .FirstOrDefaultAsync(m => m.IdPostulacion == id);
            if (postulacion == null)
            {
                return NotFound();
            }

            return View(postulacion);
        }

        // POST: Postulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario, Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postulacion = await _context.Postulacion.FindAsync(id);
            if (postulacion != null)
            {
                _context.Postulacion.Remove(postulacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostulacionExists(int id)
        {
            return _context.Postulacion.Any(e => e.IdPostulacion == id);
        }
    }
}
