using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;

namespace Renova.Persistence
{
    public class RenovaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public RenovaDbContext(DbContextOptions<RenovaDbContext> options) : base(options) { }
        
        public DbSet<UsuarioModel> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
