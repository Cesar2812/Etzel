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



    //datos de persona
    public int IdPersona { get; set; }
    public string? NombrePersona { get; set; }

    public int Id_municipo { get; set; }

    public DMMunicipio? objMunicipio { get; set; }




    //datos de persona Juridica
    public int IdPersonaJuridica { get; set; }

    public string? NumeroRuc { get; set; }

    public string? RazonSocial { get; set; }


    //datos de persona Natural
    public int IdPersonaNatural { get; set; }
    public string? CedulaPerosonaNatural { get; set; }

    public string? ApellidoPersonaNatural { get; set; }
    public int Id_genero { get; set; }
    public DMGenero? objGenero { get; set; }




    //campos para el guardado de la imagen o foto del Usuario
    public string? RutaFoto { get; set; }

    public string? NombreFoto { get; set; }
    public string? Base64 { get; set; }

    public string? Extension { get; set; }

}