using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;
using System.Security.Claims;

namespace SOP.DAL.DAO
{
    public class AcabamentoDAO
    {

        public static List<Acabamento> ObterAcabamento()
        {
            List<Acabamento> listaAcabamento = new List<Acabamento>();

            try
            {
                String SQL = @"SELECT A.ID_ACBMT, A.NM_ACBMT, A.PRECO_ACBMT 
	                                FROM T_ACBMT A 
                                  WHERE A.DT_INAT_ACBMT IS NULL";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Acabamento acab = new Acabamento();
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_ACBMT"))) acab.Id_Acabamento = reader.GetInt32(reader.GetOrdinal("ID_ACBMT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NM_ACBMT"))) acab.Nm_Acabamento = reader.GetString(reader.GetOrdinal("NM_ACBMT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("PRECO_ACBMT"))) acab.Nu_Preco = reader.GetDouble(reader.GetOrdinal("PRECO_ACBMT"));
                           
                            listaAcabamento.Add(acab);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaAcabamento;
        }

        public static void InserirAcabamento(Acabamento item)
        {
            DateTime dataAtual = DateTime.Now;

            try
            {
                String SQL = @"INSERT INTO T_ACBMT
                                 (NM_ACBMT, PRECO_ACBMT, DT_INCS_ACBMT, CD_USUA_INCS_ACBMT,
                                   DT_ALTR_ACBMT, CD_USUA_ALTR_ACBMT, DT_INAT_ACBMT)
                                VALUES
                                 (@nome, @preco, @dataInclusao, @usuarioInclusao,
                                  null, null, null)";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("nome", item.Nm_Acabamento));
                    comando.Parameters.Add(new SqlParameter("preco", item.Nu_Preco));                    
                    comando.Parameters.Add(new SqlParameter("dataInclusao", dataAtual));
                    comando.Parameters.Add(new SqlParameter("usuarioInclusao", item.Cd_Usua_Rgst));

                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveAcabamento(Acabamento item)
        {
            try
            {
                String SQL = @"UPDATE T_ACBMT
                                    SET DT_INAT_ACBMT = @data, 
                                        CD_USUA_ALTR_ACBMT = @cdUsua, 
                                        DT_ALTR_ACBMT = @data
                                  WHERE ID_ACBMT = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("id", item.Id_Acabamento));
                    comando.Parameters.Add(new SqlParameter("data", DateTime.Now));
                    comando.Parameters.Add(new SqlParameter("cdUsua", item.Cd_Usua_Altr));
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void AtualizaAcabamento(Acabamento item)
        {
            try
            {
                String SQL = @"UPDATE T_ACBMT
                                SET NM_ACBMT = @nome,
		                            PRECO_ACBMT = @login,
                                    CD_USUA_ALTR_ACBMT = @cdUsua, 
                                    DT_ALTR_ACBMT = @data
                                WHERE ID_ACBMT = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("nome", item.Nm_Acabamento));
                    comando.Parameters.Add(new SqlParameter("login", item.Nu_Preco));
                    comando.Parameters.Add(new SqlParameter("cdUsua", item.Cd_Usua_Altr));
                    comando.Parameters.Add(new SqlParameter("data", DateTime.Now));
                    comando.Parameters.Add(new SqlParameter("id", item.Id_Acabamento));
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
