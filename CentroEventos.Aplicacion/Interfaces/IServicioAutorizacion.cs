namespace CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enumerativos;
public interface IServicioAutorizacion{
    public bool EstaAutorizado(int id, Permiso permiso);
}