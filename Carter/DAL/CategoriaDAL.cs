using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter.DAL
{
    class CategoriaDAL
    {
        public void CadastrarCategorias(string descricao, bool habilitado)
        {
            string strsql = @"INSERT INTO categoria (
	                                        descricao
	                                        ,habilitado
	                                        )
                                        VALUES (
	                                        @descricao
	                                        ,@habilitado 
	                                        )";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@descricao", descricao);
                command.Parameters.AddWithValue("@utilizaPoupanca", habilitado == true ? 1 : 0);

                command.ExecuteNonQuery();
            }
        }
    }
}
