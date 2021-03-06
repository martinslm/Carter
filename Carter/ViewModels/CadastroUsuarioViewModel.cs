﻿using Carter.DAL;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
namespace Carter.ViewModels
{
    public class CadastroUsuarioViewModel : BindableObject
    {
        #region [Atributos Privados]
        private string _textAvisoCadastro;
        private string _email;
        private decimal _salario;
        private bool _utilizaPoupanca;
        private decimal _valorPoupanca;
        private DateTime _dataObjetivoPoupanca;
        private Categoria _categoriaPoupanca;
        private ICommand _cancelarCommand;
        private ICommand _cadastrarCommand;
        private bool _dadosPoupancaIsEnabled = false;
        private UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private PoupancaDAL _poupancaDAL = new PoupancaDAL();
        private SalarioDAL _salarioDAL = new SalarioDAL();
        #endregion
        public Action AtribuirSenhas;
        public Action <bool> FecharTela { get; set; }
        #region [Atributos Publicos]
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public string TextAvisoCadastro
        {
            get
            {
                return _textAvisoCadastro;
            }
            set
            {
                _textAvisoCadastro = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }
        public decimal Salario
        {
            get
            {
                return _salario;
            }
            set
            {
                _salario = value;
                RaisePropertyChanged();
                PodeCadastrar();
            }
        }
        public bool UtilizaPoupanca
        {
            get
            {
                return _utilizaPoupanca;
            }
            set
            {
                _utilizaPoupanca = value;
                AtualizarIsEnabled();
            }
        }

        private void AtualizarIsEnabled()
        {
            _dadosPoupancaIsEnabled = false;

            if (UtilizaPoupanca == true)
            {
                _dadosPoupancaIsEnabled = true;
            }

            RaisePropertyChanged("DadosPoupancaIsEnabled");
        }

        public decimal ValorPoupanca
        {
            get
            {
                return _valorPoupanca;
            }
            set
            {
                _valorPoupanca = value;
            }
        }
        public DateTime DataObjetivoPoupanca
        {
            get
            {
                return _dataObjetivoPoupanca;
            }
            set
            {
                _dataObjetivoPoupanca = value;
            }
        }
        public Categoria CategoriaPoupanca
        {
            get
            {
                return _categoriaPoupanca;
            }
            set
            {
                _categoriaPoupanca = value;
            }
        }
        public List<Categoria> Categoria
        {
            get
            {
                return _usuarioDAL.BuscarCategorias();
            }
        }
        public bool DadosPoupancaIsEnabled
        {
            get { return _dadosPoupancaIsEnabled; }
        }
        public ICommand CadastrarCommand
        {
            get { return _cadastrarCommand; }
        }
        public ICommand CancelarCommand
        {
            get { return _cancelarCommand; }
        }
        #endregion
        #region [Construtor]
        public CadastroUsuarioViewModel()
        {
            InstanciarCommands();
        }
        #endregion
        private void InstanciarCommands()
        {
            _cadastrarCommand = new CommandHandler(p => CadastrarConta(), p => PodeCadastrar());
            _cancelarCommand = new CommandHandler(p => CancelarConta());

        }

        private void CancelarConta()
        {
            FecharTela(false);
        }

        private void CadastrarConta()
        {
            if(ValidarDados())
            {
                try
                {
                   // using (var transaction = new Transactions.TransactionScope()) - Não ta implementando o Transactions???????? Framework ta ok.
                    int idPoupanca = 0;
                    var idSalario = _salarioDAL.InserirSalarioEObterId(Salario);
                    _usuarioDAL.CadastrarUsuario(Email, Senha, idSalario, UtilizaPoupanca);
                    var idUsuario = _usuarioDAL.ObterIdUsuarioPorEmail(Email);
                    _salarioDAL.VincularIdUsuarioAoSalario(idSalario, idUsuario);

                    if (UtilizaPoupanca)
                    {
                        idPoupanca = _poupancaDAL.CadastrarEObterIdPoupanca(ValorPoupanca, DataObjetivoPoupanca, idUsuario);
                        _usuarioDAL.AtualizarDadosPoupancaPorUsuario(idUsuario, idPoupanca, CategoriaPoupanca);
                        //tela de login incluir ação de enter na senha. 
                    }

                    FecharTela(true);
                }
                catch (Exception Ex)
                {
                    string error = string.Format("Erro ao cadastrar usuário: {0}", Ex);
                    Log.Add(error);
                }
            }
        }

        private bool PodeCadastrar()
        {
            AtribuirSenhas();
            if (Email == null ||  Salario <= 0 || Senha == null || ConfirmacaoSenha == null)
                return false;

                return true;
        }

        private bool ValidarDados()
        {
            if (Senha != ConfirmacaoSenha)
            {
                _textAvisoCadastro = "Senhas não conferem";
                RaisePropertyChanged("TextAvisoCadastro");
                return false;
            }

            if (UtilizaPoupanca && !ValidarDadosPoupanca())
            {
                _textAvisoCadastro = "Você marcou a opção utiliza poupança. \nPortanto, todos os dados devem estar preenchidos.";
                RaisePropertyChanged("TextAvisoCadastro");
                return false;
            }

            if(_usuarioDAL.ValidarExistenciaDeContaPorEmail(Email))
            {
                _textAvisoCadastro = "E-mail já cadastrado.";
                RaisePropertyChanged("TextAvisoCadastro");
                return false;
            }

            return true;

        }

        private bool ValidarDadosPoupanca()
        {
            if (CategoriaPoupanca == null || DataObjetivoPoupanca <= DateTime.Now || ValorPoupanca <= 0)
                return false;

            return true;
        }
    }
}
