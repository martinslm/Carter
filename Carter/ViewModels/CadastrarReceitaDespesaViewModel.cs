using Carter.DAL;
using Carter.Enums;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Carter.ViewModels
{
    public class CadastrarReceitaDespesaViewModel : BindableObject
    {
        #region [Atributos Privados]
        private CategoriaDAL _categoriaDAL = new CategoriaDAL();
        private DespesaDAL _despesaDAL = new DespesaDAL();
        private ReceitaDAL _receitaDAL = new ReceitaDAL();
        private List<TipoLancamento> _opcoesLancamento { get; set; }
        private TipoLancamento _tipoLancamentoSelecionado { get; set; }
        private DateTime _data { get; set; }
        private string _descricao { get; set; }
        private Categoria _categoriaLancamento { get; set; }
        private decimal _valor { get; set; }
        private int _totalParcelas { get; set; }
        private int _parcelaAtual { get; set; }
        private bool _valorPago { get; set; }
        private string _textAvisoCadastro { get; set; }
        private ICommand _cadastrarEFecharCommand { get; set; }
        private ICommand _cancelarCommand { get; set; }
        private ICommand _cadastrarCommand { get; set; }
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
                RaisePropertyChanged("DetalhesDespesasVisibility");
                return _tipoLancamentoSelecionado;
            }
            set
            {
                _tipoLancamentoSelecionado = value;
            }
        }
        public DateTime Data
        {
            get
            {
                RaisePropertyChanged("CadastrarCommand");
                RaisePropertyChanged("CadastrarEFecharCommand");
                return _data;
            }
            set
            {
                _data = value;
            }
        }
        public string Descricao
        {
            get
            {
                RaisePropertyChanged("CadastrarCommand");
                RaisePropertyChanged("CadastrarEFecharCommand");
                return _descricao;
            }
            set
            {
                _descricao = value;
            }
        }
        public List<Categoria> Categorias
        {
            get
            {
                return _categoriaDAL.ObterCategoriasPorUsuarioLogado();
            }
        }
        public Categoria CategoriaLancamento
        {
            get
            {
                RaisePropertyChanged("CadastrarCommand");
                RaisePropertyChanged("CadastrarEFecharCommand");
                return _categoriaLancamento;
            }
            set
            {
                _categoriaLancamento = value;
            }
        }
        public decimal Valor
        {
            get
            {
                RaisePropertyChanged("CadastrarCommand");
                RaisePropertyChanged("CadastrarEFecharCommand");
                return _valor;
            }
            set
            {
                _valor = value;
            }
        }
        public int TotalParcelas
        {
            get
            {
                RaisePropertyChanged("CadastrarCommand");
                RaisePropertyChanged("CadastrarEFecharCommand");
                return _totalParcelas;
            }
            set
            {
                _totalParcelas = value;
            }
        }
        public int ParcelaAtual
        {
            get
            {
                RaisePropertyChanged("CadastrarCommand");
                RaisePropertyChanged("CadastrarEFecharCommand");
                return _parcelaAtual;
            }
            set
            {
                _parcelaAtual = value;
            }
        }
        public bool ValorPago
        {
            get
            {
                return _valorPago;
            }
            set
            {
                _valorPago = value;
            }
        }
        public string TextAvisoCadastro
        {
            get
            {
                return _textAvisoCadastro;
            }
            set
            {
                _textAvisoCadastro = value;
            }
        }
        public Visibility DetalhesDespesasVisibility
        {
            get
            {
                return _tipoLancamentoSelecionado == TipoLancamento.Despesa ? Visibility.Visible : Visibility.Collapsed; ;
            }
        }
        public ICommand CancelarCommand
        {
            get
            {
                return _cancelarCommand; 
            }
        }
        public ICommand CadastrarCommand
        {
            get
            {
                return _cadastrarCommand;
            }
        }
        public ICommand CadastrarEFecharCommand
        {
            get
            {
                return _cadastrarEFecharCommand;
            }
        }
        public Action<bool> FecharTela { get; set; }
        #endregion
        #region [Construtor]
        public CadastrarReceitaDespesaViewModel()
        {
            _opcoesLancamento = new List<TipoLancamento>() { TipoLancamento.Receita, TipoLancamento.Despesa};
            _data = DateTime.Now;
            InstanciarCommands();

        }
        #endregion
        #region [Methods]
        private void InstanciarCommands()
        {
            _cadastrarCommand = new CommandHandler(p => InserirLancamento(false), p => PodeInserirLancamento());
            _cadastrarEFecharCommand = new CommandHandler(p => InserirLancamento(true), p => PodeInserirLancamento());
            _cancelarCommand = new CommandHandler(p => CancelarOperacao());

        }

        private void InserirLancamento(bool fecharTela)
        {
            try
            {
                switch (_tipoLancamentoSelecionado)
                {
                    case TipoLancamento.Despesa:
                        var novaDespesa = new Despesas()
                        {
                            DataVencimento = _data,
                            Valor = _valor,
                            Categoria = _categoriaLancamento,
                            Descricao = _descricao,
                            Pago = _valorPago,
                            ParcelaAtual = _parcelaAtual,
                            TotalParcelas = _totalParcelas
                        };
                        _despesaDAL.CadastrarNovaDespesa(novaDespesa);
                        break;
                    case TipoLancamento.Receita:
                        var novaReceita = new Receitas()
                        {
                            Data = _data,
                            Valor = _valor,
                            Categoria = _categoriaLancamento,
                            Descricao = _descricao
                        };
                        _receitaDAL.CadastrarNovaReceita(novaReceita);
                        break;
                }

                if(fecharTela)
                FecharTela(true);
            }
            catch (Exception ex)
            {
                var teste = ex;
                _textAvisoCadastro = string.Format("Houve um erro ao cadastrar a {0} informada.", _tipoLancamentoSelecionado);
                RaisePropertyChanged("textAvisoCadastro");
            }
        }

        private void CancelarOperacao()
        {
            FecharTela(false);
        }

        private bool PodeInserirLancamento()
        {
            if (_tipoLancamentoSelecionado == TipoLancamento.Despesa && (_parcelaAtual <= 0 || _totalParcelas < _parcelaAtual))
                return false;

            if (_categoriaLancamento == null || _valor <= 0 || _data == DateTime.MinValue)
                return false;

            return true;
        }
        #endregion
    }
}
