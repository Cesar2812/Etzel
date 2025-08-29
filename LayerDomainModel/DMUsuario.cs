namespace LayerDomainModel;

public class DMUsuario : DMPersona
{
    //datos del Usuario
    public int IdUsuario { get; set; }
    public string? Correo { get; set; }
    public string? Clave_hash { get; set; }
    public bool Restablecer { get; set; }
    public int idtipoUsuario { get; set; }

    public DMRol? objRol { get; set; }//si se quiere obtener el rol en el perfil



    //campos para el guardado de la imagen o foto del Usuario
    public string? RutaFoto { get; set; }

    public string? NombreFoto { get; set; }
    public string? Base64 { get; set; }

    public string? Extension { get; set; }

}