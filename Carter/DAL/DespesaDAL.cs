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

        public void CadastrarNovaDespesa(Despesas novaDespesa)
        {
            var strsql = @"INSERT INTO despesas (
	                                    valor
	                                    ,data_vencimento
	                                    ,categoria_despesa
	                                    ,parcela_atual
	                                    ,total_parcelas
	                                    ,valor_pago
	                                    ,id_usuario
	                                    ,descricao
	                                    )
                                    VALUES (
	                                    @valor
	                                    ,@data
	                                    ,@categoria
	                                    ,@parcelaAtual
	                                    ,@parcelaTotal
	                                    ,@pago
	                                    ,@idUsuario
	                                    ,@descricao
	                                    )";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                while (novaDespesa.ParcelaAtual <= novaDespesa.TotalParcelas)
                {
                    command.Parameters.AddWithValue("@idUsuario", Sessao.Usuario.Id);
                    command.Parameters.AddWithValue("@data", novaDespesa.DataVencimento);
                    command.Parameters.AddWithValue("@valor", novaDespesa.Valor);
                    command.Parameters.AddWithValue("@categoria", novaDespesa.Categoria.Id);
                    command.Parameters.AddWithValue("@descricao", novaDespesa.Descricao);
                    command.Parameters.AddWithValue("parcelaAtual", novaDespesa.ParcelaAtual);
                    command.Parameters.AddWithValue("@parcelaTotal", novaDespesa.TotalParcelas);
                    command.Parameters.AddWithValue("@pago", novaDespesa.Pago == true ? 1 : 0);

                    command.ExecuteNonQuery();

                    command.Parameters.Clear();

                    novaDespesa.DataVencimento = novaDespesa.DataVencimento.AddMonths(1);
                    if (novaDespesa.DataVencimento.Month > DateTime.Now.Month && novaDespesa.Pago == true)
                        novaDespesa.Pago = false;
                    novaDespesa.ParcelaAtual += 1;
                }
            }

        }

        public void BaixarPagamento(int idDespesa)
        {
            var strsql = @"UPDATE despesas
                            SET valor_pago = 1
                            WHERE id_despesas = @idDespesa";

            using (var command = new SqlCommand(strsql, Conexao.Conectar()))
            {
                    command.Parameters.AddWithValue("@idDespesa", idDespesa);
                    command.ExecuteNonQuery();
            }
        }
    }
}
