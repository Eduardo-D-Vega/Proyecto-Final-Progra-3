using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlataformaEmpleo.Models;

namespace PlataformaEmpleo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PlataformaEmpleo.Models.Candidato> Candidato { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.OfertaEmpleo> OfertaEmpleo { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.Postulacion> Postulacion { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.OfertaPostulacion> OfertaPostulacion { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.CV> CV { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.Reclutador> Reclutador { get; set; } = default!;
    }
}
