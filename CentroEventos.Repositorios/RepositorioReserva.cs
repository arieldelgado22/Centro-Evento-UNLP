using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Repositorios;
public class RepositorioReserva() : IRepositorioReserva
{
    public void AgregarReserva(Reserva r)
    {
        using var context = new CentroEventosContext();
        if (r is null)
        {
            throw new ArgumentNullException("La reserva no puede ser nula.");
        }
        context.Reservas.Add(r);
        context.SaveChanges();
    }
    public void EliminarReserva(int id)
    {
        using var context = new CentroEventosContext();
        var reserva = context.Reservas.Find(id);
        if (reserva is null)
        {
            throw new EntidadNotFoundException("La reserva no se encuentra en el repositorio.");
        }
        context.Remove(reserva);
        context.SaveChanges();
    }

    public bool ExisteReserva(int id)
    {
        using var context = new CentroEventosContext();
        return context.Reservas.Any(r => r.Id == id);
    }

/// Listar todas las reservas
    public List<Reserva> ListarReserva()
    {
        using var context = new CentroEventosContext();
        return  context.Reservas
    .Include(r => r.EventoDeportivo)  
    .Include(r => r.Persona)          
    .ToList();
    }

    /// Listar reservas por persona
    public List<Reserva> ListarReserva(int personaId)
    {
        using var context = new CentroEventosContext();
        var reservas = context.Reservas.ToList();
        return reservas.Where(r => r.PersonaId == personaId).ToList();
    }

    public void ModificarReserva(Reserva reserva)
    {
        using var context = new CentroEventosContext();
        var reservaExistente = context.Reservas.Find(reserva.Id);
        if (reservaExistente is null)
        {
            throw new EntidadNotFoundException("La reserva no se encuentra en el repositorio.");
        }
        reservaExistente.EstadoAsistencia = reserva.EstadoAsistencia;
        context.SaveChanges();
    }

    public Reserva? ObtenerReserva(int id)
    {
        using var context = new CentroEventosContext();
        return context.Reservas.Find(id);
    }
}
