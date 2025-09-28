using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Marketplace;

public class UCListarTipoRecursoSector
{
    private readonly IFilttarTipoRecursoSector _filtrar;

    public UCListarTipoRecursoSector(IFilttarTipoRecursoSector filtrar)
    {
        _filtrar = filtrar;
    }

    public async Task<List<DMTipoRecurso>> ListarRecursoSector(int idSector)
    {
        var listaRecursoSector = await _filtrar.ListarRecursoSector(idSector);

        return listaRecursoSector;
    }
}
