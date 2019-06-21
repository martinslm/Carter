using Carter.DAL;
using Carter.Enums;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Carter.ViewModels
{
    public class ReceitaseDespesasViewModel : BindableObject
    {
        #region [Propriedades Privadas]
        private ReceitaDAL _receitaDAL = new ReceitaDAL();
        private DespesaDAL _despesaDAL = new DespesaDAL();
        private IEnumerable<Receitas> _receitas;
        private IEnumerable<Despesas> _despesas;
        private bool _mostrarBotaoPagarEnabled;
        private ICommand _cadastrarLancamentoCommand;
        private ICommand _botaoSeisMesesCommand;
        private ICommand _botaoMesAtualCommand;
        private ICommand _inserirSalarioCommand;
        #endregion
        #region [Priopriedades Públicas]
        public bool MostrarBotaoPagarEnabled
        {
            get
            { return _mostrarBotaoPagarEnabled; }
            set
            { _mostrarBotaoPagarEnabled = value; }
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
        public Action AbrirTelaCadastroLancamentoFinanceiro;
        #endregion

        public ReceitaseDespesasViewModel()
        {
            FiltrarListagemMesAtual();
            InstanciarCommands();
        }

        private void InstanciarCommands()
        {
            _botaoMesAtualCommand = new CommandHandler(p => FiltrarListagemMesAtual());
            _botaoSeisMesesCommand = new CommandHandler(p => FiltrarListagemUltimosSeisMeses());
            _cadastrarLancamentoCommand = new CommandHandler(p => AbrirTelaCadastroLancamentoFinanceiro());
            _inserirSalarioCommand = new CommandHandler(p => InserirSalario());
        }

        private void InserirSalario()
        {
            _receitaDAL.InserirSalario();
            _receitas = _receitaDAL.ObterReceitasPorPeriodo(PeriodoRelatorio.MesAtual);
            RaisePropertyChanged("Receitas");
        }

        private void FiltrarListagemUltimosSeisMeses()
        {
            _receitas = _receitaDAL.ObterReceitasPorPeriodo(PeriodoRelatorio.UltimosSeisMeses);
            _despesas = _despesaDAL.ObterDespesasPorPeriodo(PeriodoRelatorio.UltimosSeisMeses);
            RaisePropertyChanged("Receitas");
            RaisePropertyChanged("Despesas");
        }

        private void FiltrarListagemMesAtual()
        {
            _receitas = _receitaDAL.ObterReceitasPorPeriodo(PeriodoRelatorio.MesAtual);
            _despesas = _despesaDAL.ObterDespesasPorPeriodo(PeriodoRelatorio.MesAtual);
        }
    }
}
