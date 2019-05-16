using Carter.Enums;
using Carter.Models;
using System;
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
    }
}
