namespace CentroEventos.Aplicacion.Entidades;
public class EventoDeportivo
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public DateTime FechaHoraInicio { get; set; }
    public double DuracionHoras { get; set; }
    public int CupoMaximo { get; set; }
    public int PersonaId { get; set; }
    public Persona? Persona { get; set; } // propiedad de navegación para EF Core
    public EventoDeportivo() { }
    public EventoDeportivo(string nombre, string descripcion, DateTime fecha, int cupoMaximo, int personaId)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        FechaHoraInicio = fecha;
        CupoMaximo = cupoMaximo;
        PersonaId = personaId;
    }

    public override string ToString()
    {
        return $"Evento: {Nombre}, Fecha: {FechaHoraInicio}, Cupo: {CupoMaximo}, Descripcion: {Descripcion}";
    }
}