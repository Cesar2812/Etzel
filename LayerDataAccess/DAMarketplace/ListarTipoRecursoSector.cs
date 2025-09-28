using LayerDomainModel;
using LayerUseCase.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDataAccess.DAMarketplace
{
    public class ListarTipoRecursoSector:IFilttarTipoRecursoSector
    {

        private readonly Conection _conection;
        private SqlConnection conexion;

        public ListarTipoRecursoSector(IOptions<Conection> options)
        {
            _conection = options.Value;
        }

        public async  Task<List<DMTipoRecurso>> ListarRecursoSector(int idSector)
        {
            List<DMTipoRecurso> lista = new List<DMTipoRecurso>();

            using (conexion = new SqlConnection(_conection.CadenaSQL))
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select distinct tr.IdTipoRecurso,tr.NombreTipoRecurso");
                sb.AppendLine("from RECURSOS.RecursosMarketplace rm");
                sb.AppendLine("inner join CATALOGOS.TipoSectorEconomico tpsc on rm.Id_tipoSectorEconomico=tpsc.IdTipoSectorEconomico");
                sb.AppendLine("inner join CATALOGOS.TipoRecurso tr on rm.Id_tipoRecurso=tr.IdTipoRecurso");
                sb.AppendLine("where tpsc.IdTipoSectorEconomico=iif(@idSector=1, tpsc.IdTipoSectorEconomico,@idSector)");

                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
                cmd.Parameters.AddWithValue("@idSector", idSector);

                cmd.CommandType = CommandType.Text;


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new DMTipoRecurso()
                        {
                            IdTipoRecurso = Convert.ToInt32(dr["IdTipoRecurso"]),
                            NombreTipoRecurso = dr["NombreTipoRecurso"].ToString()!
                        });
                    }
                }
            }
            return lista;
        }
    }
}
