using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUseCase.Usuario
{
    public interface ICambiarClave
    {
        public Task<bool> CambiarClaveUser(int idUsuario, string nuevaClave);

    }
}
