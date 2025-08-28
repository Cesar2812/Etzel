using LayerDomainModel;

namespace LayerUseCase.Usuario
{
    public interface IGuardarFoto
    {

        public Task<bool> GuardarFotoUsuario(DMUsuario objetoUsuario);

    }
}
