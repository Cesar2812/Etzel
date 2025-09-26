using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Usuario;

public class UCObtenerMunicipio
{
    private readonly IObtenerMunicipio _obtenerMunicipio;    

    public UCObtenerMunicipio( IObtenerMunicipio obtenerMunicipio)
    {
        _obtenerMunicipio = obtenerMunicipio;
    }

    public async Task<List<DMMunicipio>> ObtenerMunicipi(int idDepartamento)
    {
        var listaMunicipio = await _obtenerMunicipio.ObtenerMunicipi(idDepartamento);
        return listaMunicipio;
    }
}
