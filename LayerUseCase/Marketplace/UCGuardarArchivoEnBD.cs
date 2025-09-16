using LayerDomainModel;
using LayerUseCase.Interface;
using LayerUseCase.Usuario;

namespace LayerUseCase.Marketplace;

public class UCGuardarArchivoEnBD
{
    private readonly IGuardarArchivoBD _guardarArchivoBD;

    public UCGuardarArchivoEnBD(IGuardarArchivoBD guardarArchivoBD)
    {
        _guardarArchivoBD = guardarArchivoBD;
    }

    public async Task<bool> GuardarArchivoMarketplace(DMRecursosMarketplace objRecurso)
    {
        bool resultado = await _guardarArchivoBD.GuardarArchivoMarketplace(objRecurso);

        return resultado;
    }
}
