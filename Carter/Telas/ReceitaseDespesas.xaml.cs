using Carter.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Carter.Telas
{
    /// <summary>
    /// Interação lógica para ReceitaseDespesas.xam
    /// </summary>
    public partial class ReceitaseDespesas : Page
    {
        private ReceitaseDespesasViewModel _viewModel = new ReceitaseDespesasViewModel();
        public ReceitaseDespesas()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.AbrirTelaCadastroLancamentoFinanceiro = AbrirTelaCadastroLancamentoFinanceiro;
        }

        private void AbrirTelaCadastroLancamentoFinanceiro()
        {
            var cadastrarReceitasDespesas = new CadastrarReceitasDespesas();
            cadastrarReceitasDespesas.Owner = Application.Current.MainWindow;
            cadastrarReceitasDespesas.ShowDialog();
        }
    }
}
