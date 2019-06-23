using Carter.DAL;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Carter.ViewModels
{
    class HistoricoDeSalariosViewModel : BindableObject
    {
        private SalarioDAL _salarioDAL = new SalarioDAL();
        private decimal _novoSalario;
        private bool _salarioAtual;
        private string _textAvisoCadastro;
        private IEnumerable<Salario> _salarios;
        private ICommand _cadastrarCommand;

        public IEnumerable<Salario> Salarios
        {
            get { return _salarios; }
        }
        public decimal NovoSalario
        {
            get { return _novoSalario; }
            set { _novoSalario = value; }
        }
        public bool SalarioAtual
        {
            get { return _salarioAtual; }
            set { _salarioAtual = value; }
        }
        public string TextAvisoCadastro
        {
            get { return _textAvisoCadastro; }
            set { _textAvisoCadastro = value; }
        }
        public ICommand CadastrarCommand
        {
            get { return _cadastrarCommand; }
        }

        public HistoricoDeSalariosViewModel()
        {
            _salarios = _salarioDAL.ObterHistoricoSalariosPorUsuario();
            RaisePropertyChanged("Salarios");
            InstanciarCommands();
        }

        private void InstanciarCommands()
        {
            _cadastrarCommand = new CommandHandler(p => CadastrarNovoSalario());
        }

        private void CadastrarNovoSalario()
        {
            try
            {
                if (NovoSalario <= 0)
                {
                    _textAvisoCadastro = "O valor informado para cadastro é inválido.";
                    RaisePropertyChanged("TextAvisoCadastro");
                    return;
                }

                var idSalario = _salarioDAL.InserirSalarioPorUsuarioLogado(NovoSalario);

                if (idSalario > 0 && SalarioAtual)
                    _salarioDAL.AtualizarSalarioAtualUsuario(idSalario);

                _textAvisoCadastro = "Salário cadastrado com sucesso!";
                RaisePropertyChanged("TextAvisoCadastro");
                AtualizarListagemSalarios();
            }
            catch (Exception ex)
            {
                var teste = ex;
                _textAvisoCadastro = "Houve um erro ao cadastrar o salário informado.";
                RaisePropertyChanged("TextAvisoCadastro");
            }

        }

        private void AtualizarListagemSalarios()
        {
            _salarios = _salarioDAL.ObterHistoricoSalariosPorUsuario();
            RaisePropertyChanged("Salarios");
        }
    }
}
