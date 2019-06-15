using System;

namespace Carter.Models
{
    public class Despesas
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public Categoria Categoria { get; set; }
        public int ParcelaAtual { get; set; }
        public int TotalParcelas { get; set; }
        public bool Pago { get; set; }
    }
}
