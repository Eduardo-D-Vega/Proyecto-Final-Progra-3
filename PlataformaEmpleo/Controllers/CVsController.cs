using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlataformaEmpleo.Data;
using PlataformaEmpleo.Models;

namespace PlataformaEmpleo.Controllers
{
    public class CVsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CVsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CVs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CV.Include(c => c.Candidato);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CVs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV
                .Include(c => c.Candidato)
                .FirstOrDefaultAsync(m => m.IdCV == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // GET: CVs/Create
        public IActionResult Create()
        {
            ViewData["CandidatoId"] = new SelectList(_context.Candidato, "IdCandidato", "NombreCompleto");
            return View();
        }

        // POST: CVs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCV, PerfilProfesional,FormacionAcademica,ExperienciaLaboral,Habilidades,Idiomas,Certificaciones,CandidatoId")] CV cV)
        {
            //validación para evitar un cv duplicado por candidato
            var CvExistente = await _context.CV
                .FirstOrDefaultAsync(x => x.CandidatoId == cV.CandidatoId);
            
            if (CvExistente != null)
            {
                //evita que el programa se caiga
                ModelState.AddModelError("", "El candidato ya tiene un CV registrado. Puede editar el cv ya existente");

                //se devuelve a la vista create con el error
                ViewData["CandidatoId"] = new SelectList(_context.Candidato, "IdCandidato", "NombreCompleto", cV.CandidatoId);
                return View(cV);
            }

            try
            {
                _context.Add(cV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }

            ViewData["CandidatoId"] = new SelectList(_context.Candidato, "IdCandidato", "IdCandidato", cV.CandidatoId);
            return View(cV);
        }

        // GET: CVs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV.FindAsync(id);
            if (cV == null)
            {
                return NotFound();
            }
            ViewData["CandidatoId"] = new SelectList(_context.Candidato, "IdCandidato", "NombreCompleto", cV.CandidatoId);
            return View(cV);
        }

        // POST: CVs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCV,PerfilProfesional,FormacionAcademica,ExperienciaLaboral,Habilidades,Idiomas,Certificaciones,CandidatoId")] CV cV)
        {
            if (id != cV.IdCV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CVExists(cV.IdCV))
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
            ViewData["CandidatoId"] = new SelectList(_context.Candidato, "IdCandidato", "IdCandidato", cV.CandidatoId);
            return View(cV);
        }

        // GET: CVs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV
                .Include(c => c.Candidato)
                .FirstOrDefaultAsync(m => m.IdCV == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // POST: CVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cV = await _context.CV.FindAsync(id);
            if (cV != null)
            {
                _context.CV.Remove(cV);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CVExists(int id)
        {
            return _context.CV.Any(e => e.IdCV == id);
        }
    }
}
