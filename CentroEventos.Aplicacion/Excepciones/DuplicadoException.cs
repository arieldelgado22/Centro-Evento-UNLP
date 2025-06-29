namespace CentroEventos.Aplicacion.Excepciones;
public class DuplicadoException : Exception
{
    public DuplicadoException() : base("") {}
    public DuplicadoException(string message) : base(message) {}

}