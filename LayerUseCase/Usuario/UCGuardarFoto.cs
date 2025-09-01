using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.AspNetCore.Http;

namespace LayerUseCase.Usuario;

public class UCGuardarFoto
{
    private readonly IGuardarFoto _guardarFoto;

    public UCGuardarFoto(IGuardarFoto guardarFoto)
    {
        _guardarFoto = guardarFoto;
    }


    //en base de datos
    public async Task<bool> GuardarFoto(DMUsuario objetoUsuario)
    {
        bool resultado = await _guardarFoto.GuardarFotoUsuario(objetoUsuario);

        return resultado;
    }
}
