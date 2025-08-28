using LayerDomainModel;

namespace LayerUsesCases.Usuario
{
    public interface ICrearCuenta
    {
        Task<int> CrearCuentaUsuario(DMUsuario objetoUsuario);
    }
}
