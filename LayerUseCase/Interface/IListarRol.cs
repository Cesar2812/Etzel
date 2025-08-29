using LayerDomainModel;

namespace LayerUseCase.Interface
{
    public interface IListarRol
    {
        public Task<List<DMRol>> ListarTipoRol();
    }
}
