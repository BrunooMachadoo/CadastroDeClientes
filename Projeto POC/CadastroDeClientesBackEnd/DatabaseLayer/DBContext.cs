using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Numerics;
using static CadastroDeClientesBackEnd.DatabaseLayer.Entity;

namespace CadastroDeClientesBackEnd.DatabaseLayer
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ClienteEndereco> ClienteEndereco { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasKey(x => x.Id);
            modelBuilder.Entity<Cliente>().Property(m => m.Logotipo).IsRequired(false);
            //Um cliente não pode se registrar duas vezes com o mesmo endereço de e-mail
            modelBuilder.Entity<Cliente>().HasIndex(p => p.Email).IsUnique();

            modelBuilder.Entity<Endereco>().HasKey(x => x.Id);
            modelBuilder.Entity<Endereco>().HasIndex(p => p.Logradouro).IsUnique();

            modelBuilder.Entity<ClienteEndereco>().HasKey(bc => new { bc.ClienteId, bc.EnderecoId });
            modelBuilder.Entity<ClienteEndereco>().HasIndex(bc => new { bc.ClienteId, bc.EnderecoId }).IsUnique();

            //Um cliente pode ter vários logradouros.
            modelBuilder.Entity<ClienteEndereco>().HasOne(e => e.Endereco).WithMany(c => c.Enderecos).HasForeignKey(bc => bc.EnderecoId);
            modelBuilder.Entity<ClienteEndereco>().HasOne(c => c.Cliente).WithMany(b => b.Enderecos).HasForeignKey(bc => bc.ClienteId);
        }

    }
}
