using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class EventoDeportivoBajaUseCase(IRepositorioEventoDeportivo repoEvento, IRepositorioReserva repoReserva, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(int id, int IdUsuario)
    {
        if (!autorizacion.EstaAutorizado(IdUsuario, Permiso.EventoBaja))
        {
            throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
        }

        var reservas = repoReserva.ListarReserva();
        foreach (var reserva in reservas)
        {
            if (reserva.EventoDeportivoId == id)
            {
                throw new OperacionInvalidaException("No se puede eliminar el evento deportivo porque tiene reservas asociadas.");
            }
        }
        
        repoEvento.EliminarEventoDeportivo(id);
    }
}