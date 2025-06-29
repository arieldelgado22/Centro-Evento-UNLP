namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IRepositorioEventoDeportivo
{
    void AgregarEventoDeportivo(EventoDeportivo e);
    EventoDeportivo? ObtenerEventoDeportivo(int id);
    void ModificarEventoDeportivo(EventoDeportivo e);
    void EliminarEventoDeportivo(int id);
    bool ExisteEventoDeportivo(int id);
    List<EventoDeportivo> ListarEventoDeportivo();
    List<EventoDeportivo>ListarEventoDeportivoPasado();
}