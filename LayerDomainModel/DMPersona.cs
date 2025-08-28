
namespace LayerDomainModel;

public interface DMPersona : DMPersonaNatural, DMPersonaJuridica
{
    public int IdPersona { get; set; }
    public string? NombrePersona { get; set; }

    public int Id_municipo { get; set; }

    public DMMunicipio? objMunicipio { get; set; }

}
