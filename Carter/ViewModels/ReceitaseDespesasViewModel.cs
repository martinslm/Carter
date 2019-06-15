using Carter.DAL;
using Carter.Enums;
using Carter.Models;
using System.Collections.Generic;

namespace Carter.ViewModels
{
    class ReceitaseDespesasViewModel : BindableObject
    {
        #region [Propriedades Privadas]
        private ReceitaDAL _receitaDAL = new ReceitaDAL();
        private DespesaDAL _despesaDAL = new DespesaDAL();
        private IEnumerable<Receitas> _receitas;
        private IEnumerable<Despesas> _despesas;
        #endregion
        #region [Propriedades Públicas]
        public IEnumerable<Receitas> Receitas
        {
            get { return _receitas; }
        }
        public IEnumerable<Despesas> Despesas
        {
            get { return _despesas; }
        }
        #endregion
        public ReceitaseDespesasViewModel()
        {
            _receitas = _receitaDAL.ObterReceitasPorPeriodo(PeriodoRelatorio.MesAtual);
            _despesas = _despesaDAL.ObterDespesasPorPeriodo(PeriodoRelatorio.MesAtual);
        }
    }
}
