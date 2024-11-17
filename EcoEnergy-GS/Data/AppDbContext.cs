using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy_GS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<HistoricoPontosModel> HistoricoPontos { get; set; }
        public DbSet<ResidenciaModel> Residencia { get; set; }
        public DbSet<ConsumoEnergiaModel> ConsumoEnergia { get; set; }
        public DbSet<TipoEletrodomesticoModel> TipoEletrodomestico { get; set; }
        public DbSet<EnderecoModel> Endereco { get; set; }
        public DbSet<TrocasRecompensasModel> TrocasRecompensas { get; set; }
        public DbSet<RecompensasModel> Recompensas { get; set; }
    }
}
