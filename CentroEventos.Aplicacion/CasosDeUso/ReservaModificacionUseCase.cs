using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class ReservaModificacionUseCase(IRepositorioReserva repo, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(Reserva reserva, int IdUsuario)
    {
        if (!autorizacion.EstaAutorizado(IdUsuario, Permiso.EventoModificacion))
        {
            throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
        }
        
        try
        {
            repo.ModificarReserva(reserva);
        }
        catch (EntidadNotFoundException e)
        {
            throw new EntidadNotFoundException(e.Message);
        }
    }
}