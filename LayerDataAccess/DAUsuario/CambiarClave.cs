using LayerDomainModel;
using LayerUseCase.Usuario;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DAUsuario;

public class CambiarClave:ICambiarClave
{
    private readonly Conection _conection;
    private readonly ILogger<DMUsuario> _logger;


    private SqlConnection conexion;


    public CambiarClave(IOptions<Conection> options, ILogger<DMUsuario> logger)
    {
        _conection = options.Value;
    }


    //metodo para cambiar clave
    public async Task<bool> CambiarClaveUser(int idUsuario, string nuevaClave)
    {
        bool resultado = false; // guarda el resultado de la operacion
        try
        {
            using (conexion = new SqlConnection(_conection.CadenaSQL))
            {
                string query = "update SEGURIDAD.Usuario set Clave_hash=@nuevaClave,Restablecer=0 where IdUsuario=@idUsuario";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@nuevaClave", nuevaClave);
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

            _logger.LogCritical(ex, "Se ha producido un error inesperado al recuperar la clave");
            throw new ApplicationException("Ocurrió un error inesperado. Por favor, contacte al soporte.", ex);
        }
        finally
        {
            await conexion.CloseAsync();
        }
        return resultado;
    }
}
