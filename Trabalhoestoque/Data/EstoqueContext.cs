using Microsoft.EntityFrameworkCore;
using Trabalhoestoque.Models;

namespace Trabalhoestoque.Data
{
    public class EstoqueContext : DbContext
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options)
        {
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
