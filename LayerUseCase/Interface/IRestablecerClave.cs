namespace LayerUseCase.Interface
{
    public interface IRestablecerClave
    {
        public Task<bool> RestablecerClaveUser(int idUsuario, string Clave);
    }
}
