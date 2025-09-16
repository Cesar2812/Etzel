using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Extensions.Options;

namespace LayerAdapters
{
    public class GuardarArchivoDeRecurso:IGuardarRecursoServidor
    {
        private readonly RutaArchivos _rutaArchivos;

        public GuardarArchivoDeRecurso(IOptions<RutaArchivos> options)
        {
            _rutaArchivos = options.Value;
        }

        public async Task<string> SubirRecurso(DMRecursosMarketplace objRecurso)
        {
            string ruta = _rutaArchivos.RutaServidorArchivos;
            string rutaArchivo = Path.Combine(ruta, objRecurso.archivo.FileName);//ruta donde se guardara el archivo con el nombre del archivo

            using (FileStream newFile = System.IO.File.Create(rutaArchivo))
            {
                await objRecurso.archivo.CopyToAsync(newFile);
                newFile.Flush();
            }

            // devolver ruta relativa para guardar en BD
            return rutaArchivo;
        }
    }
}
