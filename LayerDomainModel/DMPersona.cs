
namespace LayerDomainModel;

public class DMPersona : DMPersonaNatural, DMPersonaJuridica
{
    public int IdPersona { get; set; }
    public string? NombrePersona { get; set; }

    public int Id_municipo { get; set; }

    public DMMunicipio? objMunicipio { get; set; }



    //persona Juridica
    public int IdPersonaJuridica { get; set; }

    public string? NumeroRuc { get; set; }

    public string? RazonSocial { get; set; }


    //persona Natural
    public int IdPersonaNatural { get; set; }
    public string? CedulaPerosonaNatural { get; set; }

    public string? ApellidoPersonaNatural { get; set; }

    public int Id_genero { get; set; }

    public DMGenero? objGenero { get; set; }

}
