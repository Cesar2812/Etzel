using LayerDomainModel;

namespace LayerUseCase.Interface
{
    public interface IListar
    {
        public Task<List<DMUsuario>> ListarUsuario();

    }
}
