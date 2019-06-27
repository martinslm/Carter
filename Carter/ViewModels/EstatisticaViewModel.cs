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
        private string _porcentagemAumento;
        private string _desvioPadraoSalarial;
        private string _mediaReceitas;
        private string _mediaDespesas;
        private string _totalEmConta;
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
        #endregion
        public EstatisticaViewModel()
        {
            ObterValoresDashTotalEmConta();
            ObterValoresDashDespesas();
            ObterValoresDashReceitas();
            ObterValoresDashSalario();
        }

        private void ObterValoresDashTotalEmConta()
        {
            _totalEmConta = _ServicoReceitasDespesas.ObterValorTotalEmConta(_despesas,_receitas, true);
            RaisePropertyChanged("TotalEmConta");
        }

        private void ObterValoresDashDespesas()
        {
            _mediaDespesas = ObterMediaDespesas();
            RaisePropertyChanged("MediaDespesas");
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
                decimal totalReceitas = 0, media;

                foreach (var receita in _receitas)
                {
                    totalReceitas += receita.Valor;
                }

                media = totalReceitas / _receitas.Count();

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
                decimal totalDespesas = 0, media;

                foreach (var despesa in _despesas)
                {
                    totalDespesas += despesa.Valor;
                }

                media = totalDespesas / _despesas.Count();

                return string.Format("R${0:N2}", media);
            }
            catch
            {
                return "R$0,00";
            }
        }

    }
}
