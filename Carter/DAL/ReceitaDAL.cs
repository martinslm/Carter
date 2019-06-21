using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Carter.Enums;
using Carter.Models;

namespace Carter.DAL
{
    public class ReceitaDAL
    {
        private CategoriaDAL _categoriaDAL = new CategoriaDAL();

        public IEnumerable<Receitas> ObterReceitasPorPeriodo(PeriodoRelatorio periodoRelatorio)
        {
            var dataInicio = new DateTime();
            var dataFim = new DateTime();
            List<Receitas> receitas = new List<Receitas>();
            switch (periodoRelatorio)
            {
                case PeriodoRelatorio.MesAtual:
                    dataInicio = Convert.ToDateTime(string.Format("01/{0}/{1}", DateTime.Now.Month, DateTime.Now.Year));
                    dataFim = Convert.ToDateTime(string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month),DateTime.Now.Month, DateTime.Now.Year));
                    break;
                case PeriodoRelatorio.UltimosSeisMeses:
                    dataInicio = DateTime.Now.AddMonths(-6);
                    dataFim = DateTime.Now;
                    break;
            }

            var strsql = @"SELECT id_recebimento
	                                ,data_recebimento
	                                ,valor
	                                ,id_categoria
	                                ,descricao
                                FROM recebimentos
                                WHERE id_usuario = @idUsuario
	                                AND data_recebimento BETWEEN @dataInicio
		                                AND @dataFim";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);
                busca.Parameters.AddWithValue("@dataInicio", dataInicio);
                busca.Parameters.AddWithValue("@dataFim", dataFim);

                using (var reader = busca.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var receita = new Receitas()
                        {
                            Id = Convert.ToInt32(reader["id_recebimento"]),
                            Valor = Convert.ToDecimal(reader["valor"]),
                            Data = Convert.ToDateTime(reader["data_recebimento"]),
                            Categoria = new Categoria() { Id = Convert.ToInt32(reader["id_categoria"])},
                            Descricao = reader["descricao"].ToString()
                        };
                        receitas.Add(receita);
                    }
                }

                foreach(var rec in receitas)
                {
                    rec.Categoria = _categoriaDAL.ObterDadosCategoriaPorId(rec.Categoria.Id);
                }

                return receitas;
            }
        }

        public void InserirSalario()
        {
            var strsql = @"INSERT INTO recebimentos (
	                                data_recebimento
	                                ,valor
	                                ,id_categoria
	                                ,descricao
	                                ,id_usuario
	                                )
                                VALUES (
	                                GETDATE()
	                                ,(
		                                SELECT salario
		                                FROM historico_salarios hs
		                                INNER JOIN usuario u ON (u.salario_atual = hs.id_salario)
		                                WHERE u.id_usuario = @idUsuario
		                                )
	                                ,9
	                                ,'Recebimento de Salario'
	                                ,@idUsuario
	                                )";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                command.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);

                command.ExecuteNonQuery();
            }
        }
    }
}
