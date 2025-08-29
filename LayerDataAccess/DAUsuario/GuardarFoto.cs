using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DAUsuario;


public class GuardarFoto : IGuardarFoto
{
    private readonly Conection _conection;
    private readonly ILogger<DMUsuario> _logger;


    private SqlConnection conexion;

    public GuardarFoto(IOptions<Conection> options, ILogger<DMUsuario> logger)
    {
        _conection = options.Value;
    }

    //metodo para guardar o foto del usuario durante la creacion de la cuenta
    public async Task<bool> GuardarFotoUsuario(DMUsuario objetoUsuario)
    {
        bool resultado = false; // guarda el resultado de la operacion
        try
        {
            using (conexion = new SqlConnection(_conection.CadenaSQL))
            {
                string query = "update SEGURIDAD.Usuario set RutaFoto=@rutaFoto, NombreFoto=@nombreFoto where IdUsuario=@IdUsuario";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@rutaFoto", objetoUsuario.RutaFoto);
                cmd.Parameters.AddWithValue("@nombreFoto", objetoUsuario.NombreFoto);
                cmd.Parameters.AddWithValue("@IdUsuario", objetoUsuario.IdUsuario);
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

            _logger.LogCritical(ex, "Se ha producido un error inesperado al guardar la cuenta.");
            throw new ApplicationException("Ocurrió un error inesperado. Por favor, contacte al soporte.", ex);
        }
        finally
        {
            await conexion.CloseAsync();
        }
        return resultado;
    }
}
