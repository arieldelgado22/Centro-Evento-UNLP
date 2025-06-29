using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Repositorios;

public class RepositorioUsuario() : IRepositorioUsuario
{

 public void AgregarUsuario(Usuario p)
{
    if (p is null)
    {
        throw new ArgumentNullException("El usuario no puede ser nulo.");
    }
    
    using var context = new CentroEventosContext();
    

    context.Usuarios.Add(p);
    context.SaveChanges();
    
    if (p.Id == 1)
    {
        var listaDePermisos = new List<Permiso>()
        {
            Permiso.EventoAlta,
            Permiso.EventoModificacion,
            Permiso.EventoBaja,
            Permiso.ReservaAlta,
            Permiso.ReservaModificacion,
            Permiso.ReservaBaja,
            Permiso.GestionUsuarios,
            Permiso.PersonaAlta,
            Permiso.PersonaBaja,
            Permiso.PersonaModificacion
        };
       
        p.Permisos = listaDePermisos;
       
        context.SaveChanges();
    }
}



    public void EliminarUsuario(int id)
    {
        using var context = new CentroEventosContext();
        var usuario = context.Usuarios.Find(id);
        if (usuario is null)
        {
            throw new EntidadNotFoundException("El usuario no se encuentra en el repositorio.");
        }
        context.Usuarios.Remove(usuario);
        context.SaveChanges();
    }

    public bool ExisteUsuarioConPermiso(int id, Permiso permiso)
    {
        using var context = new CentroEventosContext();

        var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);

        return usuario?.Permisos.Contains(permiso) == true;
    }

    public Usuario? ObtenerPorId(int id)
    {
        using var context = new CentroEventosContext();
        return context.Usuarios
            .FirstOrDefault(u => u.Id == id);
    }



    public List<Usuario> ListarUsuario()
    {
        using var context = new CentroEventosContext();
        return context.Usuarios.ToList();
    }

    public void ModificarUsuario(Usuario usuario)
    {
        using var context = new CentroEventosContext();
        var usuarioExistente = context.Usuarios.Find(usuario.Id);
        if (usuarioExistente is null)
        {
            throw new EntidadNotFoundException("El usuario no se encuentra en el repositorio.");
        }
        usuarioExistente.Nombre = usuario.Nombre;
        usuarioExistente.Apellido = usuario.Apellido;
        usuarioExistente.Email = usuario.Email;
        if (!string.IsNullOrEmpty(usuario.Password))
        {
            usuarioExistente.Password = usuario.Password;    
        }
        usuarioExistente.Permisos = [.. usuario.Permisos];
        context.SaveChanges();
    }



    public Usuario? ObtenerUsuario(int id)
    {
        using var context = new CentroEventosContext();
        return context.Usuarios.Find(id);
    }
    public Usuario? ObtenerUsuario(string email)
    {
        using var context = new CentroEventosContext();
        return context.Usuarios.FirstOrDefault(u => u.Email == email);
    }
}
