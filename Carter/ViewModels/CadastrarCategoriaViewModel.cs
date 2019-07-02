using Carter.DAL;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Carter.ViewModels
{
    public class CadastrarCategoriaViewModel : BindableObject
    {

        private string _descricao;
        private bool _habilitado;
        private CategoriaDAL _categoriaDAL = new CategoriaDAL();
        private UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private IEnumerable<Categoria> _categoria;
        private string _textAvisoCadastro;
        private ICommand _cadastrarCommand;
        private ICommand _cancelarCommand;
        private ICommand _excluirCommand;
        public Action<bool> FecharTela { get; set; }

        /* public IEnumerable<Categoria> Categorias
         {
             get { return _categorias; }
         }*/
        public string Descricao
        {
            get
            {
                return _descricao;
            }
            set
            {
                _descricao = value;
                RaisePropertyChanged("CadastrarCommand");
            }
        }
        public IEnumerable<Categoria> Categoria
        {
            get
            {
                return _categoria;
            }
        }
        public string TextAvisoCadastro
        {
            get { return _textAvisoCadastro; }
            set { _textAvisoCadastro = value; }
        }
        public ICommand CadastrarCommand
        {
            get { return _cadastrarCommand; }
        }
        public ICommand CancelarCommand
        {
            get { return _cancelarCommand; }
        }
        public ICommand ExcluirCommand
        {
            get { return _excluirCommand; }
        }
        public Action AbrirTelaCategorias;
        public CadastrarCategoriaViewModel()
        {
            InstanciarCommands();
            AtualizarListagemCategorias();
        }
        private void InstanciarCommands()
        {
            _cadastrarCommand = new CommandHandler(p => CadastrarCategoria(), p => PodeCadastrar());
            _cancelarCommand = new CommandHandler(p => CancelarCategoria());
            _excluirCommand = new CommandHandler(p => ExluirCategoria());

        }
        private void CancelarCategoria()
        {
            FecharTela(false);
        }
        private void ExluirCategoria()
        {
            //fazer
        }
        private void CadastrarCategoria()
        {
            
            try
            {
                _categoriaDAL.CadastrarCategorias(Descricao);
                Descricao = string.Empty;
                RaisePropertyChanged("Descricao");
                _textAvisoCadastro = "Categoria cadastrada";
                RaisePropertyChanged("TextAvisoCadastro");
                AtualizarListagemCategorias();
                return;
            }
            catch (Exception ex)
            {
                var teste = ex;
                _textAvisoCadastro = "Houve um erro ao cadastrar a categoria";
                RaisePropertyChanged("TextAvisoCadastro");
            }


        }
        private bool PodeCadastrar()
        {
            if (_descricao == null)
                return false;

            return true;
        }
        private bool ValidarDados()
        {

            return true;

        }
        private void AtualizarListagemCategorias()
        {
            _categoria = _usuarioDAL.BuscarCategorias();
            RaisePropertyChanged("Categoria");
        }
    }
}
