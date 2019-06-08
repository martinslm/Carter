using Carter.DAL;
using Carter.Enums;
using Carter.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Carter.ViewModels
{
    class LoginViewModel: BindableObject
    {
        #region [Privates]
        private string _usuario;
        private string _senha;
        private string _textAvisoLogin;
        private ICommand _acessarCommand;
        private ICommand _queroMeCadastrarCommand;
        private ICommand _esqueciMinhaSenhaCommand;
        private UsuarioDAL _usuarioDAL = new UsuarioDAL();
        #endregion
        #region [Publics]
        public string Usuario 
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public string TextAvisoLogin
        {
            get { return _textAvisoLogin; }
            set { _textAvisoLogin = value; }
        }
        public ICommand AcessarCommand
        {
            get
            {
                if (_acessarCommand == null)
                {
                    _acessarCommand = new CommandHandler(FazerLogin);
                }

                return _acessarCommand;
            }
        }
        public ICommand QueroMeCadastrarCommand
        {
            get { return _queroMeCadastrarCommand; }
        }
        public ICommand EsqueciMinhaSenhaCommand
        {
            get
            {
                return _esqueciMinhaSenhaCommand;
            }
        }
        public Action AbrirTelaCadastroUsuario { get; set; }
        public Action AbrirTelaEsqueceuASenha { get; set; }
        public Action AbrirTelaPrincipal { get; set; }
        #endregion
        public LoginViewModel()
        {
            InstanciarCommands();
        }
        #region [Methods]
        private void InstanciarCommands()
        {
            _esqueciMinhaSenhaCommand = new CommandHandler(p => AbrirTelaEsqueceuASenha());
            _queroMeCadastrarCommand = new CommandHandler(p => AbrirTelaCadastroUsuario());
        }
        private void FazerLogin(object obj)
        {
            var passwordDigitado = (PasswordBox)obj;
            _senha = passwordDigitado.Password;

           int idUsuario = 0;
           var status = _usuarioDAL.StatusLogin(_usuario, _senha, ref idUsuario);

            switch (status)
            {
                case StatusLogin.EmailInvalido:
                    _textAvisoLogin = "Atenção: O valor informado para usuário é inválido";
                    RaisePropertyChanged("TextAvisoLogin");
                    break;
                case StatusLogin.SenhaInvalida:
                    _textAvisoLogin = "Atenção: Senha inválida";
                    RaisePropertyChanged("TextAvisoLogin");
                    break;
                case StatusLogin.Sucesso:
                    CarregarUsuarioNaSessao(idUsuario);
                    AbrirTelaPrincipal();
                    break;
            }
        }

        private void CarregarUsuarioNaSessao(int idUsuario)
        {
            Sessao.Usuario = _usuarioDAL.ObterDadosUsuarioPorId(idUsuario);
        }
        #endregion
    }
}
