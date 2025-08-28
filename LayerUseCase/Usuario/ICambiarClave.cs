namespace LayerUseCase.Usuario
{
    public interface ICambiarClave
    {
        public Task<bool> CambiarClaveUser(int idUsuario, string nuevaClave);

    }
}
