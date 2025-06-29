using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class UsuarioListarUseCase(IRepositorioUsuario repositorioUsuario)
{

    public List<Usuario> Ejecutar()
    {
        List<Usuario> usuarios = repositorioUsuario.ListarUsuario();
        if (usuarios.Count == 0)
        {
            throw new EntidadNotFoundException("No hay usuarios registrados.");
        }

        return usuarios;
    }
}