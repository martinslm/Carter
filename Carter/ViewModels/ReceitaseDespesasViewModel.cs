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
        private string _valorTotal;
        private string _valorTotalColor;
        private IEnumerable<Receitas> _receitas;
        private IEnumerable<Despesas> _despesas;
        private bool _mostrarBotaoPagarEnabled;
        private ICommand _cadastrarLancamentoCommand;
        private ICommand _botaoSeisMesesCommand;
        private ICommand _botaoMesAtualCommand;
        private ICommand _inserirSalarioCommand;
        #endregion
        #region [Priopriedades Públicas]
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
            FiltrarListagem(PeriodoRelatorio.MesAtual);
            InstanciarCommands();
        }

        private void InstanciarCommands()
        {
            _botaoMesAtualCommand = new CommandHandler(p => FiltrarListagem(PeriodoRelatorio.MesAtual));
            _botaoSeisMesesCommand = new CommandHandler(p => FiltrarListagem(PeriodoRelatorio.UltimosSeisMeses));
            _cadastrarLancamentoCommand = new CommandHandler(p => CadastrarLancamento());
            _inserirSalarioCommand = new CommandHandler(p => InserirSalario());
        }

        private void CadastrarLancamento()
        {
            AbrirTelaCadastroLancamentoFinanceiro();
            FiltrarListagem(PeriodoRelatorio.MesAtual);
        }

        private void InserirSalario()
        {
            _receitaDAL.InserirSalario();
            _receitas = _receitaDAL.ObterReceitasPorPeriodo(PeriodoRelatorio.MesAtual);
            AtualizarValorTotal();
            RaisePropertyChanged("Receitas");
        }

        private void FiltrarListagem(PeriodoRelatorio periodo)
        {
            _receitas = _receitaDAL.ObterReceitasPorPeriodo(periodo);
            _despesas = _despesaDAL.ObterDespesasPorPeriodo(periodo);
            AtualizarValorTotal();
            RaisePropertyChanged("Receitas");
            RaisePropertyChanged("Despesas");
        }

        private void AtualizarValorTotal()
        {
            decimal totalReceitas = 0, totalDespesas = 0;
            foreach(var rec in _receitas)
            {
                totalReceitas += rec.Valor;
            }
            foreach(var desp in _despesas)
            {
                if(desp.Pago)
                totalDespesas += desp.Valor;
            }

            _valorTotal = string.Format("   Total: R$ {0:N2}", totalReceitas - totalDespesas);
            _valorTotalColor = (totalReceitas - totalDespesas) <= 0 ? "DarkRed" : "#3CB371";
            RaisePropertyChanged("ValorTotal");
            RaisePropertyChanged("ValorTotalColor");
        }
    }
}
