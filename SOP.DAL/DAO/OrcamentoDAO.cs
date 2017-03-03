using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;
using System.Security.Claims;

namespace SOP.DAL.DAO
{
    public class OrcamentoDAO
    {

        public static List<ItemOrcamento> ObterItemOrcamento(int idOrcamento)
        {
            List<ItemOrcamento> listaItemOrcamento = new List<ItemOrcamento>();

            try
            {
                String SQL = @"SELECT ID_ITEM_ORCMT, ID_ORCMT, ID_PEDRA, ID_ACBMT, ID_TP_PEDRA, QT, CP							
		                            , LA, ESQD, DRTO, ACIMA, ABXO, VL_TT, DT_INCS_ITEM_ORCMT			
		                            , CD_USUA_INCS_ITEM_ORCMT, DT_ALTR_ITEM_ORCMT, CD_USUA_ALTR_ITEM_ORCMT		
		                            , DT_INAT_ITEM_ORCMT
                                FROM T_ITEM_ORCMT
                                WHERE DT_INAT_ITEM_ORCMT IS NULL";

                if (idOrcamento > 0)
                {
                    SQL += " AND ID_ORCMT = " + idOrcamento;
                }

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemOrcamento usua = new ItemOrcamento();
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_ITEM_ORCMT"))) usua.Id_ItemOrcamento = reader.GetInt32(reader.GetOrdinal("ID_ITEM_ORCMT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_ORCMT"))) usua.Id_Orcamento = reader.GetInt32(reader.GetOrdinal("ID_ORCMT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_PEDRA"))) usua.Id_Pedra = reader.GetInt32(reader.GetOrdinal("ID_PEDRA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_ACBMT"))) usua.Id_Tipo_Acabamento = reader.GetInt32(reader.GetOrdinal("ID_ACBMT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_TP_PEDRA"))) usua.Id_Tipo_Pedra = reader.GetInt32(reader.GetOrdinal("ID_TP_PEDRA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("QT"))) usua.Quantidade = reader.GetInt32(reader.GetOrdinal("QT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("CP"))) usua.Comprimento = reader.GetDouble(reader.GetOrdinal("CP"));
                            if (!reader.IsDBNull(reader.GetOrdinal("LA"))) usua.Largura = reader.GetDouble(reader.GetOrdinal("LA"));

                            if (reader.GetBoolean(reader.GetOrdinal("ESQD")))
                            {
                                usua.EsquerdaString = "SIM";
                                usua.Esquerda = true;
                            }
                            else
                            {
                                usua.EsquerdaString = "NÃO";
                                usua.Esquerda = false;
                            }

                            if (reader.GetBoolean(reader.GetOrdinal("DRTO")))
                            {
                                usua.DireitaString = "SIM";
                                usua.Direita = true;
                            }
                            else
                            {
                                usua.DireitaString = "NÃO";
                                usua.Direita = false;
                            }

                            if (reader.GetBoolean(reader.GetOrdinal("ACIMA")))
                            {
                                usua.CimaString = "SIM";
                                usua.Cima = true;
                            }
                            else
                            {
                                usua.CimaString = "NÃO";
                                usua.Cima = false;
                            }

                            if (reader.GetBoolean(reader.GetOrdinal("ABXO")))
                            {
                                usua.BaixoString = "SIM";
                                usua.Baixo = true;
                            }
                            else
                            {
                                usua.BaixoString = "NÃO";
                                usua.Baixo = false;
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("VL_TT"))) usua.ValorTotal = reader.GetDouble(reader.GetOrdinal("VL_TT"));

                            listaItemOrcamento.Add(usua);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaItemOrcamento;
        }

