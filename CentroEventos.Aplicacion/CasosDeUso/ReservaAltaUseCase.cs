using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;
namespace CentroEventos.Aplicacion.CasosDeUso;
public class ReservaAltaUseCase(IRepositorioReserva repo, ReservaValidador validador, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(Reserva reserva, int idUsuario)
    {
        if (!autorizacion.EstaAutorizado(idUsuario, Permiso.ReservaAlta))
        {
            throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
        }

        var resultado = validador.Validar(reserva);

        switch (resultado.Codigo)
        {
            case CodigoValidacion.SinErrores:
                break;

            case CodigoValidacion.ValidacionError:
                throw new EntidadNotFoundException(resultado.Mensaje);

            case CodigoValidacion.DuplicadoError:
                throw new DuplicadoException(resultado.Mensaje);
            case CodigoValidacion.CupoExcedido:
                throw new ValidacionException(resultado.Mensaje);

            default:
                throw new Exception("Código de validación desconocido.");
        }

        reserva.FechaAltaReserva = DateTime.Now;
        repo.AgregarReserva(reserva);
    }
}