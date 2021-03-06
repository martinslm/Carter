﻿using Carter.DAL;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
namespace Carter.ViewModels
{
    public class MinhaContaViewModel : BindableObject
    {
        #region [Atributos Privados]
        private string _textAvisoCadastro;
        private string _email;
        private decimal _salario;
        private bool _utilizaPoupanca;
        private decimal _valorPoupanca;
        private DateTime _dataObjetivoPoupanca;
        private Categoria _categoriaPoupanca;
        private ICommand _atualizarCommand;
        private bool _dadosPoupancaIsEnabled = false;
        private UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private PoupancaDAL _poupancaDAL = new PoupancaDAL();
        private SalarioDAL _salarioDAL = new SalarioDAL();
        #endregion
        public Action AtribuirSenhas;
        public Action<bool> FecharTela { get; set; }
        #region [Atributos Publicos]

        public string TextAvisoCadastro
        {
            get
            {
                return _textAvisoCadastro;
            }
            set
            {
                _textAvisoCadastro = value;
                RaisePropertyChanged();
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
                PodeAlterar();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
        public ICommand AtualizarCommand
        {
            get { return _atualizarCommand; }
        }
        #endregion
        #region [Construtor]
        public MinhaContaViewModel()
        {
            InstanciarCommands();
            AtribuirDadosUsuarioLogado();
        }

        private void AtribuirDadosUsuarioLogado()
        {
            Email = Sessao.Usuario.Email;
            Salario = Sessao.Usuario.SalarioAtual.Valor;
            UtilizaPoupanca = Sessao.Usuario.UtilizaPoupanca;
            ValorPoupanca = Sessao.Usuario.ObjetivoValorPoupanca != null ? Sessao.Usuario.ObjetivoValorPoupanca.Valor : 0;
            DataObjetivoPoupanca = Sessao.Usuario.ObjetivoValorPoupanca != null ? Sessao.Usuario.ObjetivoValorPoupanca.DataObjetivo : DateTime.MinValue;
            CategoriaPoupanca = Sessao.Usuario.CategoriaPoupanca != null ? Sessao.Usuario.CategoriaPoupanca : new Categoria();

        }
        #endregion
        private void InstanciarCommands()
        {
            _atualizarCommand = new CommandHandler(p => AtualizarConta(), p => PodeAlterar());

        }

        private void Fechar()
        {
            FecharTela(false);
        }

        private void AtualizarConta()
        {
            if (ValidarDados())
            {
                try
                {
                    /* Verificar esse item
                    int idPoupanca = 0;
                    var idSalario = _salarioDAL.AtualizarSalarioAtualUsuario(Salario);
                    _usuarioDAL.CadastrarUsuario(Email, Senha, idSalario, UtilizaPoupanca);
                    var idUsuario = _usuarioDAL.ObterIdUsuarioPorEmail(Email);
                    _salarioDAL.VincularIdUsuarioAoSalario(idSalario, idUsuario);

                    if (UtilizaPoupanca)
                    {
                        idPoupanca = _poupancaDAL.CadastrarEObterIdPoupanca(ValorPoupanca, DataObjetivoPoupanca, idUsuario);
                        _usuarioDAL.AtualizarDadosPoupancaPorUsuario(idUsuario, idPoupanca, CategoriaPoupanca);
                        //tela de login incluir ação de enter na senha. 
                    }
                    */
                    FecharTela(true);
                }
                catch (Exception Ex)
                {
                    string error = string.Format("Erro ao atualizar: {0}", Ex);
                    Log.Add(error);
                }
            }
        }

        private bool PodeAlterar()
        {
            if (Email == null || Salario <= 0)
                return false;

            return true;
        }

        private bool ValidarDados()
        {

            if (UtilizaPoupanca && !ValidarDadosPoupanca())
            {
                _textAvisoCadastro = "Você marcou a opção utiliza poupança. \nPortanto, todos os dados devem estar preenchidos.";
                RaisePropertyChanged("TextAvisoCadastro");
                return false;
            }

            if (_usuarioDAL.ValidarExistenciaDeContaPorEmail(Email))
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
