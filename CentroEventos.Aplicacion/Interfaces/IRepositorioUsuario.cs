using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;

public interface IRepositorioUsuario
{
    public void  AgregarUsuario(Usuario usuario);


    public Usuario? ObtenerPorId(int id);

    public void EliminarUsuario(int id);

    public void ModificarUsuario(Usuario usuario);
    
    public bool ExisteUsuarioConPermiso(int id,Permiso permiso);
    public Usuario? ObtenerUsuario(int id);
    public Usuario? ObtenerUsuario(string email);
    List<Usuario> ListarUsuario();
}
