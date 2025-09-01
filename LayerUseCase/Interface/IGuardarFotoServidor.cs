using LayerDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUseCase.Interface
{
    public interface IGuardarFotoServidor
    {
        public Task<string> SubirFoto(DMUsuario user,string nombreGenerado);
    }
}
