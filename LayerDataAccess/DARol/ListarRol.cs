using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace LayerDataAccess.DARol;


public class ListarRol : IListarRol
{
    private readonly Conection _conection;
    SqlConnection conexion;

    public ListarRol(IOptions<Conection> options)
    {
        _conection = options.Value;
    }

    //Tarea para Listar Roles de forma asincrona a la BD
    public async Task<List<DMRol>> ListarTipoRol()
    {
        List<DMRol> lista = new List<DMRol>();

        try
        {
            using (conexion = new SqlConnection(_conection.CadenaSQL))
            {
                string consulta = "select * from SEGURIDAD.RolUsuario";

                await conexion.OpenAsync();
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.Text;


                using (var dr = await comando.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add
                        (
                            new DMRol()
                            {
                                IdRolUsuario = Convert.ToInt32(dr["IdRolUsuario"].ToString()),
                                DescripcionRol = dr["DescripcionRol"].ToString()
                            }
                        );
                    }
                }
            }
        }
        catch
        {
            lista = new List<DMRol>();
        }
        finally
        {
            await conexion.CloseAsync();
        }

        return lista;
    }
}
