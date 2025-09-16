using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Marketplace;

public class UCSubirArchivoServidor
{
    private readonly IGuardarRecursoServidor _guardarRecursoServidor;

    public UCSubirArchivoServidor(IGuardarRecursoServidor guardarRecursoServidor)
    {
        _guardarRecursoServidor = guardarRecursoServidor;
    }

    public async Task<string> SubirRecurso(DMRecursosMarketplace objRecurso)
    {
        return await _guardarRecursoServidor.SubirRecurso(objRecurso);
    }
}
