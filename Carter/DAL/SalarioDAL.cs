using Carter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter.DAL
{
    public class SalarioDAL
    {
        public int InserirSalarioEObterId(decimal ValorSalario)
        {
            string strsql = @" INSERT INTO historico_salarios 
                              (salario, data_cadastro)
                              VALUES(@ValorSalario, GETDATE());

                            SELECT IDENT_CURRENT('historico_salarios') AS id_salario";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@ValorSalario", ValorSalario);

                using (var reader = busca.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["id_salario"]);
                    }
                }
            }

            return 0;
        }

        public void VincularIdUsuarioAoSalario(int idSalario, int idUsuario)
        {
            string strsql = @" UPDATE historico_salarios 
                               SET id_usuario = @idUsuario
                               WHERE id_salario = @idSalario";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@idSalario", idSalario);

                command.ExecuteNonQuery();
            }
        }

        public Salario ObterDadosSalarioPorId(int idSalario)
        {
            string strsql = @"SELECT salario
	                                ,data_cadastro
                                FROM historico_salarios
                                WHERE id_salario = @idSalario";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@idSalario", idSalario);
                using (var reader = busca.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Salario()
                        {
                            Id = idSalario,
                            DataCadastro = Convert.ToDateTime(reader["data_cadastro"]),
                            Valor = Convert.ToDecimal(reader["salario"])
                        };
                    }
                }
            }

            return new Salario();
        }

        internal void AtualizarSalarioAtualUsuario(int idSalario)
        {
            string strsql = @" UPDATE usuario
                               SET salario_atual = @idSalario
                               WHERE id_usuario = @idUsuario";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);
                command.Parameters.AddWithValue("@idSalario", idSalario);

                command.ExecuteNonQuery();
            }

            Sessao.Usuario.SalarioAtual = ObterDadosSalarioPorId(idSalario);
        }

        public int InserirSalarioPorUsuarioLogado(decimal novoSalario)
        {
            string strsql = @" INSERT INTO historico_salarios 
                              (salario, data_cadastro, id_usuario)
                              VALUES(@novoSalario, GETDATE(), @idUsuario);

                            SELECT IDENT_CURRENT('historico_salarios') AS id_salario";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@novoSalario", novoSalario);
                busca.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);

                using (var reader = busca.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["id_salario"]);
                    }
                }
            }
            return 0;
        }

        public IEnumerable<Salario> ObterHistoricoSalariosPorUsuario()
        {
            var salarios = new List<Salario>();
            var strsql = @"SELECT id_salario
	                            ,salario
	                            ,data_cadastro
                            FROM historico_salarios
                            WHERE id_usuario = @idUsuario";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);
                using (var reader = busca.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var salario = new Salario()
                        {
                            Id = Convert.ToInt32(reader["id_salario"]),
                            DataCadastro = Convert.ToDateTime(reader["data_cadastro"]),
                            Valor = Convert.ToDecimal(reader["salario"])
                        };

                        salarios.Add(salario);
                    }
                }
                return salarios;
            }
        }

        public List<Salario> ObterDoisUltimosSalarios()
        {
            var salarios = new List<Salario>();
            var strsql = @"SELECT TOP 2 id_salario
                                        , salario
                                        , data_cadastro
                                    FROM historico_salarios
                                    WHERE id_usuario = @idUsuario
                                    ORDER BY id_salario DESC";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);

                using (var reader = busca.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var salario = new Salario()
                        {
                            Id = Convert.ToInt32(reader["id_salario"]),
                            DataCadastro = Convert.ToDateTime(reader["data_cadastro"]),
                            Valor = Convert.ToDecimal(reader["salario"])
                        };

                        salarios.Add(salario);
                    }
                }
            }
            return salarios;
        }
    }
}
