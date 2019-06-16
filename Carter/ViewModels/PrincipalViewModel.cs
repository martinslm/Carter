using Carter.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace Carter.ViewModels
{
    public class PrincipalViewModel : BindableObject
    {
        private ICommand _historicoDeSalariosCommand;
        private ICommand _categoriasCommand;
        private Visibility _poupancaVisibility = Sessao.Usuario.UtilizaPoupanca ? Visibility.Visible : Visibility.Collapsed;
        public Visibility PoupancaVisibility
        {
            get { return _poupancaVisibility; }
            set
            {
                _poupancaVisibility = value;
                RaisePropertyChanged("PoupancaVisibility");
            }
        }
        public ICommand HistoricoDeSalariosCommand
        {
            get { return _historicoDeSalariosCommand; }
        }
        public ICommand CategoriasCommand
        {
            get { return _categoriasCommand; }
        }
        public Action AbrirTelaHistoricoSalarios;

        public Action AbrirCategorias;

        public PrincipalViewModel()
        {
            InstanciarCommands();
        }

        private void InstanciarCommands()
        {
            _historicoDeSalariosCommand = new CommandHandler(p => AbrirTelaHistoricoSalarios());
            _categoriasCommand = new CommandHandler(p => AbrirCategorias());
    }

    }
}
