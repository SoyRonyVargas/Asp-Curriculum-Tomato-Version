using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_24BM.Models;

namespace Web_24BM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Persona> personas { get; set; }
        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<Experiencia> Experiencia { get; set; }
        public virtual DbSet<Habilidad> Habilidades { get; set; }
        public virtual DbSet<Educacion> Educacion { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Habilidad>()
                .HasOne(h => h.Contacto)
                .WithMany(c => c.Habilidades)
                .HasForeignKey(h => h.ContactoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Experiencia>()
                .HasOne(h => h.Contacto)
                .WithMany(c => c.Experiencia)
                .HasForeignKey(h => h.ContactoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Educacion>()
              .HasOne(h => h.Contacto)
              .WithMany(c => c.Educacion)
              .HasForeignKey(h => h.ContactoId)
              .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}