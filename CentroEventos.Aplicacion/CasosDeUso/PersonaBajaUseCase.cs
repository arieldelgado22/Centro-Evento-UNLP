using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enumerativos;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class PersonaBajaUseCase(IRepositorioPersona repoPersona, IRepositorioEventoDeportivo repoEvento, IRepositorioReserva repoReserva, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(int id, int idUsuario)
    {
        if (!autorizacion.EstaAutorizado(idUsuario, Permiso.PersonaBaja))
        {
            throw new FalloAutorizacionException("El usuario no cuenta con los permisos necesarios.");
        }

        var eventos = repoEvento.ListarEventoDeportivo();
        foreach (var evento in eventos)
        {
            if (evento.PersonaId == id)
            {
                throw new OperacionInvalidaException("No se puede eliminar la persona porque tiene eventos deportivos asociados.");
            }
        }
        
        var reservas = repoReserva.ListarReserva();
        foreach (var reserva in reservas)
        {
            if (reserva.PersonaId == id)
            {
                throw new OperacionInvalidaException("No se puede eliminar la persona porque tiene reservas asociadas.");
            }
        }
        
        repoPersona.EliminarPersona(id);
    }
}