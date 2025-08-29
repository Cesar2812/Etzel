using LayerDomainModel;

namespace LayerUseCase.Interface
{
    public interface IGuardarFoto
    {

        public Task<bool> GuardarFotoUsuario(DMUsuario objetoUsuario);

    }
}
