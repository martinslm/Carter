using Carter.Telas;
using Carter.ViewModels;
using System;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace Carter
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class Login : Window
    {
        private readonly LoginViewModel _viewmodel;
        public Login()
        {
            _viewmodel = new LoginViewModel();
            DataContext = _viewmodel;
            InitializeComponent();
            InitializeDelegates();
        }
        private void InitializeDelegates()
        {
            _viewmodel.AbrirTelaCadastroUsuario = AbrirTelaCadastroUsuario;
            _viewmodel.AbrirTelaEsqueceuASenha = AbrirTelaEsqueceuASenha;
        }

        private void AbrirTelaCadastroUsuario()
        {
            var cadastroUsuario = new CadastroUsuario();
            cadastroUsuario.Owner = this;
            cadastroUsuario.ShowDialog();


        }
        private void AbrirTelaEsqueceuASenha()
        {

        }
        
        public new bool? ShowDialog()
        {
            Owner.Opacity = 0.85;
            return base.ShowDialog();
        }

        private void Fechartela(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
