using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Marketplace;

public class UCMostrarRecursosMarketplace
{
    private readonly IMostrarMarketplaceGeneral _mostrarMarketplaceGeneral;

    public UCMostrarRecursosMarketplace(IMostrarMarketplaceGeneral mostrarMarketplaceGeneral)
    {
        _mostrarMarketplaceGeneral = mostrarMarketplaceGeneral;
    }

    public async Task<List<DMUsuarioRecursosMarketplace>> ListarRecursoMarketplace()
    {
        var listaRecursosMarketplace= await _mostrarMarketplaceGeneral.ListarRecursoMarketplace();
        return listaRecursosMarketplace;
    }
}
