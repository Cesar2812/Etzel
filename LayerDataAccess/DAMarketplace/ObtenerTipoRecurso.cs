using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;


namespace LayerDataAccess.DAMarketplace;

public class ObtenerTipoRecurso:IListarTipoRecusro
{
    private readonly Conection _conection;

    SqlConnection conexion;

    public ObtenerTipoRecurso(IOptions<Conection> options)
    {
        _conection = options.Value;
    }


    public async Task<List<DMTipoRecurso>> ListarTipoRecuro()
    {
        List<DMTipoRecurso> lista = new List<DMTipoRecurso>();

        try
        {
            using (conexion = new SqlConnection(_conection.CadenaSQL))
            {
                string consulta = "select * from CATALOGOS.TipoRecurso";

                await conexion.OpenAsync();
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.Text;


                using (var dr = await comando.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())//leyendo en otro hilo
                    {
                        lista.Add
                        (
                            new DMTipoRecurso()
                            {
                                IdTipoRecurso = Convert.ToInt32(dr["IdTipoRecurso"].ToString()),
                                NombreTipoRecurso = dr["NombreTipoRecurso"].ToString()
                            }
                        );

                    }
                }
            }
        }
        catch
        {
            lista = new List<DMTipoRecurso>();//lista vacia o null
        }
        finally
        {
            await conexion.CloseAsync();//liberando la conexion
        }

        return lista;
    }
}