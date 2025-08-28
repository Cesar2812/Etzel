

using LayerDomainModel;

namespace LayerUseCase.Usuario
{
    public interface ICrearCuenta
    {

        public Task<int> CrearCuentaUsuario(DMUsuario objetoUsuario);

    }
}
