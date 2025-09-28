using LayerDomainModel;


namespace LayerUseCase.Interface;

public interface IFilttarTipoRecursoSector
{
    public Task<List<DMTipoRecurso>> ListarRecursoSector(int idSector);

}
