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
                RaisePropertyChanged();
            }
        }
        public IEnumerable<Categoria> Categoria
        {
            get
            {
                return _categoria;
            }
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

        public CadastrarCategoriaViewModel()
        {
            InstanciarCommands();
            _categoria = _usuarioDAL.BuscarCategorias();
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
            if (ValidarDados())
            {
                try
                {
                    /*int idCategoria = 0;
                    string descricao = _descricao;
                    _categoriaDAL.CadastrarCategorias(Descricao,Habilitado);
                    var idUsuario = _categoriaDAL.ObterDadosCategoriaPorId(Descricao);
                    _categoriaDAL.VincularIdUsuarioAcategoria(idDescricao, idUsuario);*/
                }
                catch (Exception Ex)
                {
                    string error = string.Format("Erro ao cadastrar categoria: {0}", Ex);
                    Log.Add(error);
                }
            }
        }
        private bool PodeCadastrar()
        {
            if (Descricao == null)
                return false;

            return true;
        }
        private bool ValidarDados()
        {

            return true;

        }
        private void AtualizarListagemCategorias()
        {
            //_categorias = _categoriaDAL.ObterDadosCategoriaPorId();
            //erro ao atualizar listagem - VER DEPOIS
            //RaisePropertyChanged();
        }
    }
}
