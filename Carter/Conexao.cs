using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter
{
    class Conexao
    {
        SqlConnection Connection = new SqlConnection();
        public Conexao()
        {
            Connection.ConnectionString = @"Data Source=DESKTOP-KU4CSAM\SQLEXPRESS;Initial Catalog=carterSystem;Integrated Security=True";
        }
        public SqlConnection Conectar()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
            return Connection; 
        }
        public void Desconectar()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }
    }
}
