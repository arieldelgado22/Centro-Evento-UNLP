using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Repositorios;
public class RepositorioPersona() : IRepositorioPersona
{
    public void AgregarPersona(Persona p)
    {
        if (p is null)
        {
            throw new ArgumentNullException("La persona no puede ser nula.");
        }
        using var context = new CentroEventosContext();
        context.Personas.Add(p);
        context.SaveChanges();
    }
    public void EliminarPersona(int id)
    {
        using var context = new CentroEventosContext();
        var persona = context.Personas.Find(id);
        if (persona is null)
        {
            throw new EntidadNotFoundException("La Persona no se encuentra en el repositorio.");
        }
        context.Personas.Remove(persona);
        context.SaveChanges();
    }

    public bool ExistePersona(int id)
    {
        using var context = new CentroEventosContext();
        return context.Personas.Any(p => p.Id == id);
    }

    public List<Persona> ListarPersona()
    {
        using var context = new CentroEventosContext();
        return context.Personas.ToList();
    }

    public void ModificarPersona(Persona persona)
    {
        using var context = new CentroEventosContext();
        var personaExistente = context.Personas.Find(persona.Id);
        if (personaExistente is null)
        {
            throw new EntidadNotFoundException("La Persona no se encuentra en el repositorio.");
        }
        personaExistente.Nombre = persona.Nombre;
        personaExistente.Apellido = persona.Apellido;
        personaExistente.Dni = persona.Dni;
        personaExistente.Telefono = persona.Telefono;
        personaExistente.Email = persona.Email;
        context.SaveChanges();
    }
    public Persona? ObtenerPersona(int id)
    {
        using var context = new CentroEventosContext();
        return context.Personas.Find(id);
    }
}
