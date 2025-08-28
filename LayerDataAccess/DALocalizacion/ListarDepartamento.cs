using LayerDomainModel;
using LayerUsesCases.Localizacion;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DALocalizacion
{
    public class ListarDepartamento:IListarDepartamento
    {
        private readonly Conection _conection;

        SqlConnection conexion;

        public ListarDepartamento(IOptions<Conection> options)
        {
            _conection = options.Value;//valor de la cadena de conexion
        }

        //Tarea para Listar Departamento de forma asincrona a la BD
        public async Task<List<DMDepartamento>> ListaDepartament()
        {
            List<DMDepartamento> lista = new List<DMDepartamento>();

            try
            {
                using (conexion = new SqlConnection(_conection.CadenaSQL))
                {
                    string consulta = "select * from LOCALIZACION.Departamento";

                    await conexion.OpenAsync();
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.CommandType = CommandType.Text;


                    using (var dr = await comando.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())//leyendo en otro hilo
                        {
                            lista.Add
                            (
                                new DMDepartamento()
                                {
                                    IdDepartamento = Convert.ToInt32(dr["IdDepartamento"].ToString()),
                                    NombreDepartamento = dr["NombreDepartamento"].ToString()
                                }
                            );

                        }
                    }
                }
            }
            catch
            {
                lista = new List<DMDepartamento>();//lista vacia o null
            }
            finally
            {
                await conexion.CloseAsync();//liberando la conexion
            }

            return lista;
        }
    }
}
