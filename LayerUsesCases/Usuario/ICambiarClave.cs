using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUsesCases.Usuario
{
    public interface ICambiarClave
    {

        Task<bool> CambiarClaveUser(int idUsuario, string nuevaClave);

    }
}
