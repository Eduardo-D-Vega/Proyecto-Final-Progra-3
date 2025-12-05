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
    public class OfertaEmpleosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfertaEmpleosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfertaEmpleos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OfertaEmpleo.Include(o => o.Reclutador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OfertaEmpleos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofertaEmpleo = await _context.OfertaEmpleo
                .Include(o => o.Postulaciones) //lista de ofertaEmpelo a OfertaPostulacion
                    .ThenInclude(p => p.Postulaciones) 
                    .ThenInclude(p => p.Candidato)
                .FirstOrDefaultAsync(m => m.IdOferta == id);
            if (ofertaEmpleo == null)
            {
                return NotFound();
            }

            return View(ofertaEmpleo);
        }

        // GET: OfertaEmpleos/Create
        public IActionResult Create()
        {
            ViewData["ReclutadorId"] = new SelectList(_context.Set<Reclutador>(), "IdReclutador", "NombreEmpresa");
            return View();
        }

        // POST: OfertaEmpleos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOferta,Titulo,Descripcion,Requisitos,Ubicacion,FechaPublicacion,FechaCierre,TipoContrato,Salario,Empresa,Horario,ReclutadorId")] OfertaEmpleo ofertaEmpleo)
        {
            try
            {
                _context.Add(ofertaEmpleo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }

            ViewData["ReclutadorId"] = new SelectList(_context.Set<Reclutador>(), "IdReclutador", "IdReclutador", ofertaEmpleo.ReclutadorId);
            return View(ofertaEmpleo);
        }

        // GET: OfertaEmpleos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofertaEmpleo = await _context.OfertaEmpleo.FindAsync(id);
            if (ofertaEmpleo == null)
            {
                return NotFound();
            }
            ViewData["ReclutadorId"] = new SelectList(_context.Set<Reclutador>(), "IdReclutador", "IdReclutador", ofertaEmpleo.ReclutadorId);
            return View(ofertaEmpleo);
        }

        // POST: OfertaEmpleos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOferta,Titulo,Descripcion,Requisitos,Ubicacion,FechaPublicacion,FechaCierre,TipoContrato,Salario,Empresa,Horario,ReclutadorId")] OfertaEmpleo ofertaEmpleo)
        {
            if (id != ofertaEmpleo.IdOferta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ofertaEmpleo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaEmpleoExists(ofertaEmpleo.IdOferta))
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
            ViewData["ReclutadorId"] = new SelectList(_context.Set<Reclutador>(), "IdReclutador", "IdReclutador", ofertaEmpleo.ReclutadorId);
            return View(ofertaEmpleo);
        }

        // GET: OfertaEmpleos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofertaEmpleo = await _context.OfertaEmpleo
                .Include(o => o.Reclutador)
                .FirstOrDefaultAsync(m => m.IdOferta == id);
            if (ofertaEmpleo == null)
            {
                return NotFound();
            }

            return View(ofertaEmpleo);
        }

        // POST: OfertaEmpleos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ofertaEmpleo = await _context.OfertaEmpleo.FindAsync(id);
            if (ofertaEmpleo != null)
            {
                _context.OfertaEmpleo.Remove(ofertaEmpleo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfertaEmpleoExists(int id)
        {
            return _context.OfertaEmpleo.Any(e => e.IdOferta == id);
        }
    }
}
