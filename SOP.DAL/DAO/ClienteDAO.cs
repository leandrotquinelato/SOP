using SOP.Entidades;
using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using System.Data.SqlClient;
using System.Security.Claims;

namespace SOP.DAL.DAO
{
    public class ClienteDAO
    {

        public static List<Cliente> ObterCliente()
        {
            List<Cliente> listaCliente = new List<Cliente>();

            try
            {
                String SQL = @"SELECT C.ID_CLIE, C.NM_CLIE, C.EMAIL_CLIE, C.TEL_CEL_CLIE, C.TEL_FIXO_CLIE,
	                                  C.NM_RUA, C.NU_RSDN, C.NU_CMPT, C.NM_BAIRRO, C.NU_CEP
	                                FROM T_CDTR_CLIE C 
                                  WHERE C.DT_INAT_CLIE IS NULL";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente clie = new Cliente();
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_CLIE"))) clie.Id_Cliente = reader.GetInt32(reader.GetOrdinal("ID_CLIE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NM_CLIE"))) clie.Nm_Cliente = reader.GetString(reader.GetOrdinal("NM_CLIE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("EMAIL_CLIE"))) clie.Email_Cliente = reader.GetString(reader.GetOrdinal("EMAIL_CLIE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("TEL_CEL_CLIE"))) clie.Tel_Cel_Cliente = reader.GetString(reader.GetOrdinal("TEL_CEL_CLIE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("TEL_FIXO_CLIE"))) clie.Tel_Fixo_Cliente = reader.GetString(reader.GetOrdinal("TEL_FIXO_CLIE"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NM_RUA"))) clie.Nm_Rua = reader.GetString(reader.GetOrdinal("NM_RUA"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NU_RSDN"))) clie.Nu_Residencia = reader.GetString(reader.GetOrdinal("NU_RSDN"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NU_CMPT"))) clie.Nu_Complemento = reader.GetString(reader.GetOrdinal("NU_CMPT"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NM_BAIRRO"))) clie.Nm_Bairro = reader.GetString(reader.GetOrdinal("NM_BAIRRO"));
                            if (!reader.IsDBNull(reader.GetOrdinal("NU_CEP"))) clie.Nu_CEP = reader.GetString(reader.GetOrdinal("NU_CEP"));

                            listaCliente.Add(clie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaCliente;
        }

        public static void InserirCliente(Cliente item)
        {
            DateTime dataAtual = DateTime.Now;

            try
            {
                String SQL = @"INSERT INTO T_CDTR_CLIE
                                 (NM_CLIE, TEL_FIXO_CLIE, TEL_CEL_CLIE, NM_RUA, NU_CEP			 
                                  , NU_RSDN, NU_CMPT, NM_BAIRRO, EMAIL_CLIE, DT_INCS_CLIE     
                                  , CD_USUA_INCS_CLIE, DT_ALTR_CLIE, CD_USUA_ALTR_CLIE, DT_INAT_CLIE    )
                                VALUES
                                 ('" + item.Nm_Cliente + @"',  '" + item.Tel_Fixo_Cliente + @"', '" + item.Tel_Cel_Cliente + @"', '" + item.Nm_Rua + @"', '" + item.Nu_CEP + @"',
                                  '" + item.Nu_Residencia + @"', '" + item.Nu_Complemento + @"', '" + item.Nm_Bairro + @"', '" + item.Email_Cliente + @"', '" + DateTime.Now + @"',
                                  '" + item.Cd_Usua_Altr + @"', null, null, null)";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);

                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveCliente(Cliente item)
        {
            try
            {
                String SQL = @"UPDATE T_CDTR_CLIE
                                    SET DT_INAT_CLIE = @data, 
                                        CD_USUA_ALTR_CLIE = @cdUsua, 
                                        DT_ALTR_CLIE = @data
                                  WHERE ID_CLIE = @id";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
                    comando.Parameters.Add(new SqlParameter("id", item.Id_Cliente));
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

        public static void AtualizaCliente(Cliente item)
        {
            try
            {
                String SQL = @"UPDATE T_CDTR_CLIE
                                SET NM_CLIE = '" + item.Nm_Cliente + @"', 
                                    TEL_FIXO_CLIE = '" + item.Tel_Fixo_Cliente + @"', 
                                    TEL_CEL_CLIE = '" + item.Tel_Cel_Cliente + @"',
                                    NM_RUA = '" + item.Nm_Rua + @"',
                                    NU_CEP = '" + item.Nu_CEP + @"',
                                    NU_RSDN = '" + item.Nu_Residencia + @"',
                                    NU_CMPT ='" + item.Nu_Complemento + @"',
                                    NM_BAIRRO = '" + item.Nm_Bairro + @"',
                                    EMAIL_CLIE = '" + item.Email_Cliente + @"', 
                                    DT_ALTR_CLIE = '" + DateTime.Now + @"',
                                    CD_USUA_ALTR_CLIE = '" + item.Cd_Usua_Altr + @"'
                                WHERE ID_CLIE = '" + item.Id_Cliente + "'";

                using (SqlConnection conexao = Conexoes.ObterConexaoSql())
                {
                    SqlCommand comando = new SqlCommand(SQL, conexao);
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
