using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Usuario;

public class UCcrearCuentaUser
{
    private readonly ICrearCuenta _crearCuenta;

    public UCcrearCuentaUser(ICrearCuenta crearCuenta)
    {
        _crearCuenta = crearCuenta;
    }

    public async Task<int> CrearCuenta(DMUsuario usuario)
    {
        if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrEmpty(usuario.Clave_hash))
        {
            throw new ApplicationException("El correo y la clave son obligatorios.");
        }
        else if (string.IsNullOrEmpty(usuario.NombrePersona))
        {
            throw new ApplicationException("Es Obligatorio Su Nombre");
        }

        usuario.Clave_hash = BCrypt.Net.BCrypt.HashPassword(usuario.Clave_hash);
        int resultado = await _crearCuenta.CrearCuentaUsuario(usuario);

        return resultado;
    }
}
