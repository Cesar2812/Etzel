using LayerDomainModel;

namespace LayerUseCase.Interface
{
    public interface IListarDepartamento
    {
        public Task<List<DMDepartamento>> ListaDepartament();


    }
}
