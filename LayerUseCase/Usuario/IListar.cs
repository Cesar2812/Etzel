using LayerDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUseCase.Usuario
{
    public interface IListar
    {
        public Task<List<DMUsuario>> ListarUsuario();

    }
}
