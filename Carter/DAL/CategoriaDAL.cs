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

        public void CadastrarCategorias()
        {
            var strsql = @"INSERT INTO categoria (
	                                        descricao
	                                        ,habilitado
	                                        )
                                        VALUES (
	                                        @descricao
	                                        ,@habilitado 
	                                        )";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);

                command.ExecuteNonQuery();
            }
            /*
            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@descricao", descricao);
                command.Parameters.AddWithValue("@habilitado", habilitado == true ? 1 : 0);

                command.ExecuteNonQuery();
            }

            */
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

        public List<Categoria> ObterCategoriasPorUsuarioLogado()
        {
            List<Categoria> categorias = new List<Categoria>();

            string strsql = @"SELECT id_categoria
	                                ,descricao
	                                ,habilitado
                                FROM categoria
                                WHERE habilitado = 1
	                                AND (
		                                id_usuario IS NULL
		                                OR id_usuario = @idUsuario
		                                )";

            if (Sessao.Usuario.UtilizaPoupanca && Sessao.Usuario.CategoriaPoupanca != null)
                strsql += string.Format("AND id_categoria <> {0}", Sessao.Usuario.CategoriaPoupanca.Id);

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);

                using (var reader = busca.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var categoria = new Categoria()
                        {
                            Id = Convert.ToInt32(reader["id_categoria"]),
                            Habilitado = Convert.ToInt32(reader["habilitado"]) == 1 ? true : false,
                            Descricao = reader["descricao"].ToString()
                        };
                        categorias.Add(categoria);
                    }
                }

                return categorias;
            }
        }
        public void VincularIdUsuarioAcategoria(int idCategoria, int idUsuario)
        {
            string strsql = @" UPDATE categoria 
                               SET id_usuario = @idUsuario
                               WHERE id_categoria = @idCategoria";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@idCategoria", idCategoria);

                command.ExecuteNonQuery();
            }
        }
    }
}
