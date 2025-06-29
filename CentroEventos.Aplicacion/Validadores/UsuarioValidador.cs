using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Validadores;

public class UsuarioValidador
{
    private readonly IRepositorioUsuario _repositorio;

    public UsuarioValidador(IRepositorioUsuario repositorio)
    {
        _repositorio = repositorio;
    }

    public bool EsEmailUnico(Usuario usuario)
    {
        var listaUsuario = _repositorio.ListarUsuario();
        return !listaUsuario.Any(p =>
            !string.IsNullOrEmpty(p.Email) &&
            p.Email.Equals(usuario.Email, StringComparison.OrdinalIgnoreCase) && p.Id != usuario.Id);
    }

    public (CodigoValidacion Codigo, string Mensaje) Validar(Usuario usuario, bool esNuevo)
    {
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            return (CodigoValidacion.ValidacionError, "El nombre no puede estar vacío.");

        if (string.IsNullOrEmpty(usuario.Apellido))
        {
            return (CodigoValidacion.ValidacionError, "El apellido no puede estar vacio");
        }
        if (string.IsNullOrEmpty(usuario.Email))
        {
            return (CodigoValidacion.ValidacionError, "El email no puede estar vacío");
        }
        if (!EsEmailUnico(usuario))
        {
            return (CodigoValidacion.DuplicadoError, "El email no puede estar repetido");
        }
        if (esNuevo && string.IsNullOrEmpty(usuario.Password))
        {
            return (CodigoValidacion.ValidacionError, "La contraseña no puede estar vacía");
        }
        return (CodigoValidacion.SinErrores, "");
    }
}