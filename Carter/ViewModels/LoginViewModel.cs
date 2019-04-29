using System;
using System.Windows.Input;

namespace Carter.ViewModels
{
    class LoginViewModel
    {
        private string _usuario;
        private string _senha;
        private ICommand _acessarCommand;
        public string Usuario 
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }
        public ICommand AcessarCommand
        {
            get { return _acessarCommand; }
        }

        private void InstanciarCommands()
        {
            _acessarCommand = new CommandHandler(p => FazerLogin());
        }

        private void FazerLogin()
        {
            throw new NotImplementedException();
        }
    }
}
