namespace CentroEventos.Aplicacion.Resultados;
using CentroEventos.Aplicacion.Entidades;   
public class ResultadoLogin
{
    public bool Exitoso => Usuario is not null;
    public string? Error { get; set; }
    public Usuario? Usuario { get; set; }

    public static ResultadoLogin Fallo(string error) => new() { Error = error };
    public static ResultadoLogin Ok(Usuario usuario) => new() { Usuario = usuario };
}