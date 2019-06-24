using Carter.Telas;
using Carter.ViewModels;
using System;
using System.Windows;

namespace Carter.Telas
{
    /// <summary>
    /// Lógica interna para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        private readonly PrincipalViewModel _viewmodel;

        public Principal()
        {
            _viewmodel = new PrincipalViewModel();
            InitializeComponent();
            DataContext = _viewmodel;
            AbrirTelaEstatistica();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void Fechartela(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void HistoricoDeSalarios(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            framePrincipal.Navigate(new HistoricoDeSalarios());
        }

        private void ReceitasDespesas(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            framePrincipal.Navigate(new ReceitaseDespesas());
        }

        private void Categoria(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            framePrincipal.Navigate(new CadastrarCategoria());
        }

        private void Estatisticas(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AbrirTelaEstatistica();
        }

        private void AbrirTelaEstatistica()
        {
            framePrincipal.Navigate(new Estatisticas());
        }
    }
}
