using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Extensions.Options;

namespace LayerAdapters;

public class GuardarArchivoDeRecurso:IGuardarRecursoServidor
{
    private readonly RutaArchivos _rutaArchivos;

    public GuardarArchivoDeRecurso(IOptions<RutaArchivos> options)
    {
        _rutaArchivos = options.Value;
    }

    public async Task<string> SubirRecurso(DMRecursosMarketplace objRecurso)
    {
        string carpetaUploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        string rutaArchivoFisica = Path.Combine(carpetaUploads, objRecurso.archivo.FileName);

        using (FileStream newFile = System.IO.File.Create(rutaArchivoFisica))
        {
            await objRecurso.archivo.CopyToAsync(newFile);
            newFile.Flush();
        }

        
        return $"uploads/{objRecurso.archivo.FileName}";
    }
}
