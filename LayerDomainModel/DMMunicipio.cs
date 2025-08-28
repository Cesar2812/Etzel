
namespace LayerDomainModel;
public class DMMunicipio
{
    public int IdMunicipio { get; set; }

    public string? CodigoPostal { get; set; }

    public string? NombreMunicipio { get; set; }

    public DMDepartamento? objDepartamento { get; set; }
}
