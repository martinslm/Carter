﻿using Carter.Telas;
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
            _viewmodel.AbrirTelaHistoricoSalarios = AbrirTelaHistoricoSalarios;
            AbrirTelaHistoricoSalarios();
        }

        private void AbrirTelaHistoricoSalarios()
        {
            framePrincipal.Navigate(new HistoricoDeSalarios());
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
    }
}
