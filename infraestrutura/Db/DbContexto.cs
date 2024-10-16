using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.infraestrutura.Db;

public class DbContexto : DbContext
{    

    private readonly IConfiguration _configuracaoAppSettings;
    public DbContexto(IConfiguration configuracaoAppSettings)
    {
       _configuracaoAppSettings = configuracaoAppSettings;
    }
    public DbSet<Administrador> Administradores { get;set;} = default!;
    public DbSet<Veiculo> Veiculos { get;set;} = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador> ().HasData(
         new Administrador{
             Id = 1,
             Email = "administrador@teste.com",
             Senha = "123456",
             Perfil = "Adm"
         }
        ) ;
    }   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var stringConexao = _configuracaoAppSettings.GetConnectionString("MySql")?. ToString();
        if(!string.IsNullOrEmpty(stringConexao))
        {
        optionsBuilder.UseMySql(
        stringConexao,
        ServerVersion.AutoDetect(stringConexao)
            );
          }
       }
    }
