using Carter.ViewModels;
using System.Windows;

namespace Carter.Telas
{
    /// <summary>
    /// Lógica interna para MinhaConta.xaml
    /// </summary>
    public partial class MinhaConta : Window
    {
        private readonly MinhaConta _viewmodel;
        /*
        public MinhaConta()
        {
            _viewmodel = new MinhaConta();
            InitializeComponent();
            DataContext = _viewmodel;
            viewmodel.AtribuirSenhas = AtribuirSenhas;
            _viewmodel.FecharTela = FecharTela;
        }
        private void AtribuirSenhas()
        {
            _viewmodel.Senha = SenhaCadastro.Password;
            _viewmodel.ConfirmacaoSenha = SenhaConfirmacao.Password;
        }
        */
        private void FecharTela(bool dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
