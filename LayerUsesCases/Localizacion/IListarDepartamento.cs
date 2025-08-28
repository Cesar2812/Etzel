using LayerDomainModel;

namespace LayerUsesCases.Localizacion
{
    public interface IListarDepartamento
    {
        public Task<List<DMDepartamento>> ListaDepartament();
    }
}
