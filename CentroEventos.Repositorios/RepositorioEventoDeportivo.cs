using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Repositorios;
public class RepositorioEventoDeportivo() : IRepositorioEventoDeportivo
{
    public void AgregarEventoDeportivo(EventoDeportivo e)
    {
        if (e is null)
        {
            throw new ArgumentNullException("El evento deportivo no puede ser nulo.");
        }
        using var context = new CentroEventosContext();
        context.Eventos.Add(e);
        context.SaveChanges();
    }

    public void EliminarEventoDeportivo(int id)
    {
        using var context = new CentroEventosContext();
        var evento = context.Eventos.Find(id);
        if (evento is null)
        {
            throw new EntidadNotFoundException("El Evento Deportivo no se encuentra en el repositorio.");
        }
        context.Eventos.Remove(evento);
        context.SaveChanges();
    }
     
    public bool ExisteEventoDeportivo(int id)
    {
        using var context = new CentroEventosContext();
        return context.Eventos.Any(e => e.Id == id);
    }

    public List<EventoDeportivo> ListarEventoDeportivo()
    {
        using var context = new CentroEventosContext();
        return context.Eventos
            .Include(e => e.Persona)
            .ToList();
    }

     public List<EventoDeportivo> ListarEventoDeportivoPasado()
    {
        using var context = new CentroEventosContext();
        return context.Eventos
            .Include(e => e.Persona)
            .Where(e => e.FechaHoraInicio < DateTime.Now)
            .ToList();
    }
    public void ModificarEventoDeportivo(EventoDeportivo evento)
    {
        using var context = new CentroEventosContext();
        var eventoExistente = context.Eventos.Find(evento.Id);
        if (eventoExistente is null)
        {
            throw new EntidadNotFoundException("El Evento Deportivo no se encuentra en el repositorio.");
        }
        eventoExistente.Nombre = evento.Nombre;
        eventoExistente.Descripcion = evento.Descripcion;
        eventoExistente.FechaHoraInicio = evento.FechaHoraInicio;
        eventoExistente.DuracionHoras = evento.DuracionHoras;
        eventoExistente.CupoMaximo = evento.CupoMaximo;
        eventoExistente.PersonaId = evento.PersonaId;
        context.SaveChanges();
    }

    public EventoDeportivo? ObtenerEventoDeportivo(int id)
    {
        using var context = new CentroEventosContext();
        return context.Eventos.Find(id);
    }
}