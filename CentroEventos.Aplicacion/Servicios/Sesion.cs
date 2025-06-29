using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Servicios;

public class Sesion
{
    public Usuario? UsuarioActual { get; set; }
    public event Action? OnSesionCambiada;
    public event Action? OnSesionActualizada;

    public bool EstaLogueado()
    {
        return UsuarioActual != null;
    }

    public void Iniciar(Usuario usuario)
    {
        UsuarioActual = usuario;
        
        OnSesionCambiada?.Invoke();
    }

    public void ActualizarUsuario(Usuario nuevoUsuario)
    {
        UsuarioActual = nuevoUsuario;
        OnSesionCambiada?.Invoke();
        OnSesionActualizada?.Invoke();
  
    }

    public void CerrarSesion()
    {
        UsuarioActual = null;

        OnSesionCambiada?.Invoke();
    }
}