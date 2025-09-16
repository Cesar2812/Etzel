using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Usuario;

public class UCListarGenero
{
    private readonly IListarGenero _listarGenero;
    public UCListarGenero(IListarGenero listarGenero)
    {
       _listarGenero = listarGenero;
    }
    public async Task<List<DMGenero>> ListarGenero()
    {
        var listaGenero=await _listarGenero.ListarGenero();
        return listaGenero;
    }
}
