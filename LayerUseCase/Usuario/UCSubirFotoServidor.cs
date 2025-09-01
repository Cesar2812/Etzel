using LayerDomainModel;
using LayerUseCase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerUseCase.Usuario
{
    public class UCSubirFotoServidor
    {
        private readonly IGuardarFotoServidor _subirFotoServidor;

        public UCSubirFotoServidor(IGuardarFotoServidor subirFoto)
        {
            _subirFotoServidor = subirFoto; 
        }

        public async Task<string> AgregarFotoEnServidor(DMUsuario usuario,string nombreGenerado)
        {
            return await _subirFotoServidor.SubirFoto(usuario,nombreGenerado);
        }


    }
}
