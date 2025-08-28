using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUseCase.Usuario
{
    public interface IRestablecerClave
    {
        public Task<bool> RestablecerClaveUser(int idUsuario, string Clave);


    }
}
