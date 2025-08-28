using LayerDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUsesCases.Localizacion
{
    public interface IObtenerMunicipio
    {
        public Task<List<DMMunicipio>> ObtenerMunicipi(string idDepartamento);

    }
}
