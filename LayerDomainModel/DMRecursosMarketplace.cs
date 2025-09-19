using Microsoft.AspNetCore.Http;

namespace LayerDomainModel;


public class DMRecursosMarketplace
{ 
    public int IdRecurso {  get; set; }

    public string? TituloRecurso {  get; set; }

    public string? DescripcionRecurso { get; set; }


    public string? RutaArchivoRecurso { get; set; }

    public string? NombreArchivoRecurso { get; set; }

    public IFormFile? archivo {  get; set; }

    
    public DMTipoSectorEconomico? objTipoSectorEconomico { get; set; }


    public DMTipoRecurso? objTipoRecurso { get; set; }  

    public DMEstadoRecurso? objEstadoRecurso { get; set; }

    public decimal Precio { get; set; }

    public string? PrecioTexto { get; set; }



    //para mostrar los recursos o imagenes en la vista
    public string? Base64 { get; set; }

    public string? Extension { get; set; }

}
