namespace CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Resultados;
using System.Security.Cryptography;
using System.Text;

public class UsuarioLoginUseCase(IRepositorioUsuario repo)
{
    public ResultadoLogin Ejecutar(string email, string contraseña)
    {
        var usuario = repo.ObtenerUsuario(email);
        if (usuario is null)
        {
            return ResultadoLogin.Fallo("El usuario no está registrado");
        }

        using var sha256 = SHA256.Create();
        var hash = Convert.ToHexString(sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña)));

        if (usuario.Password != hash)
        {
            return ResultadoLogin.Fallo("Contraseña incorrecta");
        }

        return ResultadoLogin.Ok(usuario);
    }
}