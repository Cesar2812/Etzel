using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Extensions.Options;

namespace LayerAdapters;

public class GuardarFotoEnServidor:IGuardarFotoServidor
{
    private readonly Ruta _rutaServidorFoto;
    public GuardarFotoEnServidor(IOptions<Ruta> options)
    {
        _rutaServidorFoto = options.Value;
    }

    public async Task<string> SubirFoto(DMUsuario user)
    {
        string ruta = _rutaServidorFoto.RutaServidorFotos;
        string rutaArchivo = Path.Combine(ruta, user.archivo.FileName);//ruta donde se guardara el archivo con el nombre del archivo

        using (FileStream newFile= System.IO.File.Create(rutaArchivo))
        {
            await user.archivo.CopyToAsync(newFile);//copiando foto
            newFile.Flush();
        }

        // devolver ruta relativa para guardar en BD
        return rutaArchivo;
    }
}
