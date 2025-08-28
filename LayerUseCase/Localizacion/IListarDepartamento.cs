using LayerDomainModel;

namespace LayerUseCase.Localizacion
{
    public interface IListarDepartamento
    {
        public Task<List<DMDepartamento>> ListaDepartament();


    }
}
