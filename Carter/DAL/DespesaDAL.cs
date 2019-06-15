using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carter.Enums;
using Carter.Models;

namespace Carter.DAL
{
    public class DespesaDAL
    {
        public IEnumerable<Despesas> ObterDespesasPorPeriodo(PeriodoRelatorio mesAtual, DateTime DataInicio = new DateTime(), DateTime DataFinal = new DateTime())
        {
            throw new NotImplementedException();
        }
    }
}
