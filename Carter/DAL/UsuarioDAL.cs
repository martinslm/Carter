using Carter.Enums;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Carter.DAL
{
    class UsuarioDAL
    {
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
                    if(!reader.Read()) { return Enums.StatusLogin.EmailInvalido; }
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

        internal List<Categoria> BuscarCategorias()
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
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void cadastrarUsuario()
        {

        }
    }
}
