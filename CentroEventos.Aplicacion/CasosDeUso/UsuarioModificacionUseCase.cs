using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class UsuarioModificacionUseCase(IRepositorioUsuario repo, UsuarioValidador validador, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(Usuario usuario, int usuarioId)
    {
        var usuarioActual = repo.ObtenerUsuario(usuario.Id);
        if (usuarioActual is null)
        {
            throw new EntidadNotFoundException("Usuario no encontrado.");
        }

        // si el usuario se quiere modificar a si mismo no se requieren permisos
        if (usuarioActual.Id != usuario.Id &&
            !autorizacion.EstaAutorizado(usuarioId, Permiso.GestionUsuarios))
        {
            throw new FalloAutorizacionException("error Autorizacion");
        }

        var resultado = validador.Validar(usuario, false);
        switch (resultado.Codigo)
        {
            case CodigoValidacion.SinErrores:
                break;

            case CodigoValidacion.DuplicadoError:
                throw new DuplicadoException(resultado.Mensaje);

            case CodigoValidacion.ValidacionError:
                throw new ValidacionException(resultado.Mensaje);

            default:
                throw new Exception("Codigo de validacion no reconocido.");
        }
        repo.ModificarUsuario(usuario);
    }
}