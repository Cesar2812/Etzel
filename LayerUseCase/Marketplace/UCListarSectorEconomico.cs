using LayerDomainModel;
using LayerUseCase.Interface;


namespace LayerUseCase.Marketplace
{
    public class UCListarSectorEconomico
    {
        private readonly IListarSectorEconomico _listarSectorEconomico;

        public UCListarSectorEconomico(IListarSectorEconomico listarSector)
        {
            _listarSectorEconomico = listarSector;
        }

        public async Task<List<DMTipoSectorEconomico>> ListarSectorEconomico()
        {
            var listaSector = await _listarSectorEconomico.ListarSectorEconomico();
            return listaSector;
        }
    }
}
