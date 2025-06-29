using System.Security.Cryptography;
using System.Text;
using CentroEventos.Aplicacion.Enumerativos;
namespace CentroEventos.Aplicacion.Entidades;

public class Usuario
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<Permiso> Permisos { get; set; } = new List<Permiso>();
    public Usuario() { }

    public Usuario(string nombre, string apellido, string email, string password)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        EstablecerContraseña(password);
    }
    public void EstablecerContraseña(string? password)
    {
        if (string.IsNullOrEmpty(password))
        {
            Password = password;
            return;
        }
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);
        Password = Convert.ToHexString(hash);
    }

    public void AgregarPermiso(Permiso permiso)
    {
        if (!Permisos.Contains(permiso))
        {
            Permisos.Add(permiso);
        }
    }

    public override string ToString()
    {
        return $"Usuario: {Nombre} {Apellido}, Email: {Email}";
    }
}