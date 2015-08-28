using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.crosscutting.utilities
{
    public static class DataBaseProvider
    {

        public static DataTable GetApontamentoAnalitico(int empresaId, int usuarioId, int competenciaId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                con.Open();
                SqlCommand cmd =
                    new SqlCommand(
                        @"SELECT * FROM dbo.vApontamentoAprovado WHERE UsuarioId = ISNULL(NULLIF(@usuarioId, 0), UsuarioId) AND EmpresaId = @empresaId AND CompetenciaId = @competenciaId ORDER BY Nome, Data",
                        con);
                cmd.Parameters.Add("@usuarioId", SqlDbType.Int);
                cmd.Parameters.Add("@empresaId", SqlDbType.Int);
                cmd.Parameters.Add("@competenciaId", SqlDbType.Int);
                cmd.Parameters["@usuarioId"].Value = usuarioId;
                cmd.Parameters["@empresaId"].Value = empresaId;
                cmd.Parameters["@competenciaId"].Value = competenciaId;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetApontamentoSintetico(int empresaId, int usuarioId, int competenciaId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                con.Open();
                SqlCommand cmd =
                    new SqlCommand(
                        @"SELECT * FROM dbo.vApontamentoSumarizado WHERE UsuarioId = ISNULL(NULLIF(@usuarioId, 0), UsuarioId) AND EmpresaId = @empresaId AND CompetenciaId = @competenciaId ORDER BY Nome, CentroDeCusto, Hh DESC",
                        con);
                cmd.Parameters.Add("@usuarioId", SqlDbType.Int);
                cmd.Parameters.Add("@empresaId", SqlDbType.Int);
                cmd.Parameters.Add("@competenciaId", SqlDbType.Int);
                cmd.Parameters["@usuarioId"].Value = usuarioId;
                cmd.Parameters["@empresaId"].Value = empresaId;
                cmd.Parameters["@competenciaId"].Value = competenciaId;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                return dt;
            }
        }

    }
}
