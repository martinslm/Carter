using Carter.DAL;
using Carter.Enums;
using Carter.Models;
using Carter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter.ViewModels
{
    public class EstatisticaViewModel : BindableObject
    {
        #region [Atributos Privados]
        private ServicoReceitasEDespesas _ServicoReceitasDespesas = new ServicoReceitasEDespesas();
        private SalarioDAL _salarioDAL = new SalarioDAL();
        private CategoriaDAL _categoriaDAL = new CategoriaDAL();
        private string _porcentagemAumento;
        private string _desvioPadraoSalarial;
        private string _mediaReceitas;
        private string _mediaDespesas;
        private string _totalEmConta;
        private decimal _totalDespesas;
        private decimal _totalReceitas;
        private int _totalDespesasPendentes; 
        private List<Receitas> _receitas
        {
            get
            {
                return (List<Receitas>)_ServicoReceitasDespesas.ObterReceitasPorPeriodo(PeriodoRelatorio.MesAtual);
            }
        }
        private List<Despesas> _despesas
        {
            get
            {
                return (List<Despesas>)_ServicoReceitasDespesas.ObterDespesasPorPeriodo(PeriodoRelatorio.MesAtual);
            }
        }
        private List<Categoria> _categoriasReceitas
        {
            get
            {
                return _categoriaDAL.ObterTop3Receitas();
            }
        }
        private List<Categoria> _categoriasDespesas
        {
            get
            {
                return _categoriaDAL.ObterTop3Despesas();
            }
        }
        #endregion
        #region [Atributos Publicos]
        public string PorcentagemAumento
        {
            get
            {
                return _porcentagemAumento;
            }
        }
        public string DesvioPadraoSalarial
        {
            get
            {
                return _desvioPadraoSalarial;
            }
        }
        public string MediaReceitas
        {
            get
            {
                return _mediaReceitas;
            }
        }
        public string MediaDespesas
        {
            get
            {
                return _mediaDespesas;
            }
        }
        public string TotalEmConta
        {
            get
            {
                return _totalEmConta;
            }
        }
        public int TotalDespesasPendentes
        {
            get
            {
                return _totalDespesasPendentes;
            }
            set
            {
                _totalDespesasPendentes = value;
            }
        }
        public RankingTops Top1Receita { get; set; }
        public RankingTops Top2Receita { get; set; }
        public RankingTops Top3Receita { get; set; }
        public RankingTops Top1Despesa { get; set; }
        public RankingTops Top2Despesa { get; set; }
        public RankingTops Top3Despesa { get; set; }
        #endregion
        public EstatisticaViewModel()
        {
            ObterValoresDashTotalEmConta();
            ObterValoresDashDespesas();
            ObterValoresDashReceitas();
            ObterValoresDashSalario();
             //ObterValoresDashCategoria();
        }

        private void ObterValoresDashCategoria()
        {
            #region [Atribuir Valores]
            Top1Receita = new RankingTops()
            {
                DescricaoCategoria = _categoriasReceitas.Select(c => c.Descricao).ElementAt(0),
                Porcentagem = (_categoriasReceitas.Select(c => c.ValorTotal).ElementAt(0) * 100) / _totalReceitas
             };
            Top2Receita = new RankingTops()
            {
                DescricaoCategoria = _categoriasReceitas.Select(c => c.Descricao).ElementAt(1),
                Porcentagem = (_categoriasReceitas.Select(c => c.ValorTotal).ElementAt(1) * 100) / _totalReceitas
            };
            Top3Receita = new RankingTops()
            {
                DescricaoCategoria = _categoriasReceitas.Select(c => c.Descricao).ElementAt(2),
                Porcentagem = (_categoriasReceitas.Select(c => c.ValorTotal).ElementAt(2) * 100) / _totalReceitas
            };

            Top1Despesa = new RankingTops()
            {
                DescricaoCategoria = _categoriasDespesas.Select(c => c.Descricao).ElementAt(0),
                Porcentagem = (_categoriasDespesas.Select(c => c.ValorTotal).ElementAt(0) * 100) / _totalDespesas
            };
            Top2Despesa = new RankingTops()
            {
                DescricaoCategoria = _categoriasDespesas.Select(c => c.Descricao).ElementAt(1),
                Porcentagem = (_categoriasDespesas.Select(c => c.ValorTotal).ElementAt(1) * 100) / _totalDespesas
            };
            Top3Despesa = new RankingTops()
            {
                DescricaoCategoria = _categoriasDespesas.Select(c => c.Descricao).ElementAt(2),
                Porcentagem = (_categoriasDespesas.Select(c => c.ValorTotal).ElementAt(2) * 100) / _totalDespesas
            };
            #endregion
            RaisePropertyChanged("Top1Receita");
            RaisePropertyChanged("Top2Receita");
            RaisePropertyChanged("Top3Receita");
            RaisePropertyChanged("Top1Despesa");
            RaisePropertyChanged("Top2Despesa");
            RaisePropertyChanged("Top3Despesa");
        }

        private void ObterValoresDashTotalEmConta()
        {
            _totalEmConta = _ServicoReceitasDespesas.ObterValorTotalEmConta(_despesas,_receitas, true);
            RaisePropertyChanged("TotalEmConta");
        }

        private void ObterValoresDashDespesas()
        {
            _mediaDespesas = ObterMediaDespesas();
            _totalDespesasPendentes = _despesas.Where(d => d.Pago == false).Count();
            RaisePropertyChanged("MediaDespesas");
            RaisePropertyChanged("TotalDespesasPendentes");
        }

        private void ObterValoresDashReceitas()
        {
            _mediaReceitas = ObterMediaReceitas();
            RaisePropertyChanged("MediaReceitas");
        }

        public void ObterValoresDashSalario()
        {
            _porcentagemAumento = ObterPorcentagemAumentoSalarial();
            _desvioPadraoSalarial = ObterDesvioPadraoSalarial();
            RaisePropertyChanged("PorcentagemAumento");
            RaisePropertyChanged("DesvioPadraoSalarial");
        }

        private string ObterDesvioPadraoSalarial()
        {
            try
            {
                decimal totalSalarios = 0, media = 0, desvioPadrao;
                double baseCalculoDesvio = 0;
                var salarios = _salarioDAL.ObterHistoricoSalariosPorUsuario();

                foreach (var salario in salarios)
                {
                    totalSalarios += salario.Valor;
                }

                media = totalSalarios / salarios.Count();

                foreach (var salario in salarios)
                {
                    baseCalculoDesvio += Math.Pow(Convert.ToDouble(salario.Valor - media), 2);
                }

                desvioPadrao = Convert.ToDecimal(Math.Sqrt(baseCalculoDesvio / salarios.Count()));
                return string.Format("R${0:N2}", desvioPadrao);
            }
            catch
            {
                return "R$0,00";
            }
        }

        private string ObterPorcentagemAumentoSalarial()
        {
            try
            {
                var salarios = _salarioDAL.ObterDoisUltimosSalarios();
                var ultimoSalario = salarios.First();
                var salarioAnterior = salarios.Where(s => s != ultimoSalario).First();

                var porcentagemAumentoSalarial = (((ultimoSalario.Valor * 100) / salarioAnterior.Valor) - 100);

                return string.Format("{0:N2}%", porcentagemAumentoSalarial);
            }
            catch
            {
                return "0,00%";
            }
        }

        private string ObterMediaReceitas()
        {
            try
            {
                _totalReceitas = 0;
                decimal media;

                foreach (var receita in _receitas)
                {
                    _totalReceitas += receita.Valor;
                }

                media = _totalReceitas / _receitas.Count();

                return string.Format("R${0:N2}", media);
            }
            catch
            {
                return "R$0,00";
            }
        }

        private string ObterMediaDespesas()
        {
            try
            {
                _totalDespesas = 0;
                decimal media;

                foreach (var despesa in _despesas)
                {
                    _totalDespesas += despesa.Valor;
                }

                media = _totalDespesas / _despesas.Count();

                return string.Format("R${0:N2}", media);
            }
            catch
            {
                return "R$0,00";
            }
        }

    }
}
