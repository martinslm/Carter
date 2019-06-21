using Carter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Carter.ViewModels
{
    public class EsqueciMinhaSenhaViewModel : BindableObject
    {
        private string _usuario;
        private string _senha;
        private ICommand _cancelarCommand;
        private ICommand _confirmarCommand;
        private bool _dadosPoupancaIsEnabled = false;
        private UsuarioDAL _usuarioDAL = new UsuarioDAL();
        public Action AtribuirSenhas;
        public Action<bool> FecharTela { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public string Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                RaisePropertyChanged();
            }
        }
        public ICommand ConfirmarCommand
        {
            get { return _confirmarCommand; }
        }
        public ICommand CancelarCommand
        {
            get { return _cancelarCommand; }
        }
        public Action AbrirTelaPrincipal { get; set; }
        public EsqueciMinhaSenhaViewModel()
        {
            InstanciarCommands();
        }
        private void InstanciarCommands()
        {
            _confirmarCommand = new CommandHandler(p => AbrirTelaPrincipal());
            _cancelarCommand = new CommandHandler(p => AbrirTelaPrincipal());
        }
        private void AlterarSenha(object obj)
        {
            /*
            var passwordDigitado = (Senha)obj;
            _confirmarsenha = passwordDigitado.Password;
            _senha = passwordDigitado.Password;

            int idUsuario = 0;
            var status = _usuarioDAL.StatusLogin(_usuario, _senha, ref idUsuario);

            switch (status)
            {
                case Usuario.UsuarioInvalido:
                    _textAvisoSenha = "Atenção: O valor informado para usuário é inválido";
                    RaisePropertyChanged("TextAvisoUsuario");
                    break;
                case SenhaAtual.SenhaInvalida:
                    _textAvisoSenha = "Atenção: Senha inválida";
                    RaisePropertyChanged("TextAvisoSenhaAtual");
                    break;
                case Senha.SenhaInvalida:
                    __textAvisoSenha = "Atenção: Informe a mesma senha da anterior";
                    RaisePropertyChanged("TextAvisoSenha");
                    break;
                case ConfirmacaoSenha.ConfirmacaoInvalida:
                    __textAvisoSenha = "Atenção: Informe a mesma senha da anterior";
                        RaisePropertyChanged("TextAvisoConfirmarcaoSenha");
                    break;
                case StatusLogin.Sucesso:
                    CarregarUsuarioNaSessao(idUsuario);
                    AbrirTelaPrincipal();
                    break;
            }*/
        }
        private void AlterarSenha(int idUsuario)
        {
            
        }
        private bool PodeAlterar()
        {
            AtribuirSenhas();
            if (Senha == null || ConfirmacaoSenha == null)
                return false;
            /*if (SenhaAtual = ! _usuarioDAL(Senha))
                return false;*/

            return true;
        }
    }
}
