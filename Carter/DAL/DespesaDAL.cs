using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carter.Enums;
using Carter.Models;

namespace Carter.DAL
{
    public class DespesaDAL
    {
        private CategoriaDAL _categoriaDAL = new CategoriaDAL();

        public IEnumerable<Despesas> ObterDespesasPorPeriodo(PeriodoRelatorio periodoRelatorio)
        {
            var dataInicio = new DateTime();
            var dataFim = new DateTime();
            List<Despesas> despesas = new List<Despesas>();
            switch (periodoRelatorio)
            {
                case PeriodoRelatorio.MesAtual:
                    dataInicio = Convert.ToDateTime(string.Format("01/{0}/{1}", DateTime.Now.Month, DateTime.Now.Year));
                    dataFim = Convert.ToDateTime(string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), DateTime.Now.Month, DateTime.Now.Year));
                    break;
                case PeriodoRelatorio.UltimosSeisMeses:
                    dataInicio = DateTime.Now.AddMonths(-6);
                    dataFim = DateTime.Now;
                    break;
            }

            var strsql = @"SELECT id_despesas
	                                ,valor
	                                ,data_vencimento
	                                ,categoria_despesa
	                                ,descricao
	                                ,parcela_atual
	                                ,total_parcelas
	                                ,valor_pago
                                FROM despesas
                                WHERE id_usuario = @idUsuario
	                                AND data_vencimento BETWEEN @dataInicio
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
                        var despesa = new Despesas()
                        {
                            Id = Convert.ToInt32(reader["id_despesas"]),
                            Valor = Convert.ToDecimal(reader["valor"]),
                            DataVencimento = Convert.ToDateTime(reader["data_vencimento"]),
                            Categoria = new Categoria() { Id = Convert.ToInt32(reader["categoria_despesa"]) },
                            Descricao = reader["descricao"].ToString(),
                            ParcelaAtual = Convert.ToInt32(reader["parcela_atual"]),
                            TotalParcelas = Convert.ToInt32(reader["total_parcelas"]),
                            Pago = Convert.ToInt32(reader["valor_pago"]) == 1 ? true : false
                        };
                        despesas.Add(despesa);
                    }
                }

                foreach (var desp in despesas)
                {
                    desp.Categoria = _categoriaDAL.ObterDadosCategoriaPorId(desp.Categoria.Id);
                }

                return despesas;
            }
        }
    }
}
