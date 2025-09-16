using LayerDomainModel;


namespace LayerUseCase.Interface;

public interface IGuardarArchivoBD
{
    public Task<bool> GuardarArchivoMarketplace(DMRecursosMarketplace objRecurso);
}