        public static void InserirItemOrcamento(ItemOrcamento item)
        {
            DateTime dataAtual = DateTime.Now;

            try
            {
                String SQL = @"INSERT INTO T_ITEM_ORCMT
                                 (ID_ORCMT, ID_PEDRA, ID_ACBMT, ID_TP_PEDRA, QT, CP							
		                            , LA, ESQD, DRTO, ACIMA, ABXO, VL_TT, DT_INCS_ITEM_ORCMT			
		                            , CD_USUA_INCS_ITEM_ORCMT, DT_ALTR_ITEM_ORCMT, CD_USUA_ALTR_ITEM_ORCMT		
		                            , DT_INAT_ITEM_ORCMT)
                                VALUES
                                 (@idOrcamento, @idPedra, @idAcabamento, @idTipoPedra, @qnt, @compr,
                                  @larg, @esq, @dir, @cima, @baixo, @valorTotal, @dataInclusao, 
                                  @usuarioInclusao, null, null, 
                                  null)";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("idOrcamento", item.Id_Orcamento));
                    comando.Parameters.Add(new SqlParameter("idPedra", item.Id_Pedra));
                    comando.Parameters.Add(new SqlParameter("idAcabamento", item.Id_Tipo_Acabamento));
                    comando.Parameters.Add(new SqlParameter("idTipoPedra", item.Id_Tipo_Pedra));
                    comando.Parameters.Add(new SqlParameter("qnt", item.Quantidade));
                    comando.Parameters.Add(new SqlParameter("compr", item.Comprimento));
                    comando.Parameters.Add(new SqlParameter("larg", item.Largura));
                    comando.Parameters.Add(new SqlParameter("esq", item.Esquerda));
                    comando.Parameters.Add(new SqlParameter("dir", item.Direita));
                    comando.Parameters.Add(new SqlParameter("cima", item.Cima));
                    comando.Parameters.Add(new SqlParameter("baixo", item.Baixo));
                    comando.Parameters.Add(new SqlParameter("valorTotal", item.ValorTotal));
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

        public static void RemoveItemOrcamento(ItemOrcamento item)
        {
            try
            {
                String SQL = @"UPDATE T_ITEM_ORCMT
                                    SET DT_INAT_ITEM_ORCMT = @data, 
                                        CD_USUA_ALTR_ITEM_ORCMT = @cdUsua, 
                                        DT_ALTR_ITEM_ORCMT = @data
                                  WHERE ID_ITEM_ORCMT = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("id", item.Id_ItemOrcamento));
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

        public static void AtualizaItemOrcamento(ItemOrcamento item)
        {
            try
            {
                String SQL = @"UPDATE T_ITEM_ORCMT
                                SET ID_ORCMT = @idOrcamento
                                    , ID_PEDRA = @idPedra
                                    , ID_ACBMT = @idAcabamento
                                    , ID_TP_PEDRA = @idTipoPedra 
                                    , QT = @qnt	
                                    , CP = @compr	
                                    , LA = @larg	
                                    , ESQD = @esq 
                                    , DRTO = @dir
                                    , ACIMA = @cima 
                                    , ABXO = @baixo
                                    , VL_TT = @valorTotal
                                    ,CD_USUA_ALTR_ITEM_ORCMT = @cdUsua 
                                    ,DT_ALTR_ITEM_ORCMT = @data
                                WHERE ID_ITEM_ORCMT = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("idOrcamento", item.Id_Orcamento));
                    comando.Parameters.Add(new SqlParameter("idPedra", item.Id_Pedra));
                    comando.Parameters.Add(new SqlParameter("idAcabamento", item.Id_Tipo_Acabamento));
                    comando.Parameters.Add(new SqlParameter("idTipoPedra", item.Id_Tipo_Pedra));
                    comando.Parameters.Add(new SqlParameter("qnt", item.Quantidade));
                    comando.Parameters.Add(new SqlParameter("compr", item.Comprimento));
                    comando.Parameters.Add(new SqlParameter("larg", item.Largura));
                    comando.Parameters.Add(new SqlParameter("esq", item.Esquerda));
                    comando.Parameters.Add(new SqlParameter("dir", item.Direita));
                    comando.Parameters.Add(new SqlParameter("cima", item.Cima));
                    comando.Parameters.Add(new SqlParameter("baixo", item.Baixo));
                    comando.Parameters.Add(new SqlParameter("valorTotal", item.ValorTotal));
                    comando.Parameters.Add(new SqlParameter("cdUsua", item.Cd_Usua_Altr));
                    comando.Parameters.Add(new SqlParameter("data", DateTime.Now));
                    comando.Parameters.Add(new SqlParameter("id", item.Id_ItemOrcamento));
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InserirOrcamento(Orcamento item)
        {
            DateTime dataAtual = DateTime.Now;

            try
            {
                String SQL = @"INSERT INTO T_ORCMT
                                 (ID_CLIE, PRECO_FRETE, DT_INCS_ORCMT, CD_USUA_INCS_ORCMT, 
                                  VL_TT, DT_ALTR_ORCMT, CD_USUA_ALTR_ORCMT, DT_INAT_ORCMT	)
                                VALUES
                                 (@idCliente, @frete, @dataInclusao, @usuarioInclusao,
                                  @valorTotal, null, null, null);
                                SELECT MAX(ID_ORCMT) FROM T_ORCMT";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    if (item.Id_Cliente == 0 || item.Id_Cliente == null)
                        comando.Parameters.Add(new SqlParameter("idCliente", DBNull.Value));
                    else
                        comando.Parameters.Add(new SqlParameter("idCliente", item.Id_Cliente));
                    comando.Parameters.Add(new SqlParameter("frete", item.Frete));
                    if (item.ValorTotal != null)
                        comando.Parameters.Add(new SqlParameter("valorTotal", item.ValorTotal));
                    else
                        comando.Parameters.Add(new SqlParameter("valorTotal", 0.0));

                    comando.Parameters.Add(new SqlParameter("dataInclusao", dataAtual));
                    comando.Parameters.Add(new SqlParameter("usuarioInclusao", item.Cd_Usua_Rgst));

                    var idOrcamento = Convert.ToInt32(comando.ExecuteScalar());

                    return idOrcamento;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AtualizarOrcamento(Orcamento item)
        {
            try
            {
                String SQL = @"UPDATE T_ORCMT
                                SET ID_CLIE = @idCliente, 
                                    PRECO_FRETE = @frete, 
                                    VL_TT = @valorTotal, 
                                    DT_ALTR_ORCMT = @data,
                                    CD_USUA_ALTR_ORCMT = @cdUsua
                                WHERE ID_ORCMT = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    if (item.Id_Cliente == 0 || item.Id_Cliente == null)
                        comando.Parameters.Add(new SqlParameter("idCliente", DBNull.Value));
                    else
                        comando.Parameters.Add(new SqlParameter("idCliente", item.Id_Cliente));

                    comando.Parameters.Add(new SqlParameter("frete", item.Frete));
                    comando.Parameters.Add(new SqlParameter("valorTotal", item.ValorTotal));
                    comando.Parameters.Add(new SqlParameter("cdUsua", item.Cd_Usua_Altr));
                    comando.Parameters.Add(new SqlParameter("data", DateTime.Now));
                    comando.Parameters.Add(new SqlParameter("id", item.Id_Orcamento_Capa));
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int RecuperarUltimoId()
        {
            int retorno = 0;

            try
            {
                String SQL = @"Select TOP 1 ID_ORCMT from T_ORCMT order by ID_ORCMT desc";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retorno = reader.GetInt32(reader.GetOrdinal("ID_ORCMT"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public static double RecuperarValorProduto(ItemOrcamento item)
        {
            double retorno = 0;

            try
            {
                String SQL = @"SELECT P.PRECO_PEDRA
	                            FROM T_PEDRA P
                             WHERE P.ID_PEDRA = " + item.Id_Pedra;

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retorno = reader.GetDouble(reader.GetOrdinal("PRECO_PEDRA"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }
        public static double RecuperarValorTipo(ItemOrcamento item)
        {
            double retorno = 0;

            try
            {
                String SQL = @"SELECT P.PRECO_TP_PEDRA
	                                FROM T_TP_PEDRA P
                                 WHERE P.ID_TP_PEDRA = " + item.Id_Tipo_Pedra;

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retorno = reader.GetDouble(reader.GetOrdinal("PRECO_TP_PEDRA"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }
        public static double RecuperarValorAcabamento(ItemOrcamento item)
        {
            double retorno = 0;

            try
            {
                String SQL = @"SELECT A.PRECO_ACBMT
	                                FROM T_ACBMT A
                                 WHERE A.ID_ACBMT = " + item.Id_Tipo_Acabamento;

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retorno = reader.GetDouble(reader.GetOrdinal("PRECO_ACBMT"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public static Orcamento CarregarOrcamento(string idOrcamento)
        {
            Orcamento orcamento = new Orcamento();

            try
            {
                String SQL = @"SELECT O.ID_ORCMT, O.ID_CLIE, O.PRECO_FRETE, O.VL_TT, O.DT_INCS_ORCMT
                                    FROM T_ORCMT O
                                WHERE O.ID_ORCMT = " + idOrcamento;

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_ORCMT"))) orcamento.Id_Orcamento_Capa = reader.GetInt32(reader.GetOrdinal("ID_ORCMT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_CLIE"))) orcamento.Id_Cliente = reader.GetInt32(reader.GetOrdinal("ID_CLIE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("PRECO_FRETE"))) orcamento.Frete = reader.GetDouble(reader.GetOrdinal("PRECO_FRETE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("VL_TT"))) orcamento.ValorTotal = reader.GetDouble(reader.GetOrdinal("VL_TT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("DT_INCS_ORCMT"))) orcamento.Dt_Incs_Rgst = reader.GetDateTime(reader.GetOrdinal("DT_INCS_ORCMT"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return orcamento;
        }

        public static double RecuperarValorTotalItensOrcamento(int idOrcamento)
        {
            double retorno = 0;

            try
            {
                String SQL = @"SELECT SUM(VL_TT) AS TOTAL				                            
                                    FROM T_ITEM_ORCMT
                                  WHERE ID_ORCMT = " + idOrcamento;

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retorno = reader.GetDouble(reader.GetOrdinal("TOTAL"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }
    }
}
