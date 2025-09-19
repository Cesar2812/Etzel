using LayerUseCase.Interface;


namespace LayerUseCase.Marketplace;

public class UCConvertirRecurso
{
    private readonly IConversionRecurso _conversionRecurso;

    public UCConvertirRecurso(IConversionRecurso conversionRecurso)
    {
        _conversionRecurso = conversionRecurso;
    }
    public string convertirBase64(string ruta, out bool conversion)
    {
        string textoBase64=_conversionRecurso.convertirBase64(ruta,out conversion);
        return textoBase64;
    }
}
