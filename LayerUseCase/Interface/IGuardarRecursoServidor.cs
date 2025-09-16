using LayerDomainModel;


namespace LayerUseCase.Interface;

public interface IGuardarRecursoServidor
{
    public Task<string> SubirRecurso(DMRecursosMarketplace objRecurso);
}
