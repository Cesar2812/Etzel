using LayerDomainModel;

namespace LayerUseCase.Interface;

public interface IListarEstadoRecurso
{
    public Task<List<DMEstadoRecurso>> ListarEstadoRecurso();
}
