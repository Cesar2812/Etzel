using LayerDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUsesCases.Rol
{
    public interface IListarRol
    {

        public Task<List<DMRol>> ListarTipoRol();

    }
}
