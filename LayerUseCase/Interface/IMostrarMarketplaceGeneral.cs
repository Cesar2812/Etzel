using LayerDomainModel;

namespace LayerUseCase.Interface;

public interface IMostrarMarketplaceGeneral
{
    public Task<List<DMUsuarioRecursosMarketplace>> ListarRecursoMarketplace();
}
