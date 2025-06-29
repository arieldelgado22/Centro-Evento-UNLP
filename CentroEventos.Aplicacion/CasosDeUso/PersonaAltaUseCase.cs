using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class PersonaAltaUseCase(IRepositorioPersona repo, PersonaValidador validador, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(Persona persona, int usuarioId)
    {
        if (!autorizacion.EstaAutorizado(usuarioId, Permiso.PersonaAlta))
        {
            throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
        }

        var resultado = validador.Validar(persona);

        switch (resultado.Codigo)
        {
            case CodigoValidacion.SinErrores:
                break;

            case CodigoValidacion.DuplicadoError:
               throw new DuplicadoException(resultado.Mensaje);

            case CodigoValidacion.ValidacionError:
                throw new ValidacionException(resultado.Mensaje);

            default:
                throw new Exception("Código de validación desconocido.");
        }

        repo.AgregarPersona(persona);
    }
}
