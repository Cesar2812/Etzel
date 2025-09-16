using LayerDomainModel;
using LayerUseCase.Interface;


namespace LayerUseCase.Marketplace;

public class UCAgregarRecurso
{
    private readonly ICrearRecursoMarketplace _crearRecurso;

    public UCAgregarRecurso( ICrearRecursoMarketplace crearRecurso)
    {
        _crearRecurso = crearRecurso;
    }
    public async Task<int> CrearRecursosMarketplace(DMRecursosMarketplace objetoRecursoMarketplace, int idUsuario)
    {
        int resultado = await _crearRecurso.CrearRecursosMarketplace(objetoRecursoMarketplace,idUsuario);
        return resultado;
    }
}
