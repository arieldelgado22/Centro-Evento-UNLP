using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class ListarEventosConCupoDisponibleUseCase(IRepositorioReserva repositorioReserva, IRepositorioEventoDeportivo repositorioEventoDeportivo)
{
    public List<EventoDeportivo> Ejecutar()
    {
        var eventosConCupo = new List<EventoDeportivo>();
        var listaEvento = repositorioEventoDeportivo.ListarEventoDeportivo();

        foreach (var evento in listaEvento)
        {
            if (HayCupoDisponible(evento.Id))
            {
                eventosConCupo.Add(evento);
            }
        }
        return eventosConCupo;
    }

    private bool HayCupoDisponible(int idEvento)
    {
        repositorioEventoDeportivo.ObtenerEventoDeportivo(idEvento);
        if (repositorioEventoDeportivo.ExisteEventoDeportivo(idEvento) == false)
        {
            return false;
        }
        var listaReserva = repositorioReserva.ListarReserva();
        int cantidadReservas = 0;
        foreach (var reserva in listaReserva)
        {
            if (reserva.EventoDeportivoId == idEvento)
            {
                cantidadReservas++;
            }
        }
        return cantidadReservas < repositorioEventoDeportivo.ObtenerEventoDeportivo(idEvento)?.CupoMaximo;
    }

}