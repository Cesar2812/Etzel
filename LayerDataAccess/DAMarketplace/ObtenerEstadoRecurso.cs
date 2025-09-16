using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;


namespace LayerDataAccess.DAMarketplace
{
    public class ObtenerEstadoRecurso:IListarEstadoRecurso
    {
        private readonly Conection _conection;

        SqlConnection conexion;

        public ObtenerEstadoRecurso(IOptions<Conection> options)
        {
            _conection = options.Value;
        }


        public async Task<List<DMEstadoRecurso>> ListarEstadoRecurso()
        {

            List<DMEstadoRecurso> lista = new List<DMEstadoRecurso>();

            try
            {
                using (conexion = new SqlConnection(_conection.CadenaSQL))
                {
                    string consulta = "select * from CATALOGOS.EstadoRecurso";

                    await conexion.OpenAsync();
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.CommandType = CommandType.Text;


                    using (var dr = await comando.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())//leyendo en otro hilo
                        {
                            lista.Add
                            (
                                new DMEstadoRecurso()
                                {
                                    IdEstadoRecurso = Convert.ToInt32(dr["IdEstadoRecurso"].ToString()),
                                    DescripcionEstadoRecurso = dr["DescripcionEstadoRecurso"].ToString()
                                  
                                }
                            );

                        }
                    }
                }
            }
            catch
            {
                lista = new List<DMEstadoRecurso>();//lista vacia o null
            }
            finally
            {
                await conexion.CloseAsync();//liberando la conexion
            }

            return lista;

        }
    }
}
