using LayerDomainModel;

namespace LayerUseCase.Interface
{
    public interface IObtenerMunicipio
    {
        public Task<List<DMMunicipio>> ObtenerMunicipi(string idDepartamento);
    }
}
