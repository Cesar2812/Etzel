using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DAMarketplace;

public class GuardarArchivoBD:IGuardarArchivoBD
{
    private readonly Conection _conection;
    private readonly ILogger<DMUsuario> _logger;

    private SqlConnection conexion;
    public GuardarArchivoBD(IOptions<Conection> options, ILogger<DMUsuario> logger)
    {
        _conection = options.Value;
    }

    public async Task<bool> GuardarArchivoMarketplace(DMRecursosMarketplace objRecurso)
    {
        bool resultado = false; // guarda el resultado de la operacion
        try
        {
            using (conexion = new SqlConnection(_conection.CadenaSQL))
            {
                string query = "update RECURSOS.RecursosMarketplace set RutaArchivoRecurso=@rutaArchivo, NombreArchivoRecurso=@nombreArchivo where IdRecurso=@IdRecurso";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@rutaArchivo", objRecurso.RutaArchivoRecurso);
                cmd.Parameters.AddWithValue("@nombreArchivo", objRecurso.NombreArchivoRecurso);
                cmd.Parameters.AddWithValue("@IdRecurso", objRecurso.IdRecurso);
                cmd.CommandType = CommandType.Text;

                await conexion.OpenAsync();

                if (await cmd.ExecuteNonQueryAsync() > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
        }
        catch (Exception ex)
        {

            _logger.LogCritical(ex, "Se ha producido un error inesperado al guardar el Archivo en Base de datos.");
            throw new ApplicationException("Ocurrió un error inesperado. Por favor, contacte al soporte.", ex);
        }
        finally
        {
            await conexion.CloseAsync();
        }
        return resultado;
    }
}
