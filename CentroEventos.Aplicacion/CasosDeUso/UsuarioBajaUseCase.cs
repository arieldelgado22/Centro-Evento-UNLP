using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class UsuarioBajaUseCase(IRepositorioUsuario repoUsuario, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(int id, int idUsuario)
    {
        if (id == idUsuario)
        {
            throw new ValidacionException("El usuario no puede eliminarse a sí mismo.");
        }

        if (!autorizacion.EstaAutorizado(idUsuario, Permiso.GestionUsuarios))
        {
            throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
        }
        
        repoUsuario.EliminarUsuario(id);
    }
}