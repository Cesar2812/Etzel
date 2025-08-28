using LayerDomainModel;

namespace LayerUseCase.Rol
{
    public interface IListarRol
    {


        public Task<List<DMRol>> ListarTipoRol();
    }
}
