namespace CentroEventos.Aplicacion.Servicios;

using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Interfaces;
public class ServicioAutorizacion(IRepositorioUsuario repoUsuario) : IServicioAutorizacion
{
    private readonly IRepositorioUsuario _repoUsuario = repoUsuario;

    public bool EstaAutorizado(int id, Permiso permiso)
    {
       return _repoUsuario.ExisteUsuarioConPermiso(id, permiso);
    }
}