using LayerDomainModel;
using LayerUseCase.Interface;


namespace LayerUseCase.Usuario;

public class UCListarRol
{
    
    private readonly IListarRol _listarRol;

    public UCListarRol(IListarRol listarRol)
    {
        _listarRol = listarRol;
    }
    public async Task<List<DMRol>> ListarTipoRol()
    {
        var listaRol= await _listarRol.ListarTipoRol();
        return listaRol;
    }
}
