namespace CentroEventos.Aplicacion.Excepciones;
public class OperacionInvalidaException : Exception
{
    public OperacionInvalidaException() : base("") {}
    public OperacionInvalidaException(string message) : base(message) {}

}