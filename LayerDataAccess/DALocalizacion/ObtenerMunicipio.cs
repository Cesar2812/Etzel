using LayerDomainModel;
using LayerUseCase.Localizacion;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

using System.Data;

namespace LayerDataAccess.DALocalizacion
{
    public class ObtenerMunicipio : IObtenerMunicipio
    {
        private readonly Conection _conection;

        SqlConnection conexion;

        public ObtenerMunicipio(IOptions<Conection> options)
        {
            _conection = options.Value;//valor de la cadena de conexion
        }

        //Tarea para Listar Municipio por departamento
        public async Task<List<DMMunicipio>> ObtenerMunicipi(string idDepartamento)
        {
            List<DMMunicipio> lista = new List<DMMunicipio>();

            try
            {
                using (conexion = new SqlConnection(_conection.CadenaSQL))
                {
                    string consulta = "select * from LOCALIZACION.Municipio where Id_departamento=@iddepartamento";

                    await conexion.OpenAsync();
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@iddepartamento", idDepartamento);
                    comando.CommandType = CommandType.Text;

                    using (var dr = await comando.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            lista.Add
                            (
                                new DMMunicipio()
                                {
                                    IdMunicipio = Convert.ToInt32(dr["IdMunicipio"].ToString()),
                                    CodigoPostal = dr["CodigoPostal"].ToString(),
                                    NombreMunicipio = dr["NombreMunicipio"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<DMMunicipio>();
            }
            finally
            {
                await conexion.CloseAsync();
            }

            return lista;
        }


    }
}
