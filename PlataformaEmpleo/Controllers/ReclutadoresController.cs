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
    public class ReclutadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReclutadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reclutadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reclutador.ToListAsync());
        }

        // GET: Reclutadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclutador = await _context.Reclutador
                .FirstOrDefaultAsync(m => m.IdReclutador == id);
            if (reclutador == null)
            {
                return NotFound();
            }

            return View(reclutador);
        }

        // GET: Reclutadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reclutadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReclutador,NombreEmpresa,CorreoEmpresa")] Reclutador reclutador)
        {
            try
            {
                _context.Add(reclutador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }

            return View(reclutador);
        }

        // GET: Reclutadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclutador = await _context.Reclutador.FindAsync(id);
            if (reclutador == null)
            {
                return NotFound();
            }
            return View(reclutador);
        }

        // POST: Reclutadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReclutador,NnombreEmpresa,CorreoEmpresa")] Reclutador reclutador)
        {
            if (id != reclutador.IdReclutador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reclutador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReclutadorExists(reclutador.IdReclutador))
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
            return View(reclutador);
        }

        // GET: Reclutadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclutador = await _context.Reclutador
                .FirstOrDefaultAsync(m => m.IdReclutador == id);
            if (reclutador == null)
            {
                return NotFound();
            }

            return View(reclutador);
        }

        // POST: Reclutadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reclutador = await _context.Reclutador.FindAsync(id);
            if (reclutador != null)
            {
                _context.Reclutador.Remove(reclutador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReclutadorExists(int id)
        {
            return _context.Reclutador.Any(e => e.IdReclutador == id);
        }
    }
}
