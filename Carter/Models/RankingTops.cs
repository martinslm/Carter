using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter.Models
{
    public class RankingTops
    {
        public string DescricaoCategoria { get; set; }
        public double TamanhoBarraGrafico
        {
            get
            {
                var tam = Convert.ToDouble(Porcentagem) * 6.5;

                if (tam > 300)
                    return 300;

                return tam < 15 ? 15 : tam;
            }
        }
        public decimal Porcentagem { get; set; }
        public string TextoGrafico
        {
            get
            {
                return string.Format("{0}  -   {1:N2}%", DescricaoCategoria.ToUpper(), Porcentagem);
            }
        }
    }
}
