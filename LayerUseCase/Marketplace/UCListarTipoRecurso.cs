using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Marketplace;

public class UCListarTipoRecurso
{
    private readonly IListarTipoRecusro _listarTipoRecurso;

    public UCListarTipoRecurso(IListarTipoRecusro listarTipoRecusro)
    {
        _listarTipoRecurso = listarTipoRecusro;
    }
    public async Task<List<DMTipoRecurso>> ListarTipoRecuro()
    {
        var listTipoRecurso= await _listarTipoRecurso.ListarTipoRecuro();
        return listTipoRecurso;

    }
}
