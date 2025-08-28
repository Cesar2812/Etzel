using LayerDomainModel;
using LayerUsesCases.Usuario;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DAUsuario
{
    public class RestablecerClave:IRestablecerClave
    {
        private readonly Conection _conection;
        private readonly ILogger<DMUsuario> _logger;


        private SqlConnection conexion;

        public RestablecerClave(IOptions<Conection> options, ILogger<DMUsuario> logger)
        {
            _conection = options.Value;
        }

        //metodo para recuperar clave
        public async Task<bool> RestablecerClaveUser(int idUsuario, string Clave)
        {
            bool resultado = false; // guarda el resultado de la operacion
            try
            {
                using (conexion = new SqlConnection(_conection.CadenaSQL))
                {
                    string query = "update SEGURIDAD.Usuario set Clave_hash=@Clave,Restablecer=1 where IdUsuario=@idUsuario";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@Clave", Clave);
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

                _logger.LogCritical(ex, "Se ha producido un error inesperado al cambiar la cuenta.");
                throw new ApplicationException("Ocurrió un error inesperado. Por favor, contacte al soporte.", ex);
            }
            finally
            {
                await conexion.CloseAsync();
            }
            return resultado;
        }
    }
}
