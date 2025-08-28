using LayerDomainModel;

namespace LayerUseCase.Usuario
{
    public interface IListar
    {
        public Task<List<DMUsuario>> ListarUsuario();

    }
}
