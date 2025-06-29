using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Validadores;

public class EventoDeportivoValidador
{
    private readonly IRepositorioPersona _repositorio;

    public EventoDeportivoValidador(IRepositorioPersona repositorio)
    {
        _repositorio = repositorio;
    }
    public (CodigoValidacion Codigo, string Mensaje) Validar(EventoDeportivo evento)
    {
        if (string.IsNullOrEmpty(evento.Nombre))
        {
            return (CodigoValidacion.ValidacionError, "El nombre no puede estar vacio");
            
        }
        if (string.IsNullOrEmpty(evento.Descripcion))
        {
            return (CodigoValidacion.ValidacionError, "La descripcion no puede estar vacia");
           
        }
        if (evento.FechaHoraInicio < DateTime.Now)
        {
            return (CodigoValidacion.ValidacionError, "La fecha y hora de inicio no puede ser menor a la fecha y hora actual");

            }
        if (evento.CupoMaximo < 1)
        {

            return (CodigoValidacion.ValidacionError, "El cupo maximo no puede ser menor a 1");
           
        }
        if (evento.DuracionHoras <= 0)
        {
            return (CodigoValidacion.ValidacionError, "La duracion debe ser mayor a 0");
          
        }
        if (_repositorio.ExistePersona(evento.PersonaId) == false)
        {
            return (CodigoValidacion.ValidacionError, "El responsable no existe");
            
        }
            
        return (CodigoValidacion.SinErrores, "");
    }
}