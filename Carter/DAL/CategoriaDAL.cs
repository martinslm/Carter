using Carter.Models;
using System;
using System.Data.SqlClient;

namespace Carter.DAL
{
    class CategoriaDAL
    {
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
