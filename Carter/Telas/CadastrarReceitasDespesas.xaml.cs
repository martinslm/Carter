using Carter.Enums;
using Carter.ViewModels;
using System.Windows;

namespace Carter.Telas
{
    /// <summary>
    /// Interação lógica para CadastrarReceitasDespesas.xam
    /// </summary>
    public partial class CadastrarReceitasDespesas : Window
    {
        private readonly CadastrarReceitaDespesaViewModel _viewmodel;

        public CadastrarReceitasDespesas()
        {
            _viewmodel = new CadastrarReceitaDespesaViewModel();
            InitializeComponent();
            DataContext = _viewmodel;
            _viewmodel.FecharTela = FecharTela;
        }

        private void FecharTela(bool dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
