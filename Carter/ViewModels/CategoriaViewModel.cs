using Carter.DAL;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Carter.ViewModels
{
    public class CategoriaViewModel : BindableObject
    {

        private string _descricao;
        private bool _habilitado;
        private CategoriaDAL _categoriaDAL = new CategoriaDAL();
        private UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private ICommand _cadastrarCommand;
        private ICommand _cancelarCommand;
        private ICommand _excluirCommand;
        public Action<bool> FecharTela { get; set; }


        public string Descricao
        {
            get
            {
                return Descricao;
            }
            set
            {
                Descricao = value;
            }
        }
        public List<Categoria> Categoria
        {
            get
            {
                return _usuarioDAL.BuscarCategorias();
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
                    //int idCategoria = 0;
                    var descricao = _descricao;
                   // _categoriaDAL.CadastrarCategoria(Descricao);

                    FecharTela(true);
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
            CadastrarCategoria();
            if (Descricao == null)
                return false;

            return true;
        }
        private bool ValidarDados()
        {
            
            return true;

        }
    }
}
