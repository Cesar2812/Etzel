using LayerDomainModel;

namespace LayerUsesCases.Usuario
{
    public interface IGuardarFoto
    {
        Task<bool> GuardarFotoUsuario(DMUsuario objetoUsuario);
    }
}
