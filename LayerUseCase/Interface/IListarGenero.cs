

using LayerDomainModel;

namespace LayerUseCase.Interface;

public interface IListarGenero
{
    public Task<List<DMGenero>> ListarGenero();
}
