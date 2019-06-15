using System;

namespace Carter.Models
{
    public class Receitas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public Categoria Categoria { get; set; }
        public string Descricao { get; set; }
    }
}
