using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosDeUso;
public class ListarAsistenciaAEventoUseCase(IRepositorioReserva repositorioReserva, IRepositorioEventoDeportivo repositorioEventoDeportivo, IRepositorioPersona repositorioPersona)
{
    public List<Reserva> Ejecutar(int idEvento)
    {
        List<Reserva> reservas = repositorioReserva.ListarReserva();
        EventoDeportivo? evento = repositorioEventoDeportivo.ObtenerEventoDeportivo(idEvento);

        if (evento is null)
        {
            throw new EntidadNotFoundException("El evento no existe.");
        }
        
        if (evento.FechaHoraInicio > DateTime.Now)
        {
            throw new EntidadNotFoundException("Solo puede consultar la asistencia de eventos pasados.");
        }
        
        return repositorioReserva.ListarReserva()
            .Where(r => r.EventoDeportivoId == idEvento)
            .Select(r =>
            {
                r.Persona = repositorioPersona.ObtenerPersona(r.PersonaId);
                return r;
            })
            .ToList();
    }
}