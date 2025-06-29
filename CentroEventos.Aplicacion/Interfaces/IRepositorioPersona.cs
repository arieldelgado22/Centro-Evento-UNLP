using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
public interface IRepositorioPersona
{
    public void AgregarPersona(Persona persona);

    public void EliminarPersona(int id);

    public void ModificarPersona(Persona persona);
    
    public bool ExistePersona(int id);
    public Persona? ObtenerPersona(int id);
    List<Persona> ListarPersona();
}
