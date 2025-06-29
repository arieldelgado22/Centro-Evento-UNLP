using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class EventoDeportivoAltaUseCase(IRepositorioEventoDeportivo repo, EventoDeportivoValidador validador, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(EventoDeportivo evento, int IdUsuario)
    {
        if (!autorizacion.EstaAutorizado(IdUsuario, Permiso.EventoAlta))
        {
            throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
        }

        var resultado = validador.Validar(evento);
        if (resultado.Codigo == CodigoValidacion.ValidacionError)
        {
            throw new ValidacionException(resultado.Mensaje);
        }

        repo.AgregarEventoDeportivo(evento);
    }
}