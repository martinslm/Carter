using Carter.ViewModels;
using System.Windows;

namespace Carter
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class Login : Window
    {
        LoginViewModel _viewmodel = new LoginViewModel();
        public Login()
        {
            InitializeComponent();
            DataContext = _viewmodel;
        }
        //Senha.Password
    }
}
