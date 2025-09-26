using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DAUsuario;

public class CrearCuenta : ICrearCuenta
{
    private readonly Conection _conection;
    private readonly ILogger<DMUsuario> _logger;

    private SqlConnection conexion;

    public CrearCuenta(IOptions<Conection> options, ILogger<DMUsuario> logger)
    {
        _conection = options.Value;
        _logger = logger;
    }


    //Creacion de Cuenta de Usuario
    public async Task<int> CrearCuentaUsuario(DMUsuario objetoUsuario)
    {
        string respuesta = "";
        int idAutogenerado = 0;

        using (conexion = new SqlConnection(_conection.CadenaSQL))
        {
            SqlCommand cmd = new SqlCommand("SEGURIDAD.sp_RegistrarUsuario", conexion);
            //usuario
            cmd.Parameters.AddWithValue("@Correo", objetoUsuario.Correo);
            cmd.Parameters.AddWithValue("@ClaveHash", objetoUsuario.Clave_hash);
            cmd.Parameters.AddWithValue("@IdRol", objetoUsuario.idtipoUsuario);
            cmd.Parameters.AddWithValue("@IdMunicipio", objetoUsuario.idMunicipio);

            cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                await conexion.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                idAutogenerado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                respuesta = Convert.ToString(cmd.Parameters["@Mensaje"].Value)!;
            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "Error al crear cuenta de usuario en la base de datos.");
                throw new ApplicationException("Ocurrió un error al registrar el usuario. Por favor, intente de nuevo.", ex);
            }
            catch (Exception ex)
            {

                _logger.LogCritical(ex, "Se ha producido un error inesperado al crear la cuenta.");
                throw new ApplicationException("Ocurrió un error inesperado", ex);
            }
            finally
            {
                await conexion.CloseAsync();
            }
        }
        return idAutogenerado;
    }
}
