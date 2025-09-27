using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DAMarketplace;

public class CrearRecursoMarketplace: ICrearRecursoMarketplace
{
    private readonly Conection _conection;
    SqlConnection conexion;
    private readonly ILogger<DMRecursosMarketplace> _logger;

    public CrearRecursoMarketplace(IOptions<Conection> options, ILogger<DMRecursosMarketplace> logger)
    {
        _conection = options.Value;
        _logger = logger;
    }

    public async Task<int> CrearRecursosMarketplace(DMRecursosMarketplace objetoRecursoMarketplace,int idUsuario)
    {

        string respuesta = "";
        int idAutogenerado = 0;

        using (conexion = new SqlConnection(_conection.CadenaSQL))
        {
            SqlCommand cmd = new SqlCommand("RECURSOS.sp_RegistrarRecursoMarketplace", conexion);
           
            cmd.Parameters.AddWithValue("@TituloRecurso", objetoRecursoMarketplace.TituloRecurso);
            cmd.Parameters.AddWithValue("@DescripcionRecurso", objetoRecursoMarketplace.DescripcionRecurso);
            cmd.Parameters.AddWithValue("@idSectorEconomico", objetoRecursoMarketplace.objTipoSectorEconomico.IdTipoSectorEconomico);
            cmd.Parameters.AddWithValue("@idTipoRecurso", objetoRecursoMarketplace.objTipoRecurso.IdTipoRecurso);
            cmd.Parameters.AddWithValue("@idEstadoRecurso", objetoRecursoMarketplace.objEstadoRecurso.IdEstadoRecurso);
            cmd.Parameters.AddWithValue("@Precio", objetoRecursoMarketplace.Precio);

            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

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

                _logger.LogError(ex, "Error insertar el recurso en la base de datos.");
                throw new ApplicationException("Ocurrió un error al registrar el recurso. Por favor, intente de nuevo.", ex);
            }
            catch (Exception ex)
            {

                _logger.LogCritical(ex, "Se ha producido un error inesperado al ingresar el recurso");
                throw new ApplicationException("Ocurrió un error inesperado.", ex);
            }
            finally
            {
                await conexion.CloseAsync();
            }
        }
        return idAutogenerado;
    }
}
