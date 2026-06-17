using Microsoft.EntityFrameworkCore;
using LeveInvestimentos.Core.Entities;

namespace LeveInvestimentos.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(e => e.Email).IsUnique(); 
            });

            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MensagemDescritiva).IsRequired().HasMaxLength(500);
                entity.HasOne(t => t.Subordinado).WithMany().HasForeignKey(t => t.SubordinadoId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}