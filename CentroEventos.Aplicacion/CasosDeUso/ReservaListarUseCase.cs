using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class ReservaListarUseCase(IRepositorioReserva repositorioReserva)
{

    public List<Reserva> Ejecutar()
    {
        return repositorioReserva.ListarReserva();
    }
}