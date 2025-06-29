
using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Repositorios;
public class CentroEventosContext : DbContext
{
    public DbSet<Persona> Personas { get; set; }
    public DbSet<EventoDeportivo> Eventos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public CentroEventosContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
     .Property(u => u.Permisos)
     .HasConversion(
    v => "[" + string.Join(",", v.Select(p => ((int)p).ToString())) + "]", // Guardar como [0,1]
    v => string.IsNullOrWhiteSpace(v)
        ? new List<Permiso>()
        : v.Trim('[', ']') // quitar corchetes
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => (Permiso)int.Parse(p))
            .ToList()
     );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("data source=CentroEventos.db");
    }

    public static void Inicializar()
    {
        using var context = new CentroEventosContext();

        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Se creó base de datos");
        }

        var connection = context.Database.GetDbConnection();
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA journal_mode=DELETE;";
            command.ExecuteNonQuery();
        }
    }
}