using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Marketplace;

public class UCListarRecursosMarketplaceUsuario
{
    private readonly IMostrarRecursosUsuario _mostrarRecursos;

    public UCListarRecursosMarketplaceUsuario(IMostrarRecursosUsuario mostrarRecursos)
    {
        _mostrarRecursos = mostrarRecursos;
    }

    public async Task<List<DMUsuarioRecursosMarketplace>> ListarRecursoMarketplaceUsuario(int idUsuario)
    {
        var listaRecursoUusario = await _mostrarRecursos.ListarRecursoMarketplaceUsuario(idUsuario);

        return listaRecursoUusario;
    }
}
