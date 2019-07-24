using Carter.Enums;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Carter.DAL
{
    class UsuarioDAL
    {
        private PoupancaDAL _poupancaDAL = new PoupancaDAL();
        private CategoriaDAL _categoriaDAL = new CategoriaDAL();
        private SalarioDAL _salarioDAL = new SalarioDAL();
        public StatusLogin StatusLogin(string login, string senha, ref int idUsuario)
        {
            string strsql = @" SELECT  u.id_usuario, 
                    u.email,
                    u.passwd_usuario
                    FROM usuario u
                    WHERE u.email = @login
            ";
            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@login", login);

                using (var reader = busca.ExecuteReader())
                {
                    if (!reader.Read()) { return Enums.StatusLogin.EmailInvalido; }
                }
            }

            strsql += @"AND u.passwd_usuario = @senha";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@login", login);
                busca.Parameters.AddWithValue("@senha", senha);

                using (var reader = busca.ExecuteReader())
                {
                    if (!reader.Read()) { return Enums.StatusLogin.SenhaInvalida; }
                    idUsuario = Convert.ToInt32(reader["id_usuario"]);
                }
            }
            return Enums.StatusLogin.Sucesso;
        }

        public List<Categoria> BuscarCategorias()
        {
            var categorias = new List<Categoria>();
            string strsql = @"SELECT 
                              c.id_categoria,
                              c.descricao,
                              c.habilitado
                              FROM categoria c
                              WHERE c.habilitado = 1";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                using (var reader = busca.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var categoria = new Categoria
                        {
                            Id = Convert.ToInt32(reader["id_categoria"]),
                            Descricao = reader["descricao"].ToString(),
                            Habilitado = Convert.ToInt32(reader["habilitado"]) == 1 ? true : false
                        };

                        categorias.Add(categoria);

                    }
                    return categorias;
                }
            }
        }

        internal bool ValidarExistenciaDeContaPorEmail(string email)
        {
            string strsql = @"SELECT * FROM usuario WHERE email = @email ";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@email", email);
                using (var reader = busca.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        public void CadastrarUsuario(string email, string senha, int idSalario, bool utilizaPoupanca)
        {
            string strsql = @"INSERT INTO usuario (
	                                        email
	                                        ,passwd_usuario
	                                        ,salario_atual
	                                        ,utiliza_poupanca
	                                        )
                                        VALUES (
	                                        @email
	                                        ,@senha
	                                        ,@idSalario
	                                        ,@utilizaPoupanca 
	                                        )";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@senha", senha);
                command.Parameters.AddWithValue("@idSalario", idSalario);
                command.Parameters.AddWithValue("@utilizaPoupanca", utilizaPoupanca == true ? 1 : 0);

                command.ExecuteNonQuery();
            }
        }
        internal void AtualizarContaUsuario(string email, bool utilizaPoupanca, int idCategoriaPoupanca = 0)
        {
            string strsql = @" UPDATE usuario
                               SET email = @email,
	                               utiliza_poupanca = @utiliza_poupanca
                                   WHERE id_usuario = @idUsuario";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@utilizaPoupanca", utilizaPoupanca == true ? 1 : 0);
                command.ExecuteNonQuery();
            }
        }
        public int ObterIdUsuarioPorEmail(string email)
        {
            string strsql = @"SELECT id_usuario
                              FROM usuario
                              WHERE email = @email";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@email", email);
                using (var reader = busca.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["id_usuario"]);
                    }
                }
            }
            return 0;
        }

        public void AtualizarDadosPoupancaPorUsuario(int idUsuario, int idPoupanca, Categoria categoriaPoupanca)
        {
            string strsql = @"UPDATE usuario
                            SET objetivo_valor_poupanca = @idPoupanca
                                , categoria_poupanca = @idCategoria
                            WHERE id_usuario = @idUsuario";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@idPoupanca", idPoupanca);
                command.Parameters.AddWithValue("@idCategoria", categoriaPoupanca.Id);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                command.ExecuteNonQuery();
            }
        }

        public Usuario ObterDadosUsuarioPorId(int idUsuario)
        {
            Usuario usuario = new Usuario();
            int idSalario = 0, idCategoria = 0, idPoupanca = 0;
            string strsql = @"SELECT email
	                                ,passwd_usuario
	                                ,ISNULL(salario_atual,0) AS salario_atual
	                                ,utiliza_poupanca
	                                ,ISNULL(objetivo_valor_poupanca,0) AS objetivo_valor_poupanca
	                                ,ISNULL(categoria_poupanca,0) AS categoria_poupanca
                                FROM usuario
                                WHERE id_usuario = @idUsuario";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@idUsuario", idUsuario);
                using (var reader = busca.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario.Id = idUsuario;
                        usuario.Email = reader["email"].ToString();
                        usuario.Password = reader["passwd_usuario"].ToString();
                        idSalario = Convert.ToInt32(reader["salario_atual"]);
                        idCategoria = Convert.ToInt32(reader["categoria_poupanca"]);
                        idPoupanca = Convert.ToInt32(reader["objetivo_valor_poupanca"]);
                        usuario.UtilizaPoupanca = Convert.ToInt32(reader["utiliza_poupanca"]) == 1 ? true : false;
                    }
                }

                if (idSalario > 0)
                    usuario.SalarioAtual = _salarioDAL.ObterDadosSalarioPorId(idSalario);
                if (idCategoria > 0)
                    usuario.CategoriaPoupanca = _categoriaDAL.ObterDadosCategoriaPorId(idCategoria);
                if (idPoupanca > 0)
                    usuario.ObjetivoValorPoupanca = _poupancaDAL.ObterDadosPoupancaPorId(idPoupanca);
            }

            return usuario;
        }
    }
}
