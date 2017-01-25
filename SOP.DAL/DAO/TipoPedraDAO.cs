using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;
using System.Security.Claims;

namespace SOP.DAL.DAO
{
    public class TipoPedraDAO
    {

        public static List<TipoPedra> ObterTipoPedra()
        {
            List<TipoPedra> listaTipoPedra = new List<TipoPedra>();

            try
            {
                String SQL = @"SELECT A.ID_TP_PEDRA, A.NM_TP_PEDRA, A.PRECO_TP_PEDRA 
	                                FROM T_TP_PEDRA A 
                                  WHERE A.DT_INAT_TP_PEDRA IS NULL";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoPedra tp = new TipoPedra();
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_TP_PEDRA"))) tp.Id_TpPedra = reader.GetInt32(reader.GetOrdinal("ID_TP_PEDRA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NM_TP_PEDRA"))) tp.Nm_TpPedra = reader.GetString(reader.GetOrdinal("NM_TP_PEDRA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("PRECO_TP_PEDRA"))) tp.Nu_Preco = reader.GetDouble(reader.GetOrdinal("PRECO_TP_PEDRA"));

                            listaTipoPedra.Add(tp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaTipoPedra;
        }

        public static void InserirTipoPedra(TipoPedra item)
        {
            DateTime dataAtual = DateTime.Now;

            try
            {
                String SQL = @"INSERT INTO T_TP_PEDRA
                                 (NM_TP_PEDRA, PRECO_TP_PEDRA, DT_INCS_TP_PEDRA, CD_USUA_INCS_TP_PEDRA,
                                   DT_ALTR_TP_PEDRA, CD_USUA_ALTR_TP_PEDRA, DT_INAT_TP_PEDRA)
                                VALUES
                                 (@nome, @preco, @dataInclusao, @usuarioInclusao,
                                  null, null, null)";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("nome", item.Nm_TpPedra));
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

        public static void RemoveTipoPedra(TipoPedra item)
        {
            try
            {
                String SQL = @"UPDATE T_TP_PEDRA
                                    SET DT_INAT_TP_PEDRA = @data, 
                                        CD_USUA_ALTR_TP_PEDRA = @cdUsua, 
                                        DT_ALTR_TP_PEDRA = @data
                                  WHERE ID_TP_PEDRA = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("id", item.Id_TpPedra));
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

        public static void AtualizaTipoPedra(TipoPedra item)
        {
            try
            {
                String SQL = @"UPDATE T_TP_PEDRA
                                SET NM_TP_PEDRA = @nome,
		                            PRECO_TP_PEDRA = @login,
                                    CD_USUA_ALTR_TP_PEDRA = @cdUsua, 
                                    DT_ALTR_TP_PEDRA = @data
                                WHERE ID_TP_PEDRA = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("nome", item.Nm_TpPedra));
                    comando.Parameters.Add(new SqlParameter("login", item.Nu_Preco));
                    comando.Parameters.Add(new SqlParameter("cdUsua", item.Cd_Usua_Altr));
                    comando.Parameters.Add(new SqlParameter("data", DateTime.Now));
                    comando.Parameters.Add(new SqlParameter("id", item.Id_TpPedra));
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
