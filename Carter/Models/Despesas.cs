using System;

namespace Carter.Models
{
    public class Despesas
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public Categoria Categoria { get; set; }
        public int ParcelaAtual { get; set; }
        public int TotalParcelas { get; set; }
        public string ResumoParcela
        {
            get
            {
                return string.Format("{0}/{1}", ParcelaAtual, TotalParcelas);
            }
        }
        public string Descricao { get; set; }
        public bool Pago { get; set; }
    }
}
