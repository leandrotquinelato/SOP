using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;
using System.Security.Claims;

namespace SOP.DAL.DAO
{
    public class RelatorioDAO
    {
        public static List<Orcamento> CarregarGridOrcamento(DateTime dataIni, DateTime dataFinal, string numOrcamento, string cliente)
        {
            List<Orcamento> listaOrcamento = new List<Orcamento>();

            try
            {
                String SQL = @"SELECT O.ID_ORCMT, O.ID_CLIE, O.PRECO_FRETE, O.VL_TT, O.DT_INCS_ORCMT
                                    FROM T_ORCMT O
                                WHERE O.DT_INCS_ORCMT >= '" + dataIni + @"' 
	                                  AND O.DT_INCS_ORCMT <= '" + dataFinal.AddDays(1) + "'";

                if (!string.IsNullOrEmpty(numOrcamento))
                {
                    SQL += " AND O.ID_ORCMT = " + numOrcamento;
                }

                if (!string.IsNullOrEmpty(cliente))
                {
                    SQL += " AND O.ID_CLIE = " + cliente;
                }

                SQL += " ORDER BY O.ID_ORCMT DESC";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Orcamento orc = new Orcamento();
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_ORCMT"))) orc.Id_Orcamento_Capa = reader.GetInt32(reader.GetOrdinal("ID_ORCMT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_CLIE"))) orc.Id_Cliente = reader.GetInt32(reader.GetOrdinal("ID_CLIE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("PRECO_FRETE"))) orc.Frete = reader.GetDouble(reader.GetOrdinal("PRECO_FRETE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("VL_TT"))) orc.ValorTotal = reader.GetDouble(reader.GetOrdinal("VL_TT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("DT_INCS_ORCMT"))) orc.Dt_Incs_Rgst = reader.GetDateTime(reader.GetOrdinal("DT_INCS_ORCMT"));

                            listaOrcamento.Add(orc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaOrcamento;
        }
    }
}
