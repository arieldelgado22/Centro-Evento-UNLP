using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class EventoDeportivoModificacionUseCase(IRepositorioEventoDeportivo repo, EventoDeportivoValidador validador, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(EventoDeportivo evento, int IdUsuario)
    {
        if (!autorizacion.EstaAutorizado(IdUsuario, Permiso.EventoModificacion))
        {
            throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
        }

        if (evento.FechaHoraInicio < DateTime.Now)
        {
            throw new OperacionInvalidaException("No se pueden modificar eventos pasados");
        }
        
        var resultado = validador.Validar(evento);
        if (resultado.Codigo == CodigoValidacion.ValidacionError)
        {
            throw new ValidacionException(resultado.Mensaje);
        }

        repo.ModificarEventoDeportivo(evento);
    }
}