using System;

namespace Carter.Models
{
    class Poupanca
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataObjetivo { get; set; }
        public DateTime DataValorPoupado { get; set; }
    }
}
