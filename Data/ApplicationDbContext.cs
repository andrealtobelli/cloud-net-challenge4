using Microsoft.EntityFrameworkCore;
using challenge_3_net.Models;

namespace challenge_3_net.Data
{
    /// <summary>
    /// Contexto do Entity Framework para a aplicação
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Operacao> Operacoes { get; set; }
        public DbSet<StatusMoto> StatusMotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Adicione configurações específicas aqui se necessário
        }
    }
}