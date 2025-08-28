using LayerDomainModel;

namespace LayerUsesCases.Usuario
{
    public interface IListarUsuario
    {

        Task<List<DMUsuario>> ListarUsuario();

    }
}
