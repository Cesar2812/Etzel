

using LayerDomainModel;

namespace LayerUseCase.Interface
{
    public interface ICrearCuenta
    {

        public Task<int> CrearCuentaUsuario(DMUsuario objetoUsuario);

    }
}
