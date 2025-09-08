using LayerDomainModel;


namespace LayerUseCase.Interface;

public interface IGuardarFotoServidor
{
    public Task<string> SubirFoto(DMUsuario user);
}
