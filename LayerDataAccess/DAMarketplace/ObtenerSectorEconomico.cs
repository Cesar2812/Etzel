using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

using System.Data;


namespace LayerDataAccess.DAMarketplace;

public class ObtenerSectorEconomico:IListarSectorEconomico
{
    private readonly Conection _conection;

    SqlConnection conexion;

    public ObtenerSectorEconomico(IOptions<Conection> options)
    {
        _conection = options.Value;//valor de la cadena de conexion
    }

    public async Task<List<DMTipoSectorEconomico>> ListarSectorEconomico()
    {

        List<DMTipoSectorEconomico> lista = new List<DMTipoSectorEconomico>();

        try
        {
            using (conexion = new SqlConnection(_conection.CadenaSQL))
            {
                string consulta = "select * from CATALOGOS.TipoSectorEconomico";

                await conexion.OpenAsync();
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.Text;


                using (var dr = await comando.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())//leyendo en otro hilo
                    {
                        lista.Add
                        (
                            new DMTipoSectorEconomico()
                            {
                                IdTipoSectorEconomico = Convert.ToInt32(dr["IdTipoSectorEconomico"].ToString()),
                                NombreSector = dr["NombreSector"].ToString(),
                                DescripcionSector = dr["DescripcionSector"].ToString()
                            }
                        );

                    }
                }
            }
        }
        catch
        {
            lista = new List<DMTipoSectorEconomico>();//lista vacia o null
        }
        finally
        {
            await conexion.CloseAsync();//liberando la conexion
        }

        return lista;

    }
}
