using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosDeUso;
public class ReservaBajaUseCase(IRepositorioReserva repo, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(int id, int IdUsuario)
    {
        if (!autorizacion.EstaAutorizado(IdUsuario, Permiso.ReservaBaja))
            {
                throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
            }

        repo.EliminarReserva(id);
    }
}