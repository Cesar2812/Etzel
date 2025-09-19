using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Globalization;
using System.Text;

namespace LayerDataAccess.DAMarketplace;


public class ListarRecursosMarketplaceUsuario:IMostrarRecursosUsuario
{
    private readonly Conection _conection;
    private SqlConnection conexion;


    public ListarRecursosMarketplaceUsuario(IOptions<Conection> options)
    {
        _conection = options.Value;
    }

    public async Task<List<DMUsuarioRecursosMarketplace>> ListarRecursoMarketplaceUsuario(int idUsuario)
    {
        List<DMUsuarioRecursosMarketplace> lista = new List<DMUsuarioRecursosMarketplace>();

        using (conexion = new SqlConnection(_conection.CadenaSQL))
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select IdRecurso,TituloRecurso[Titulo],DescripcionRecurso[Descripcion],Precio,RutaArchivoRecurso,");
            sb.AppendLine("NombreArchivoRecurso,NombreTipoRecurso[TipoRecurso], NombreSector[SectorEconomico],DescripcionEstadoRecurso[Estado], FechaPublicacion");
            sb.AppendLine("from RECURSOS.UsuarioRecursosMarketplace Urm");
            sb.AppendLine("inner join RECURSOS.RecursosMarketplace Rm on Urm.Id_recurso=rm.IdRecurso");
            sb.AppendLine("inner join CATALOGOS.EstadoRecurso ER on Rm.Id_estadoRecurso=ER.IdEstadoRecurso");
            sb.AppendLine("inner join CATALOGOS.TipoRecurso Tr on Rm.Id_tipoRecurso=tr.IdTipoRecurso");
            sb.AppendLine("inner join CATALOGOS.TipoSectorEconomico tse on Rm.Id_tipoSectorEconomico=tse.IdTipoSectorEconomico");
            sb.AppendLine("where Urm.Id_usuario=@Id_usuario");
            sb.AppendLine("order by FechaPublicacion desc");

            await conexion.OpenAsync();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
            cmd.Parameters.AddWithValue("@Id_usuario", idUsuario);
            cmd.CommandType = CommandType.Text;

            using (var dr = await cmd.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    lista.Add(new DMUsuarioRecursosMarketplace()
                    {
                       objRecursoMarketplace=new DMRecursosMarketplace()
                       {
                           IdRecurso=Convert.ToInt32( dr["IdRecurso"]),
                           TituloRecurso= dr["Titulo"].ToString()!,
                           DescripcionRecurso= dr["Descripcion"].ToString()!,
                           Precio = Convert.ToDecimal(dr["Precio"],new CultureInfo("es-NI")),
                           RutaArchivoRecurso = dr["RutaArchivoRecurso"].ToString()!,
                           NombreArchivoRecurso = dr["NombreArchivoRecurso"].ToString()!,
                           objTipoRecurso = new DMTipoRecurso()
                           {
                               NombreTipoRecurso=dr["TipoRecurso"].ToString()!,
                           },
                           objTipoSectorEconomico= new DMTipoSectorEconomico()
                           {
                               NombreSector = dr["SectorEconomico"].ToString()!
                           },
                           objEstadoRecurso=new DMEstadoRecurso() 
                           {
                               DescripcionEstadoRecurso = dr["Estado"].ToString()!
                           },  
                       },
                        FechaPublicacion = Convert.ToDateTime(dr["FechaPublicacion"])
                       
                    });
                }
            }
        }
        return lista;
    }
}
