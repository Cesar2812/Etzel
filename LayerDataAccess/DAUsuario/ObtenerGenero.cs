using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DAUsuario;

public class ObtenerGenero:IListarGenero
{
    private readonly Conection _conection;

    SqlConnection conexion;

    public ObtenerGenero(IOptions<Conection> options)
    {
        _conection = options.Value;
    }


    public async Task<List<DMGenero>> ListarGenero()
    {

        List<DMGenero> lista = new List<DMGenero>();

        try
        {
            using (conexion = new SqlConnection(_conection.CadenaSQL))
            {
                string consulta = "select * from CATALOGOS.Genero";

                await conexion.OpenAsync();
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.Text;


                using (var dr = await comando.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())//leyendo en otro hilo
                    {
                        lista.Add
                        (
                            new DMGenero()
                            {
                                IdGenero = Convert.ToInt32(dr["IdGenero"].ToString()),
                                DescripcionGenero = dr["DescripcionGenero"].ToString()
                               
                            }
                        );

                    }
                }
            }
        }
        catch
        {
            lista = new List<DMGenero>();//lista vacia o null
        }
        finally
        {
            await conexion.CloseAsync();//liberando la conexion
        }

        return lista;

    }
}
