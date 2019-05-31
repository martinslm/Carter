using Carter.ViewModels;
using System.Windows;

namespace Carter.Telas
{
    /// <summary>
    /// Interação lógica para CadastroUsuario.xam
    /// </summary>
    public partial class CadastroUsuario : Window
    {
        private readonly CadastroUsuarioViewModel _viewmodel;

        public CadastroUsuario()
        {
            _viewmodel = new CadastroUsuarioViewModel();
            InitializeComponent();
            DataContext = _viewmodel;
            _viewmodel.AtribuirSenhas = AtribuirSenhas;
            _viewmodel.FecharTela = FecharTela;
        }
        private void AtribuirSenhas()
        {
            _viewmodel.Senha = SenhaCadastro.Password;
            _viewmodel.ConfirmacaoSenha = SenhaConfirmacao.Password;
        }

        private void FecharTela(bool dialogResult)
        {
            DialogResult = dialogResult;     
        }

    }
}
