using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Validadores;
public class ReservaValidador
{
    private readonly IRepositorioReserva _repoReserva;
    private readonly IRepositorioPersona _repoPersona;
    private readonly IRepositorioEventoDeportivo _repoEventoDeportivo;

    public ReservaValidador(IRepositorioReserva repoReserva, IRepositorioPersona repoPersona, IRepositorioEventoDeportivo repoEventoDeportivo)
    {
        _repoReserva = repoReserva;
        _repoPersona = repoPersona;
        _repoEventoDeportivo = repoEventoDeportivo;
    }
    private bool YaReservoEvento(Reserva reserva)
    {
        List<Reserva> reservas = _repoReserva.ListarReserva(reserva.PersonaId);
        foreach (Reserva r in reservas)
        {
            if (r.Id != reserva.Id && r.EventoDeportivoId == reserva.EventoDeportivoId) //verifico que el ID sea diferente para permitir que ReservaModificacionUseCase use el mismo método de validación
            {
                return true;
            }
        }
        return false;
    }
    private bool HayCupoDisponible(int idEvento)
    {
        _repoEventoDeportivo.ObtenerEventoDeportivo(idEvento);
        if (_repoEventoDeportivo.ExisteEventoDeportivo(idEvento) == false)
        {
            return false;
        }
        var listaReserva = _repoReserva.ListarReserva();
        int cantidadReservas = 0;
        foreach (var reserva in listaReserva)
        {
            if (reserva.EventoDeportivoId == idEvento)
            {
                cantidadReservas++;
            }
        }
        return cantidadReservas < _repoEventoDeportivo.ObtenerEventoDeportivo(idEvento)?.CupoMaximo;
    }
    public (CodigoValidacion Codigo, string Mensaje) Validar(Reserva reserva)
    {
        if (_repoPersona.ExistePersona(reserva.PersonaId) == false)
        {
             return (CodigoValidacion.ValidacionError, "La persona no existe.");
        }
        if (_repoEventoDeportivo.ExisteEventoDeportivo(reserva.EventoDeportivoId) == false)
        {
            return (CodigoValidacion.ValidacionError, "El evento ingresado no existe.");
            
        }
        if (YaReservoEvento(reserva))
        {
            return (CodigoValidacion.DuplicadoError, "La persona ya tiene una reserva para el evento seleccionado.");
            
        }
        if (!HayCupoDisponible(reserva.EventoDeportivoId))
        {
            return (CodigoValidacion.CupoExcedido, "No hay cupo disponible para el evento seleccionado.");
            
        }

        return (CodigoValidacion.SinErrores, "");
    }
}