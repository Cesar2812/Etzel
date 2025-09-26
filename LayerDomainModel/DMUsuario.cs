using Microsoft.AspNetCore.Http;

namespace LayerDomainModel;

public class DMUsuario
{
    //datos del Usuario
    public int IdUsuario { get; set; }
    public string? Correo { get; set; }
    public string? Clave_hash { get; set; }
    public bool Restablecer { get; set; }
    public int idtipoUsuario { get; set; }

    public DMRol? objRol { get; set; }//si se quiere obtener el rol en el perfil


    public int idMunicipio { get; set; }

    public DMMunicipio? objMunicipio { get; set; }





    //datos para la vista 
    public string? confirmarClave { get; set; }

}