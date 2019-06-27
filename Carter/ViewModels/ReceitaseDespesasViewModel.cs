using Carter.DAL;
using Carter.Enums;
using Carter.Models;
using Carter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Carter.ViewModels
{
    public class ReceitaseDespesasViewModel : BindableObject
    {
        #region [Propriedades Privadas]
        private readonly ServicoReceitasEDespesas _servicoReceitaDespesa;
        private ReceitaDAL _receitaDAL = new ReceitaDAL();
        private DespesaDAL _despesaDAL = new DespesaDAL();
        private string _valorTotal;
        private string _valorTotalColor;
        private string _alertaMarcarComoPago;
        private Despesas _despesaSelecionada;
        private IEnumerable<Receitas> _receitas;
        private IEnumerable<Despesas> _despesas;
        private bool _mostrarBotaoPagarEnabled;
        private ICommand _cadastrarLancamentoCommand;
        private ICommand _botaoSeisMesesCommand;
        private ICommand _botaoMesAtualCommand;
        private ICommand _inserirSalarioCommand;
        private ICommand _marcarComoPagoCommand;
        #endregion
        #region [Priopriedades Públicas]
        public string AlertaMarcarComoPago
        {
            get
            {
                return _alertaMarcarComoPago;
            }
            set
            {
                _alertaMarcarComoPago = value; 
            }
        }
        public string ValorTotal
        {
            get
            {
                return _valorTotal;
            }
            set
            {
                _valorTotal = value;
            }
        }
        public string ValorTotalColor
        {
            get
            {
                return _valorTotalColor;
            }
            set
            {
                _valorTotalColor = value;
            }
        }
        public bool MostrarBotaoPagarEnabled
        {
            get
            { return _mostrarBotaoPagarEnabled; }
            set
            { _mostrarBotaoPagarEnabled = value; }
        }
        public Despesas DespesaSelecionada
        {
            get
            {
                return _despesaSelecionada;
            }
            set
            {
                _despesaSelecionada = value;
            }
        }
        public IEnumerable<Receitas> Receitas
        {
            get { return _receitas; }
        }
        public IEnumerable<Despesas> Despesas
        {
            get { return _despesas; }
        }
        public ICommand CadastrarLancamentoCommand
        {
            get
            {
                return _cadastrarLancamentoCommand;
            }
            set
            {
                _cadastrarLancamentoCommand = value;
            }
        }
        public ICommand BotaoSeisMesesCommand
        {
            get
            {
                return _botaoSeisMesesCommand;
            }
            set
            {
                _botaoSeisMesesCommand = value;
            }
        }
        public ICommand BotaoMesAtualCommand
        {
            get
            {
                return _botaoMesAtualCommand;
            }
            set
            {
                _botaoMesAtualCommand = value;
            }
        }
        public ICommand InserirSalarioCommand
        {
            get
            {
                return _inserirSalarioCommand;
            }
            set
            {
                _inserirSalarioCommand = value;
            }
        }
        public ICommand MarcarComoPagoCommand
        {
            get
            {
                return _marcarComoPagoCommand;
            }
            set
            {
                _marcarComoPagoCommand = value;
            }
        }
        public Action AbrirTelaCadastroLancamentoFinanceiro;
        #endregion

        public ReceitaseDespesasViewModel()
        {
            _servicoReceitaDespesa = new ServicoReceitasEDespesas();
            FiltrarListagem(PeriodoRelatorio.MesAtual);
            InstanciarCommands();
        }

        private void InstanciarCommands()
        {
            _botaoMesAtualCommand = new CommandHandler(p => FiltrarListagem(PeriodoRelatorio.MesAtual));
            _botaoSeisMesesCommand = new CommandHandler(p => FiltrarListagem(PeriodoRelatorio.UltimosSeisMeses));
            _cadastrarLancamentoCommand = new CommandHandler(p => CadastrarLancamento());
            _inserirSalarioCommand = new CommandHandler(p => InserirSalario());
            _marcarComoPagoCommand = new CommandHandler(p => MarcarComoPago());
        }

        private void MarcarComoPago()
        {
            try
            {
                if (_despesaSelecionada.Pago)
                {
                    _alertaMarcarComoPago = "Erro: Fatura não está pendente";
                    RaisePropertyChanged("AlertaMarcarComoPago");
                    return;
                }
                _despesaDAL.BaixarPagamento(_despesaSelecionada.Id);
                FiltrarListagem(PeriodoRelatorio.MesAtual);
            }
            catch
            {
                _alertaMarcarComoPago = "Erro: Selecione uma despesa válida.";
                RaisePropertyChanged("AlertaMarcarComoPago");
            }
        }

        private void CadastrarLancamento()
        {
            AbrirTelaCadastroLancamentoFinanceiro();
            FiltrarListagem(PeriodoRelatorio.MesAtual);
        }

        private void InserirSalario()
        {
            _receitaDAL.InserirSalario();
            _receitas = _servicoReceitaDespesa.ObterReceitasPorPeriodo(PeriodoRelatorio.MesAtual);
            AtualizarValorTotal();
            RaisePropertyChanged("Receitas");
        }

        private void FiltrarListagem(PeriodoRelatorio periodo)
        {
            _receitas = _servicoReceitaDespesa.ObterReceitasPorPeriodo(periodo);
            _despesas = _servicoReceitaDespesa.ObterDespesasPorPeriodo(periodo);
            AtualizarValorTotal();
            RaisePropertyChanged("Receitas");
            RaisePropertyChanged("Despesas");
        }

        private void AtualizarValorTotal()
        {
            _valorTotal = _servicoReceitaDespesa.ObterValorTotalEmConta(_despesas, _receitas);
            _valorTotalColor = _valorTotal.Contains("-") ? "DarkRed" : "#3CB371";
            RaisePropertyChanged("ValorTotal");
            RaisePropertyChanged("ValorTotalColor");
        }
    }
}
