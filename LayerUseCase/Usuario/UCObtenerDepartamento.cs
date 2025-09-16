using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Usuario;

public class UCObtenerDepartamento
{
    private readonly IListarDepartamento _listarDepartamento;

    public UCObtenerDepartamento(IListarDepartamento listarDepartamento)
    {
        _listarDepartamento = listarDepartamento;
    }

    public async Task<List<DMDepartamento>> ListaDepartament()
    {
        var listaDepartamneto= await _listarDepartamento.ListaDepartament();    
        return listaDepartamneto;
    }
}
