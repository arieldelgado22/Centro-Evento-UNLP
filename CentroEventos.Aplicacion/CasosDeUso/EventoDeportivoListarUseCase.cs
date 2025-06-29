using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosDeUso;
public class EventoDeportivoListarUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo)
{

    public List<EventoDeportivo> Ejecutar(bool eventoPasado = false)
    {
        if (eventoPasado)
        {
            return repositorioEventoDeportivo.ListarEventoDeportivoPasado();
        }
        return repositorioEventoDeportivo.ListarEventoDeportivo();
    }
}