using System;
using System.Collections.Generic;
using Carter.Enums;
using Carter.Models;

namespace Carter.DAL
{
    public class ReceitaDAL
    {
        public IEnumerable<Receitas> ObterReceitasPorPeriodo(PeriodoRelatorio mesAtual, DateTime DataInicio = new DateTime(), DateTime DataFinal = new DateTime())
        {
            return new List<Receitas>();
        }
    }
}
