using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUsesCases.Usuario
{
    public interface IRestablecerClave
    {
        Task<bool> RestablecerClaveUser(int idUsuario, string Clave);
    }
}
