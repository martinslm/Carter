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
using System.Windows.Shapes;

namespace Carter.Telas
{
    /// <summary>
    /// Lógica interna para EsqueciMinhaSenha.xaml
    /// </summary>
    public partial class EsqueciMinhaSenha : Window
    {
        private readonly EsqueciMinhaSenhaViewModel _viewmodel;
        public EsqueciMinhaSenha()
        {
            _viewmodel = new EsqueciMinhaSenhaViewModel();
            DataContext = _viewmodel;
            /*InitializeComponent();*/
            InitializeDelegates();
        }
        private void InitializeDelegates()
        {
            _viewmodel.AbrirTelaPrincipal = AbrirTelaPrincipal;
        }

        private void AbrirTelaPrincipal()
        {
            var principal = new Principal();
            this.Close();
            principal.ShowDialog();
        }

        public new bool? ShowDialog()
        {
            Owner.Opacity = 0.85;
            return base.ShowDialog();
        }

        private void FecharTela(bool dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
