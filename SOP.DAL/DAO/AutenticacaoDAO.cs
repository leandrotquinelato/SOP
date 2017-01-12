using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;

namespace SOP.DAL.DAO
{
    public class AutenticacaoDAO
    {
        public static string ValidaUsuarioLogin(string usuario, string senha)
        {
            string retorno = null;
            try
            {
                String SQL = @" SELECT NM_USUA
	                                FROM T_CDTR_USUA
                                 WHERE LOGIN_USUA = '" + usuario + @"'
	                                   AND SENHA_USUA = '" + senha + "'";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retorno = reader.GetString(reader.GetOrdinal("NM_USUA"));
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
