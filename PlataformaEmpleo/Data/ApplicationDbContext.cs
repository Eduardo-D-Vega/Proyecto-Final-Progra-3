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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PlataformaEmpleo.Models.Candidato> Candidato { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.OfertaEmpleo> OfertaEmpleo { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.Postulacion> Postulacion { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.OfertaPostulacion> OfertaPostulacion { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.CV> CV { get; set; } = default!;
        public DbSet<PlataformaEmpleo.Models.Reclutador> Reclutador { get; set; } = default!;

    

        //método para solucionar los conflictos de errores de cascada
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlataformaEmpleo.Models.OfertaPostulacion>()
                .HasOne(op => op.OfertaEmpleo)
                .WithMany(o => o.Postulaciones)
                .HasForeignKey(op => op.IdOferta)
                .OnDelete(DeleteBehavior.Restrict); //evita el borrado en cascada

            // Configuración para evitar errores de cascada en la relación entre Postulacion y Candidato
            modelBuilder.Entity<PlataformaEmpleo.Models.Postulacion>()
                .HasOne(p => p.Candidato)
                .WithMany(o => o.Postulacion)
                .HasForeignKey(op => op.IdCandidato)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
