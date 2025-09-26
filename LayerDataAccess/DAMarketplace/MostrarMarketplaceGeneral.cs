using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Globalization;
using System.Text;


namespace LayerDataAccess.DAMarketplace;

public class MostrarMarketplaceGeneral:IMostrarMarketplaceGeneral
{

    private readonly Conection _conection;
    private SqlConnection conexion;
    public MostrarMarketplaceGeneral(IOptions<Conection> options)
    {
        _conection = options.Value;
    }


    //sin recibir un parametro
    public async Task<List<DMUsuarioRecursosMarketplace>> ListarRecursoMarketplace()
    {
        List<DMUsuarioRecursosMarketplace> lista = new List<DMUsuarioRecursosMarketplace>();

        using (conexion = new SqlConnection(_conection.CadenaSQL))
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT DISTINCT rm.IdRecurso,rm.TituloRecurso,urm.FechaPublicacion[FechaPublicacion],Precio,rm.RutaArchivoRecurso,rm.NombreArchivoRecurso,");
            sb.AppendLine("CAST(rm.DescripcionRecurso AS NVARCHAR(MAX)) AS DescripcionRecurso, tr.NombreTipoRecurso, ts.NombreSector,");
            sb.AppendLine("er.DescripcionEstadoRecurso,COALESCE(CONCAT(per.NombrePersona, ' ', pn.ApellidoPersonaNatural), CONCAT(per.NombrePersona, ' ', pj.RazonSocial)) AS NombrePublicador");
            sb.AppendLine("FROM RECURSOS.UsuarioRecursosMarketplace urm INNER JOIN RECURSOS.RecursosMarketplace rm ON urm.Id_recurso = rm.IdRecurso");
            sb.AppendLine("INNER JOIN CATALOGOS.TipoRecurso tr  ON rm.Id_tipoRecurso = tr.IdTipoRecurso INNER JOIN CATALOGOS.TipoSectorEconomico ts  ON rm.Id_tipoSectorEconomico = ts.IdTipoSectorEconomico");
            sb.AppendLine("INNER JOIN CATALOGOS.EstadoRecurso er  ON rm.Id_estadoRecurso = er.IdEstadoRecurso INNER JOIN SEGURIDAD.Usuario u  ON urm.Id_usuario = u.IdUsuario");
            sb.AppendLine("INNER JOIN CATALOGOS.Persona per  ON u.IdPersona = per.IdPersona LEFT JOIN CATALOGOS.PersonaNatural pn  ON per.IdPersona = pn.Id_persona");
            sb.AppendLine("LEFT JOIN CATALOGOS.PersonaJuridica pj  ON per.IdPersona = pj.Id_persona;");

            await conexion.OpenAsync();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
            cmd.CommandType = CommandType.Text;

            using (var dr = await cmd.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    lista.Add(new DMUsuarioRecursosMarketplace()
                    {
                        objRecursoMarketplace= new DMRecursosMarketplace()
                        {
                            IdRecurso = Convert.ToInt32(dr["IdRecurso"]),
                            TituloRecurso = dr["TituloRecurso"].ToString()!,
                            DescripcionRecurso = dr["DescripcionRecurso"].ToString()!,
                            Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-NI")),
                            RutaArchivoRecurso = dr["RutaArchivoRecurso"].ToString()!,
                            NombreArchivoRecurso = dr["NombreArchivoRecurso"].ToString()!,
                            objTipoRecurso =new DMTipoRecurso()
                            {
                                NombreTipoRecurso = dr["NombreTipoRecurso"].ToString()!,
                            },
                            objTipoSectorEconomico= new DMTipoSectorEconomico()
                            {
                                NombreSector = dr["NombreSector"].ToString()!,
                            },
                            objEstadoRecurso = new DMEstadoRecurso()
                            {
                                DescripcionEstadoRecurso= dr["DescripcionEstadoRecurso"].ToString()!,
                            }
                        },
                        FechaPublicacion = Convert.ToDateTime(dr["FechaPublicacion"])
                    });
                }
            }
        }
        return lista;
    }
}
