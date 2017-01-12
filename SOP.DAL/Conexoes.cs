using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Configuration;
using Devart.Data.Oracle;
using System.Data.SqlClient;

namespace SOP.DAL
{
    [global::System.Serializable]
    public class ConexoesException : Exception
    {
        public ConexoesException() { }
        public ConexoesException(string message) : base(message) { }
        public ConexoesException(string message, Exception inner) : base(message, inner) { }
        protected ConexoesException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    public static class Conexoes
    {

        public static SqlConnection ObterConexaoSql()
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"packet size=4096;user id=admin;pwd=123456;data source=.\SQLEXPRESS;persist security info=False;initial catalog=SOP_DES";

            conn.Open();

            return conn;
        }
    }
}
