using Renova.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Renova.Persistence
{
    public class RenovaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public RenovaDbContext(DbContextOptions<RenovaDbContext> options) : base(options) { }
        
        public DbSet<RenovaModel> Renova { get; set; }

        public DbSet<UsuarioModel> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RenovaModel>(entity =>
            {
                entity.ToTable("Renova");
                entity.HasKey(p => p.Campo1);
                entity.Property(p => p.Campo1).ValueGeneratedOnAdd();
                entity.Property(p => p.Campo2).HasMaxLength(500).IsRequired();
                entity.Property(p => p.Campo3);
                entity.HasIndex(p => p.Campo1);
            });

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(p => p.Id);
                entity.Property(p=>p.Id).ValueGeneratedOnAdd();
                entity.Property(p=>p.Email).HasMaxLength(200).IsRequired();
                entity.Property(p=>p.SenhaHash).HasMaxLength(256).IsRequired();
                entity.HasIndex(p => p.Email).IsUnique();
            });
        }
    }
}
