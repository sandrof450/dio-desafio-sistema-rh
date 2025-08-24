
using Microsoft.EntityFrameworkCore;
using sistema_de_rh_API.Models;

namespace sistema_de_rh_API.Contexts
{
    public class RhContext : DbContext
    {
        public RhContext(DbContextOptions<RhContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}