using LayerDomainModel;

namespace LayerUseCase.Localizacion
{
    public interface IObtenerMunicipio
    {
        public Task<List<DMMunicipio>> ObtenerMunicipi(string idDepartamento);
    }
}
