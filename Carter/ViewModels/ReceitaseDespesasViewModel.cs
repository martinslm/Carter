using Carter.DAL;
using Carter.Models;

namespace Carter.ViewModels
{
    class ReceitaseDespesasViewModel : BindableObject
    {
        private ReceitaDAL _receitaDAL = new ReceitaDAL();
        private DespesaDAL _despesaDAL = new DespesaDAL();
        private Receitas _receita;
        private Despesas _despesa;
        public ReceitaseDespesasViewModel()
        {

        }
    }
}
