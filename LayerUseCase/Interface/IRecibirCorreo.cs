namespace LayerUseCase.Interface
{
    public interface IRecibirCorreo
    {
        public Task<bool> RecibirCorreo(string correo, string asunto, string mensaje);
    }
}
