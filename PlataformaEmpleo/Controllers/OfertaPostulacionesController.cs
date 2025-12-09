using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlataformaEmpleo.Data;
using PlataformaEmpleo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaEmpleo.Controllers
{
    [Authorize]
    public class OfertaPostulacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfertaPostulacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfertaPostulaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OfertaPostulacion.Include(o => o.OfertaEmpleo).Include(o => o.Postulaciones);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OfertaPostulaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofertaPostulacion = await _context.OfertaPostulacion
                .Include(o => o.OfertaEmpleo)
                .Include(o => o.Postulaciones)
                    .ThenInclude(p => p.Candidato) //incluye el candidato relacionado
                .FirstOrDefaultAsync(m => m.OfertasPostulacionesId == id);

            if (ofertaPostulacion == null)
            {
                return NotFound();
            }

            return View(ofertaPostulacion);
        }

        // GET: OfertaPostulaciones/Create
        public IActionResult Create()
        {
            ViewData["IdOferta"] = new SelectList(_context.OfertaEmpleo, "IdOferta", "Titulo");
            ViewData["IdPostulacion"] = new SelectList(_context.Postulacion, "IdPostulacion", "EstadoPostulacion");
            return View();
        }

        // POST: OfertaPostulaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfertasPostulacionesId,IdPostulacion,IdOferta")] OfertaPostulacion ofertaPostulacion)
        {
            try
            {
                _context.Add(ofertaPostulacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                throw;
            }

            ViewData["IdOferta"] = new SelectList(_context.OfertaEmpleo, "IdOferta", "Descripcion", ofertaPostulacion.IdOferta);
            ViewData["IdPostulacion"] = new SelectList(_context.Postulacion, "IdPostulacion", "IdPostulacion", ofertaPostulacion.IdPostulacion);
            return View(ofertaPostulacion);
        }

        // GET: OfertaPostulaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //busca la relacion ofertaPostulacion que se va a editar
            var ofertaPostulacion = await _context.OfertaPostulacion.FindAsync(id);
            if (ofertaPostulacion == null)
            {
                return NotFound();
            }

            //se contriye la lista de postulaciones con el nombre del candidato y luego lo muestra en el select
            var postulaciones = _context.Postulacion
                .Include(p => p.Candidato) 
                .ToList();

            //la lista anterior se convierte en SelectListItem
            var postulacionesSelect = postulaciones.Select(p => new SelectListItem
            {
                Value = p.IdPostulacion.ToString(),
                Text = p.Candidato != null ? p.Candidato.NombreCompleto : "Sin candidato",
                Selected = p.IdPostulacion == ofertaPostulacion.IdPostulacion //marca el candidato seleccionado
            }).ToList();

            ViewData["IdPostulacion"] = postulacionesSelect; 

            ViewData["IdOferta"] = new SelectList(_context.OfertaEmpleo, "IdOferta", "Descripcion", ofertaPostulacion.IdOferta);
            return View(ofertaPostulacion);
        }

        // POST: OfertaPostulaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfertasPostulacionesId,IdPostulacion,IdOferta")] OfertaPostulacion ofertaPostulacion)
        {
            if (id != ofertaPostulacion.OfertasPostulacionesId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(ofertaPostulacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertaPostulacionExists(ofertaPostulacion.OfertasPostulacionesId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["IdOferta"] = new SelectList(_context.OfertaEmpleo, "IdOferta", "Descripcion", ofertaPostulacion.IdOferta);
            ViewData["IdPostulacion"] = new SelectList(_context.Postulacion, "IdPostulacion", "IdPostulacion", ofertaPostulacion.IdPostulacion);
            return View(ofertaPostulacion);
        }

        // GET: OfertaPostulaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofertaPostulacion = await _context.OfertaPostulacion
                .Include(o => o.OfertaEmpleo)
                .Include(o => o.Postulaciones)
                .FirstOrDefaultAsync(m => m.OfertasPostulacionesId == id);
            if (ofertaPostulacion == null)
            {
                return NotFound();
            }

            return View(ofertaPostulacion);
        }

        // POST: OfertaPostulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ofertaPostulacion = await _context.OfertaPostulacion.FindAsync(id);
            if (ofertaPostulacion != null)
            {
                _context.OfertaPostulacion.Remove(ofertaPostulacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfertaPostulacionExists(int id)
        {
            return _context.OfertaPostulacion.Any(e => e.OfertasPostulacionesId == id);
        }
    }
}
