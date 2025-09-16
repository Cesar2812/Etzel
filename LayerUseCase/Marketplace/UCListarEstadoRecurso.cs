using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Marketplace;

public class UCListarEstadoRecurso
{

    private readonly IListarEstadoRecurso _listarEstadoRecurso;

    public UCListarEstadoRecurso(IListarEstadoRecurso listarEstadoRecurso)
    {
        _listarEstadoRecurso = listarEstadoRecurso;
    }

    public async Task<List<DMEstadoRecurso>> ListarEstadoRecurso()
    {
        var listaEtadoRecurso=await _listarEstadoRecurso.ListarEstadoRecurso();
        return listaEtadoRecurso;
    }
}
