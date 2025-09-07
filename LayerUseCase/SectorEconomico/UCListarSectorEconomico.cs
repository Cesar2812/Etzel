using LayerDomainModel;
using LayerUseCase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUseCase.SectorEconomico
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
