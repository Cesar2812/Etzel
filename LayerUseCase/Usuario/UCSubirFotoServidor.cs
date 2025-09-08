using LayerDomainModel;
using LayerUseCase.Interface;


namespace LayerUseCase.Usuario;

public class UCSubirFotoServidor
{
    private readonly IGuardarFotoServidor _subirFotoServidor;

    public UCSubirFotoServidor(IGuardarFotoServidor subirFoto)
    {
        _subirFotoServidor = subirFoto; 
    }

    public async Task<string> AgregarFotoEnServidor(DMUsuario usuario)
    {
        return await _subirFotoServidor.SubirFoto(usuario);
    }

}
