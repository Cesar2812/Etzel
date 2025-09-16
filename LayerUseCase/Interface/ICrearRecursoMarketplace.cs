using LayerDomainModel;

namespace LayerUseCase.Interface;

public interface ICrearRecursoMarketplace
{
    public Task<int> CrearRecursosMarketplace(DMRecursosMarketplace objetoRecursoMarketplace,int idUsuario);

}
