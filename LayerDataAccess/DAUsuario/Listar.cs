using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text;

namespace LayerDataAccess.DAUsuario;

public class Listar : IListar
{
    private readonly Conection _conection;
    private SqlConnection conexion;

    public Listar(IOptions<Conection> options)
    {
        _conection = options.Value;
    }

    //LISTAR USUARIO PARA BUCARLO EN LA SESION
    public async Task<List<DMUsuario>> ListarUsuario()
    {
        List<DMUsuario> lista = new List<DMUsuario>();

        using (conexion = new SqlConnection(_conection.CadenaSQL))
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select P.NombrePersona,R.DescripcionRol,U.Correo,U.Clave_hash,U.Restablecer,U.NombreFoto,U.RutaFoto");
            sb.AppendLine("from SEGURIDAD.Usuario U");
            sb.AppendLine("inner Join CATALOGOS.Persona P");
            sb.AppendLine("on U.idpersona=P.IdPersona");
            sb.AppendLine("inner join SEGURIDAD.RolUsuario R on U.idtipoUsuario=R.IdRolUsuario");

            await conexion.OpenAsync();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
            cmd.CommandType = CommandType.Text;

            using (var dr = await cmd.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    lista.Add(new DMUsuario()
                    {
                        NombrePersona = dr["NombrePersona"].ToString()!,
                        objRol = new DMRol()
                        {
                            DescripcionRol = dr["DescripcionRol"].ToString()!,
                        },
                        Correo = dr["Correo"].ToString()!,//campo de busqueda al inicar sesion
                        Clave_hash = dr["Clave_hash"].ToString(),//campo de busqueda al inicar sesion
                        Restablecer = Convert.ToBoolean(dr["Restablecer"]),
                        NombreFoto = dr["NombreFoto"].ToString()!,
                        RutaFoto = dr["RutaFoto"].ToString()!,
                    });
                }
            }
        }
        return lista;
    }
}
