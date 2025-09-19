using LayerUseCase.Interface;

namespace LayerAdapters;

public class RecursosConversionArchivo:IConversionRecurso
{
    public string convertirBase64(string ruta, out bool conversion)
    {
        string textoBase64 = string.Empty;
        //conversion que se obtiene como parametro de salida
        conversion = true;

        try
        {
            byte[] bytes = File.ReadAllBytes(ruta);// el archivo o imagen que se optiene en la ruta que lo convierta en un array de bytes
            textoBase64 = Convert.ToBase64String(bytes);
        }
        catch
        {
            conversion = false;

        }
        return textoBase64;
    }
}
