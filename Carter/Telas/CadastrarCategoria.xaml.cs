using Carter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Carter.Telas
{
    /// <summary>
    /// Interação lógica para Categoria.xam
    /// </summary>
    public partial class CadastrarCategoria : Page
    {
        private CadastrarCategoriaViewModel _viewModel = new CadastrarCategoriaViewModel();
        public CadastrarCategoria()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.AbrirTelaCategorias = AbrirTelaCategorias;
        }
        private void AbrirTelaCategorias()
        {
            var cadastrarCategorias = new CadastrarReceitasDespesas();
            cadastrarCategorias.Owner = Application.Current.MainWindow;
            cadastrarCategorias.ShowDialog();
        }

    }
}
