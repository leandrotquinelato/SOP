using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;
using System.Security.Claims;

namespace SOP.DAL.DAO
{
    public class ProdutoDAO
    {

        public static List<Produto> ObterProduto()
        {
            List<Produto> listaProduto = new List<Produto>();

            try
            {
                String SQL = @"SELECT A.ID_PEDRA, A.NM_PEDRA, A.PRECO_PEDRA 
	                                FROM T_PEDRA A 
                                  WHERE A.DT_INAT_PEDRA IS NULL";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Produto prod = new Produto();
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_PEDRA"))) prod.Id_Produto = reader.GetInt32(reader.GetOrdinal("ID_PEDRA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NM_PEDRA"))) prod.Nm_Produto = reader.GetString(reader.GetOrdinal("NM_PEDRA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("PRECO_PEDRA"))) prod.Nu_Preco = reader.GetDouble(reader.GetOrdinal("PRECO_PEDRA"));

                            listaProduto.Add(prod);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaProduto;
        }

        public static void InserirProduto(Produto item)
        {
            DateTime dataAtual = DateTime.Now;

            try
            {
                String SQL = @"INSERT INTO T_PEDRA
                                 (NM_PEDRA, PRECO_PEDRA, DT_INCS_PEDRA, CD_USUA_INCS_PEDRA,
                                   DT_ALTR_PEDRA, CD_USUA_ALTR_PEDRA, DT_INAT_PEDRA)
                                VALUES
                                 (@nome, @preco, @dataInclusao, @usuarioInclusao,
                                  null, null, null)";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("nome", item.Nm_Produto));
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

        public static void RemoveProduto(Produto item)
        {
            try
            {
                String SQL = @"UPDATE T_PEDRA
                                    SET DT_INAT_PEDRA = @data, 
                                        CD_USUA_ALTR_PEDRA = @cdUsua, 
                                        DT_ALTR_PEDRA = @data
                                  WHERE ID_PEDRA = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("id", item.Id_Produto));
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

        public static void AtualizaProduto(Produto item)
        {
            try
            {
                String SQL = @"UPDATE T_PEDRA
                                SET NM_PEDRA = @nome,
		                            PRECO_PEDRA = @login,
                                    CD_USUA_ALTR_PEDRA = @cdUsua, 
                                    DT_ALTR_PEDRA = @data
                                WHERE ID_PEDRA = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("nome", item.Nm_Produto));
                    comando.Parameters.Add(new SqlParameter("login", item.Nu_Preco));
                    comando.Parameters.Add(new SqlParameter("cdUsua", item.Cd_Usua_Altr));
                    comando.Parameters.Add(new SqlParameter("data", DateTime.Now));
                    comando.Parameters.Add(new SqlParameter("id", item.Id_Produto));
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
