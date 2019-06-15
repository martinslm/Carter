using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carter.Models;
using System;
using System.Data.SqlClient;

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

        public Categoria ObterDadosCategoriaPorId(int idCategoria)
        {
            string strsql = @"SELECT descricao
	                                ,habilitado
                                FROM categoria
                                WHERE id_categoria = @idCategoria";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@idCategoria", idCategoria);
                using (var reader = busca.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Categoria()
                        {
                            Id = idCategoria,
                            Descricao = reader["descricao"].ToString(),
                            Habilitado = Convert.ToInt32(reader["habilitado"]) == 1 ? true : false
                        };
                    }
                }
            }

            return new Categoria();

        }
    }
}
