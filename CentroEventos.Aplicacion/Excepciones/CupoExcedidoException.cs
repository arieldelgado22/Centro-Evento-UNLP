namespace CentroEventos.Aplicacion.Excepciones;
public class CupoExcedidoException : Exception
{
    public CupoExcedidoException() : base("") {}
    public CupoExcedidoException(string message) : base(message) {}

}