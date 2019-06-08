using System;

namespace Carter.Models
{
    public class Poupanca
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataObjetivo { get; set; }
        public DateTime DataValorPoupado { get; set; }
    }
}
