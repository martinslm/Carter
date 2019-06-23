using Carter.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace Carter.ViewModels
{
    public class PrincipalViewModel : BindableObject
    {
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
    }
}
