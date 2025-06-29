using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosDeUso;

public class PersonaListarUseCase(IRepositorioPersona repositorioPersona)
{
    public List<Persona> Ejecutar()
    {
        return repositorioPersona.ListarPersona();
    }
}