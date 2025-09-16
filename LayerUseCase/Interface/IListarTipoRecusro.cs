using LayerDomainModel;
namespace LayerUseCase.Interface;


public interface IListarTipoRecusro
{

    public Task<List<DMTipoRecurso>> ListarTipoRecuro();
}
