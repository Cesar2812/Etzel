using LayerUseCase.Interface;

namespace LayerUseCase.Usuario;

public class UCRestablecerClave
{
    private readonly IRestablecerClave _restablecerClave;

    private readonly IRecibirCorreo _recibirCorreo;

    public UCRestablecerClave(IRestablecerClave restablecerClave, IRecibirCorreo recibirCorreo)
    {
        _restablecerClave = restablecerClave;
        _recibirCorreo = recibirCorreo;
    }

    public async Task<bool> RestablecerClaveUser(int idUsuario, string correo)
    {
        string nuevaClave = GenerarClave();
        bool resultado = await _restablecerClave.RestablecerClaveUser(idUsuario, BCrypt.Net.BCrypt.HashPassword(nuevaClave));

        if (resultado)
        {
            string asunto = "Clave Recuperada";
            string mensajeCorreo = "<h3> Su Calve fue restablecida correctamente</h3></br><p>Su clave de usuario para ahora acceder es: !clave!</p>";
            mensajeCorreo = mensajeCorreo.Replace("!clave!", nuevaClave);

            bool respuestaCorreo = await _recibirCorreo.RecibirCorreo(correo, asunto, mensajeCorreo);

            if (respuestaCorreo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static string GenerarClave()
    {
        string clave = Guid.NewGuid().ToString("N").Substring(0, 8);
        return clave;
    }
}
