using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaEmpleo.Data;

namespace PlataformaEmpleo.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new AdminDashboardViewModel
            {
                TotalCandidatos = await _context.Candidato.CountAsync(),
                TotalCVs = await _context.OfertaEmpleo.CountAsync(),                  
                TotalOfertaEmpleos = await _context.Postulacion.CountAsync(),
                TotalOfertaPostulaciones = await _context.OfertaPostulacion.CountAsync(),
                TotalPostulaciones = await _context.CV.CountAsync(),
                TotalReclutadores = await _context.Reclutador.CountAsync()
            };

            return View(vm);
        }
    }

    // ViewModel interno simple 
    public class AdminDashboardViewModel
    {
        public int TotalCandidatos { get; set; }
        public int TotalCVs { get; set; }
        public int TotalOfertaEmpleos { get; set; }
        public int TotalOfertaPostulaciones { get; set; }
        public int TotalPostulaciones { get; set; }
        public int TotalReclutadores { get; set; }
    }
}
