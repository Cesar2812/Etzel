
namespace LayerDomainModel;

public class DMPersona : DMPersonaNatural, DMPersonaJuridica
{

    //Datos de persona
    public int IdPersona { get; set; }
    public string? NombrePersona { get; set; }

    public int Id_municipo { get; set; }

    public DMMunicipio? objMunicipio { get; set; }



    //Datos de persona Juridica
    public int IdPersonaJuridica { get; set; }

    public string? NumeroRuc { get; set; }

    public string? RazonSocial { get; set; }
    public int idSectorEconomico { get; set; }

    public DMTipoSectorEconomico? objSector { get; set; }


    //Datos de persona Natural
    public int IdPersonaNatural { get; set; }
    public string? CedulaPerosonaNatural { get; set; }

    public string? ApellidoPersonaNatural { get; set; }

    public int Id_genero { get; set; }

    public DMGenero? objGenero { get; set; }

}
