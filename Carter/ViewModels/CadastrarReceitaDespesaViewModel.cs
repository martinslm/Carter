using Carter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter.ViewModels
{
    class CadastrarReceitaDespesaViewModel : BindableObject
    {
        #region [Atributos Privados]
        private List<TipoLancamento> _opcoesLancamento { get; set; }
        private TipoLancamento _tipoLancamentoSelecionado { get; set; }
        #endregion
        #region [Atributos Publicos]
        public List<TipoLancamento> OpcoesLancamento
        {
            get
            {
                return _opcoesLancamento;
            }
            set
            { _opcoesLancamento = value; }
        }
        public TipoLancamento TipoLancamentoSelecionado
        {
            get
            {
                return _tipoLancamentoSelecionado;
            }
            set
            {
                _tipoLancamentoSelecionado = value;
            }
        }
        #endregion
        #region [Construtor]
        public CadastrarReceitaDespesaViewModel()
        {
            _opcoesLancamento = new List<TipoLancamento>();
            _opcoesLancamento.Add(TipoLancamento.Entrada);
            _opcoesLancamento.Add(TipoLancamento.Saida);

        }
        #endregion
        #region [Methods]
        #endregion
    }
}
