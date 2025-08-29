using LayerDomainModel;
using LayerUseCase.Interface;

namespace LayerUseCase.Usuario
{
    public class UCListarUsuario
    {
        private  readonly IListar _listar;

        public UCListarUsuario(IListar listar)
        {
            _listar = listar;   
        }
        public async Task<List<DMUsuario>> ListarUsuario()
        {
            var listaUsers= await _listar.ListarUsuario();
            return listaUsers;
        }
    }
}
