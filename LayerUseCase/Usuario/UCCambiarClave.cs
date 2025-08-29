
using LayerUseCase.Interface;


namespace LayerUseCase.Usuario;

public class UCCambiarClave
{
    private readonly ICambiarClave _cambiarClave;

    public UCCambiarClave(ICambiarClave cambiarClave)
    {
        _cambiarClave = cambiarClave;
    }
    public async Task<bool> CambiarClaveUser(int idUsuario, string nuevaClave)
    {
        bool resultado = await _cambiarClave.CambiarClaveUser(idUsuario, BCrypt.Net.BCrypt.HashPassword(nuevaClave));
        return resultado;
    }
}
