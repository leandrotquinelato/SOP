using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;

namespace SOP.DAL.DAO
{
    public class UsuarioDAO
    {

        public static List<Usuario> ObterUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                String SQL = @"SELECT U.ID_USUA, U.NM_USUA, U.LOGIN_USUA, U.SENHA_USUA, U.EMAIL_USUA
	                                FROM T_CDTR_USUA U
                                  WHERE U.DT_INAT_USUA IS NULL";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usua = new Usuario();
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_USUA"))) usua.Id_Usua = reader.GetInt32(reader.GetOrdinal("ID_USUA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NM_USUA"))) usua.Nm_Usua = reader.GetString(reader.GetOrdinal("NM_USUA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("LOGIN_USUA"))) usua.Login_Usua = reader.GetString(reader.GetOrdinal("LOGIN_USUA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("SENHA_USUA"))) usua.Senha_Usua = reader.GetString(reader.GetOrdinal("SENHA_USUA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("EMAIL_USUA"))) usua.Email_Usua = reader.GetString(reader.GetOrdinal("EMAIL_USUA"));

                            listaUsuarios.Add(usua);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaUsuarios;
        }

//        public static void InserirAlmoxarifadoOrigem(AlmoxarifadoOrigem item)
//        {          
//            try
//            {
//                String SQL = @"INSERT INTO mrs_mgr_almx_orig (cd_almx_orig, dc_almx_orig, dt_inat) 
//                                values (:codigo, :descricao, null)";

//                using (OracleConnection conexao = Conexoes.ObterConexaoExclusivaEBS())
//                {
//                    OracleCommand comando = new OracleCommand(SQL, conexao);
//                    comando.Parameters.Add(new OracleParameter("codigo", item.CodigoAlmoxarifadoOrigem));
//                    comando.Parameters.Add(new OracleParameter("descricao", item.Descricao));

//                    comando.ExecuteNonQuery();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }   
         
//        }

//        public static void RemoveAlmoxarifadoOrigem(AlmoxarifadoOrigem item)
//        {
//            try
//            {
//                String SQL = @"UPDATE mrs_mgr_almx_orig
//                                    SET dt_inat = :data
//                                  WHERE id_almx_orig = :id";

//                using (OracleConnection conexao = Conexoes.ObterConexaoExclusivaEBS())
//                {
//                    OracleCommand comando = new OracleCommand(SQL, conexao);
//                    comando.Parameters.Add(new OracleParameter("id", item.IdAlmoxarifadoOrigem));
//                    comando.Parameters.Add(new OracleParameter("data", DateTime.Now));
//                    comando.ExecuteNonQuery();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }   

//        }

//        public static void AtualizaAlmoxarifadoOrigem(AlmoxarifadoOrigem item)
//        {
//            try
//            {
//                String SQL = @"UPDATE mrs_mgr_almx_orig
//                                SET cd_almx_orig = :codigo, dc_almx_orig = :descricao
//                              WHERE id_almx_orig = :id";

//                using (OracleConnection conexao = Conexoes.ObterConexaoExclusivaEBS())
//                {
//                    OracleCommand comando = new OracleCommand(SQL, conexao);
//                    comando.Parameters.Add(new OracleParameter("codigo", item.CodigoAlmoxarifadoOrigem));
//                    comando.Parameters.Add(new OracleParameter("descricao", item.Descricao));
//                    comando.Parameters.Add(new OracleParameter("id", item.IdAlmoxarifadoOrigem));
//                    comando.ExecuteNonQuery();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }   
//        }


    }
}
