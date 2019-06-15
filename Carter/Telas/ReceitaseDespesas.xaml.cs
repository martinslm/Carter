using Carter.ViewModels;
using System.Windows.Controls;

namespace Carter.Telas
{
    /// <summary>
    /// Interação lógica para ReceitaseDespesas.xam
    /// </summary>
    public partial class ReceitaseDespesas : Page
    {
        private readonly ReceitaseDespesasViewModel _viewModel = new ReceitaseDespesasViewModel();
        public ReceitaseDespesas()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
