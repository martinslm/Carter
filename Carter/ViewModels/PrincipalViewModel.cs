using Carter.Models;
using System.Windows;

namespace Carter.ViewModels
{
    class PrincipalViewModel : BindableObject
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
