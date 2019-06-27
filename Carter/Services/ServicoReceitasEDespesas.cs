using Carter.DAL;
using Carter.Enums;
using Carter.Models;
using System.Collections.Generic;

namespace Carter.Services
{
    public class ServicoReceitasEDespesas
    {
        private readonly ReceitaDAL _receitaDAL;
        private readonly DespesaDAL _despesaDAL;

        public ServicoReceitasEDespesas()
        {
            _receitaDAL = new ReceitaDAL();
            _despesaDAL = new DespesaDAL();
        }

        public IEnumerable<Receitas> ObterReceitasPorPeriodo (PeriodoRelatorio periodo)
        {
            return _receitaDAL.ObterReceitasPorPeriodo(periodo);
        }

        public IEnumerable<Despesas> ObterDespesasPorPeriodo(PeriodoRelatorio periodo)
        {
            return _despesaDAL.ObterDespesasPorPeriodo(periodo);
        }

        public string ObterValorTotalEmConta(IEnumerable<Despesas> despesas, IEnumerable<Receitas> receitas, bool apenasValor = false)
        {
            try
            {
                decimal totalReceitas = 0, totalDespesas = 0;
                foreach (var rec in receitas)
                {
                    totalReceitas += rec.Valor;
                }
                foreach (var desp in despesas)
                {
                    if (desp.Pago)
                        totalDespesas += desp.Valor;
                }

                if(apenasValor)
                    return string.Format("R${0:N2}", totalReceitas - totalDespesas);

                return string.Format("   Total: R$ {0:N2}", totalReceitas - totalDespesas);
            }
            catch
            {
                return "R$0,00";
            }
        }


    }
}
