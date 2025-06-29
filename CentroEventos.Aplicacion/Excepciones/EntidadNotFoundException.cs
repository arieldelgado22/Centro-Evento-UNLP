namespace CentroEventos.Aplicacion.Excepciones;
public class EntidadNotFoundException : Exception
{
    public EntidadNotFoundException() : base("") { }
    public EntidadNotFoundException(string message) : base(message) { }
}
