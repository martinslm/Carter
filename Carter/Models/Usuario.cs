using System;

namespace Carter.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email {get;set;}
        public string Password { get; set; }
        public Salario SalarioAtual { get; set; }
        public bool UtilizaPoupanca { get; set; }
        public Poupanca ObjetivoValorPoupanca { get; set; }
        public Categoria CategoriaPoupanca { get; set; }
    }
}
