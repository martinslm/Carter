using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter
{
    public class Conexao
    {
        static private SqlConnection _connection;

        private static SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new SqlConnection(@"Data Source=DESKTOP-KU4CSAM\SQLEXPRESS;Initial Catalog=carterSystem;Integrated Security=True");
                return _connection;
            }
        }

        public static SqlConnection Conectar()
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
