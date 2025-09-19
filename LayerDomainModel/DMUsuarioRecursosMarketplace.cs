

namespace LayerDomainModel;

public class DMUsuarioRecursosMarketplace
{
    public int IdRecursoUsuario {  get; set; }

    public DMUsuario? objUsuario { get; set; }

    public DMRecursosMarketplace? objRecursoMarketplace { get; set; }

    public DateTime FechaPublicacion { get; set; }
}
