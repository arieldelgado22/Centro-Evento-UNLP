using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Validadores;

public class PersonaValidador
{
    private readonly IRepositorioPersona _repositorio;

    public PersonaValidador(IRepositorioPersona repositorio)
    {
        _repositorio = repositorio;
    }

    public bool EsEmailUnico(Persona persona)
    {
        var listaPersona = _repositorio.ListarPersona();
        return !listaPersona.Any(p =>
            !string.IsNullOrEmpty(p.Email) &&
            p.Email.Equals(persona.Email, StringComparison.OrdinalIgnoreCase) && p.Id != persona.Id);
    }

    public bool EsDNIUnico(Persona persona)
    {
        var listaPersona = _repositorio.ListarPersona();
        return !listaPersona.Any(p =>
            p.Dni.Equals(persona.Dni) && p.Id != persona.Id);
    }

    public (CodigoValidacion Codigo, string Mensaje) Validar(Persona persona)
    {
        if (string.IsNullOrWhiteSpace(persona.Nombre))
            return (CodigoValidacion.ValidacionError, "El nombre no puede estar vacío.");

        if (string.IsNullOrEmpty(persona.Apellido))
        {
            return (CodigoValidacion.ValidacionError, "El apellido no puede estar vacio");
            
        }
        if (string.IsNullOrEmpty(persona.Dni))
        {
            return (CodigoValidacion.ValidacionError, "El DNI no puede estar vacío");
            
        }
        if (string.IsNullOrEmpty(persona.Email))
        {
            return (CodigoValidacion.ValidacionError, "El email no puede estar vacío");
            
        }
        if (!this.EsEmailUnico(persona))
        {
            return (CodigoValidacion.DuplicadoError, "El email no puede estar repetido");
             
        }
        if (!this.EsDNIUnico(persona))
        {
            return (CodigoValidacion.DuplicadoError, "El DNI no puede estar repetido");
        }
        return (CodigoValidacion.SinErrores, "");

    }

 
}