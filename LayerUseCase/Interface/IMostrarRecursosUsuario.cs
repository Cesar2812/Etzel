using LayerDomainModel;

namespace LayerUseCase.Interface;

public interface IMostrarRecursosUsuario
{

    public Task<List<DMUsuarioRecursosMarketplace>> ListarRecursoMarketplaceUsuario(int idUsuario);

}
