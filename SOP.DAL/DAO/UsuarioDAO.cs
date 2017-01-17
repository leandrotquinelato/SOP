using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;
using System.Security.Claims;

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

        public static void InserirUsuario(Usuario item)
        {
            DateTime dataAtual = DateTime.Now;

            try
            {
                String SQL = @"INSERT INTO T_CDTR_USUA
                                 (NM_USUA, LOGIN_USUA, SENHA_USUA,	EMAIL_USUA,	DT_INCS_USUA,
                                  CD_USUA_INCS_USUA, DT_ALTR_USUA, CD_USUA_ALTR_USUA, DT_INAT_USUA)
                                VALUES
                                 (@nome, @login, @senha, @email, @dataInclusao,
                                  @usuarioInclusao, null, null, null)";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("nome", item.Nm_Usua));
                    comando.Parameters.Add(new SqlParameter("login", item.Login_Usua));
                    comando.Parameters.Add(new SqlParameter("senha", item.Senha_Usua));
                    comando.Parameters.Add(new SqlParameter("email", item.Email_Usua));
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

        public static void RemoveUsuario(Usuario item)
        {
            try
            {
                String SQL = @"UPDATE T_CDTR_USUA
                                    SET DT_INAT_USUA = @data, 
                                        CD_USUA_ALTR_USUA = @cdUsua, 
                                        DT_ALTR_USUA = @data
                                  WHERE ID_USUA = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("id", item.Id_Usua));
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

        public static void AtualizaUsuario(Usuario item)
        {
            try
            {
                String SQL = @"UPDATE T_CDTR_USUA
                                SET NM_USUA = @nome,
		                            LOGIN_USUA = @login,
		                            SENHA_USUA = @senha,
		                            EMAIL_USUA = @email,
                                    CD_USUA_ALTR_USUA = @cdUsua, 
                                    DT_ALTR_USUA = @data
                                WHERE ID_USUA = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("nome", item.Nm_Usua));
                    comando.Parameters.Add(new SqlParameter("login", item.Login_Usua));
                    comando.Parameters.Add(new SqlParameter("senha", item.Senha_Usua));
                    comando.Parameters.Add(new SqlParameter("email", item.Email_Usua)); 
                    comando.Parameters.Add(new SqlParameter("cdUsua", item.Cd_Usua_Altr));
                    comando.Parameters.Add(new SqlParameter("data", DateTime.Now));
                    comando.Parameters.Add(new SqlParameter("id", item.Id_Usua));
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
