using LayerDomainModel;


namespace LayerUseCase.Interface;

public interface IListarSectorEconomico
{
    public Task<List<DMTipoSectorEconomico>> ListarSectorEconomico();
}
