using Carter.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Carter.Telas
{
    /// <summary>
    /// Lógica interna para MinhaConta.xaml
    /// </summary>
    public partial class MinhaConta : Page
    {
        private readonly MinhaContaViewModel _viewmodel;
        
        public MinhaConta()
        {
            _viewmodel = new MinhaContaViewModel();
            InitializeComponent();
            DataContext = _viewmodel;
        }        

    }
}
