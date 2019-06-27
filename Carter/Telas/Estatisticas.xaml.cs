using Carter.ViewModels;
using System.Windows.Controls;

namespace Carter.Telas
{
    /// <summary>
    /// Interação lógica para Estatisticas.xam
    /// </summary>
    public partial class Estatisticas : Page
    {
        private readonly EstatisticaViewModel _viewmodel;

        public Estatisticas()
        {
            _viewmodel = new EstatisticaViewModel();
            InitializeComponent();
            DataContext = _viewmodel;
        }
    }
}
