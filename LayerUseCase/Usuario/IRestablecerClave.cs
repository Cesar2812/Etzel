namespace LayerUseCase.Usuario
{
    public interface IRestablecerClave
    {
        public Task<bool> RestablecerClaveUser(int idUsuario, string Clave);


    }
}
